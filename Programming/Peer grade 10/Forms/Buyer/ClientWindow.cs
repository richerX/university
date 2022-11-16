using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clients
{
    public partial class ClientWindow : Form
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public Form parentForm;
        public Client client;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="client"></param>
        public ClientWindow(Form parentForm, Client client)
        {
            InitializeComponent();
            MinimumSize = new Size(1000, 500);
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            this.parentForm = parentForm;
            this.client = client;

            LoginBox.Text = client.login;
            PasswordBox.Text = client.password;
            NameBox.Text = client.fullName;
            AddressBox.Text = client.address;
            TelephoneBox.Text = client.phoneNumber;
            OrdersBox.Text = GetValues().Item1.ToString();
            PaidBox.Text = GetValues().Item2.ToString();
            UnpaidBox.Text = GetValues().Item3.ToString();

            UpdateOrders();
        }

        #region Update DateGrid

        /// <summary>
        /// Обновление DataGrid
        /// </summary>
        private void UpdateOrders()
        {
            Orders.Rows.Clear();
            Orders.Columns.Clear();
            UpdateVisual(Orders);
            Orders.Columns.Add("ID", "ID");
            Orders.Columns.Add("Товары", "Товары");
            Orders.Columns.Add("Время", "Время");
            Orders.Columns.Add("Статус", "Статус");
            Orders.Columns.Add("Сумма", "Сумма");
            Orders.Columns[0].Width = Orders.Columns[0].Width / 2;

            foreach (Order order in Welcome.shop.orders)
            {
                bool statusValue = order.status.HasFlag(Status.Processed) && !order.status.HasFlag(Status.Paid);
                if (order.client == client && (!CheckBox.Checked || statusValue))
                    Orders.Rows.Add(order.GetValuesWithoutClient());
            }
        }

        /// <summary>
        /// Визуальное обновление
        /// </summary>
        /// <param name="DataGrid"></param>
        private void UpdateVisual(DataGridView DataGrid)
        {
            DataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 26);
            DataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGrid.DefaultCellStyle.BackColor = Color.FromArgb(25, 25, 26);
            DataGrid.DefaultCellStyle.ForeColor = Color.White;
            DataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 50, 50);
            DataGrid.EnableHeadersVisualStyles = false;
            DataGrid.RowHeadersVisible = false;
        }

        #endregion

        #region Дополнительные функции

        /// <summary>
        /// Кнопка назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Welcome.Exit();
        }

        /// <summary>
        /// Кнопка выйти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Кнопка личный кабинет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDataButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока что не работает :(");
        }

        /// <summary>
        /// Кнопка сформировать заказ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeOrderButton_Click(object sender, EventArgs e)
        {
            new Cart(client).ShowDialog();
            UpdateOrders();
        }

        /// <summary>
        /// Смена галочки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOrders();
        }

        /// <summary>
        /// Количества для текстовых лейблов
        /// </summary>
        /// <returns></returns>
        private (int, int, int) GetValues()
        {
            int orders = 0;
            int paid = 0;
            int unpaid = 0;
            foreach (var order in Welcome.shop.orders)
            {
                if (order.client == client)
                {
                    orders += 1;
                    if (order.status.HasFlag(Status.Paid))
                        paid += order.GetTotalSum();
                    else
                        unpaid += order.GetTotalSum();
                }
            }
            return (orders, paid, unpaid);
        }

        #endregion

        #region ПКМ оплата

        /// <summary>
        /// ПКМ на заказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orders_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = Orders.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    Orders.ClearSelection();
                    Orders.Rows[hit.RowIndex].Selected = true;
                    if (Orders.Rows[hit.RowIndex].Cells[3].Value.ToString().Contains("Processed") &&
                        !Orders.Rows[hit.RowIndex].Cells[3].Value.ToString().Contains("Paid"))
                        OrdersMenu.Show(Orders, e.Location);
                }
            }
        }

        /// <summary>
        /// Оплатить заказ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplePay_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Сумма к оплате: {Orders.SelectedRows[0].Cells[4].Value}. Оплатить?", "Оплата", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int orderId = int.Parse(Orders.SelectedRows[0].Cells[0].Value.ToString());
                    foreach (var order in Welcome.shop.orders)
                    {
                        if (order.id == orderId)
                        {
                            order.status |= Status.Paid;
                            UpdateOrders();
                            break;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так...");
                }
            }
        }

        #endregion
    }
}

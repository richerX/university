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
    public partial class Merchant : Form
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public Form parentForm;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="parentForm"></param>
        public Merchant(Form parentForm)
        {
            InitializeComponent();
            MinimumSize = new Size(1000, 500);
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            this.parentForm = parentForm;
            UpdateClients();
            UpdateOrders();
        }

        #region Обновление DataGrid

        /// <summary>
        /// Обновление клиентов
        /// </summary>
        private void UpdateClients()
        {
            Clients.Rows.Clear();
            Clients.Columns.Clear();
            UpdateVisual(Clients);
            Clients.Columns.Add("Полное имя", "Полное имя");
            Clients.Columns.Add("Телефон", "Телефон");
            Clients.Columns.Add("Адрес", "Адрес");
            Clients.Columns.Add("Логин", "Логин");
            Clients.Columns.Add("Пароль", "Пароль");
            foreach (Client client in Welcome.shop.clients)
                Clients.Rows.Add(client.GetValues());
        }

        /// <summary>
        /// Обновление заказов
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
            Orders.Columns.Add("Покупатель", "Покупатель");
            Orders.Columns[0].Width = Orders.Columns[0].Width / 2;
            if (ActiveCheckBox.Checked)
            {
                foreach (Order order in Welcome.shop.orders)
                {
                    if (!order.status.HasFlag(Status.Done))
                        Orders.Rows.Add(order.GetValues());
                }
            }
            else
            {
                foreach (Order order in Welcome.shop.orders)
                    Orders.Rows.Add(order.GetValues());
            }
        }

        /// <summary>
        /// Визуальные преобразования
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

        #region ПКМ меню

        /// <summary>
        /// ПКМ клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clients_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = Clients.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    Clients.ClearSelection();
                    Clients.Rows[hit.RowIndex].Selected = true;
                    ClientMenu.Show(Clients, e.Location);
                }
            }   
        }

        /// <summary>
        /// Подробнее о клиенте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientMenuDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string login = Clients.SelectedRows[0].Cells[3].Value.ToString();
                foreach (var client in Welcome.shop.clients)
                {
                    if (client.login == login)
                    {
                        new ClientDetails(client).ShowDialog();
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так...");
            }
        }

        /// <summary>
        /// ПКМ заказов
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
                    OrdersMenu.Show(Orders, e.Location);
                }
            }
        }

        /// <summary>
        /// Подробнее о заказе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersMenuStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = int.Parse(Orders.SelectedRows[0].Cells[0].Value.ToString());
                foreach (var order in Welcome.shop.orders)
                {
                    if (order.id == orderId)
                    {
                        new OrderDetails(order).ShowDialog();
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

        #endregion

        #region Дополнительные функции

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
        /// Кнопка выйти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Смена галочки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOrders();
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Merchant_FormClosed(object sender, FormClosedEventArgs e)
        {
            Welcome.Exit();
        }

        /// <summary>
        /// Кнопка добавить товар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductButton_Click(object sender, EventArgs e)
        {
            new AddProduct().ShowDialog();
        }

        /// <summary>
        /// Кнопка редактировать товар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeButton_Click(object sender, EventArgs e)
        {
            new ChangeProduct().ShowDialog();
        }

        /// <summary>
        /// Кнопка отчет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            new Report().ShowDialog();
        }

        #endregion
    }
}

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
        public Form parentForm;

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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            parentForm.Show();
            this.Hide();
        }

        private void ActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOrders();
        }

        private void Merchant_FormClosed(object sender, FormClosedEventArgs e)
        {
            Welcome.Exit();
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            new AddProduct().ShowDialog();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            new ChangeProduct().ShowDialog();
        }

        #endregion
    }
}

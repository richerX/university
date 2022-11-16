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
        public Merchant()
        {
            InitializeComponent();
            UpdateLists();
        }

        private void UpdateLists()
        {
            UpdateVisual(Clients);
            Clients.Columns.Add("Полное имя", "Полное имя");
            Clients.Columns.Add("Телефон", "Телефон");
            Clients.Columns.Add("Адрес", "Адрес");
            Clients.Columns.Add("Логин", "Логин");
            Clients.Columns.Add("Пароль", "Пароль");
            foreach (Client client in Welcome.shop.clients)
                Clients.Rows.Add(client.GetValues());

            UpdateVisual(Orders);
            Orders.Columns.Add("ID", "ID");
            Orders.Columns.Add("Товары", "Товары");
            Orders.Columns.Add("Время", "Время");
            Orders.Columns.Add("Статус", "Статус");
            Orders.Columns.Add("Покупатель", "Покупатель");
            foreach (Order order in Welcome.shop.orders)
                Orders.Rows.Add(order.GetValues());
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

        private void Merchant_FormClosed(object sender, FormClosedEventArgs e)
        {
            Welcome.Exit();
        }

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
                        new ClientDetails(client).ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так...");
            }
        }
    }
}

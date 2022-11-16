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
    public partial class OrderDetails : Form
    {
        public Order order;
        public bool paid;

        public OrderDetails(Order order)
        {
            InitializeComponent();
            this.order = order;

            MainTextBox.Text = $"Заказ #{order.id} от {order.time}";
            IdBox.Text = order.id.ToString();
            DateBox.Text = order.time.ToString();
            ClientBox.Text = order.client.fullName;
            AmountBox.Text = $"{order.products.Count} ({GetTotalAmount()})";

            paid = false;
            if (order.status.HasFlag(Status.Paid))
                paid = true;

            if (order.status.HasFlag(Status.Processed))
                ProcessedBox.Checked = true;
            if (order.status.HasFlag(Status.Paid))
                PaidBox.Checked = true;
            if (order.status.HasFlag(Status.Shipped))
                ShippedBox.Checked = true;
            if (order.status.HasFlag(Status.Done))
                DoneBox.Checked = true;

            UpdateData();
        }

        private void UpdateData()
        {
            Products.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 26);
            Products.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Products.DefaultCellStyle.BackColor = Color.FromArgb(25, 25, 26);
            Products.DefaultCellStyle.ForeColor = Color.White;
            Products.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 50, 50);
            Products.EnableHeadersVisualStyles = false;
            Products.RowHeadersVisible = false;

            Products.Columns.Add("ID", "ID");
            Products.Columns.Add("Название", "Название");
            Products.Columns.Add("Цена", "Цена");
            Products.Columns.Add("Количество", "Количество");

            int total = 0;
            foreach (var elem in order.products)
            {
                Products.Rows.Add(new object[] { elem.Item1.id, elem.Item1.name, elem.Item2, elem.Item3 });
                total += elem.Item2 * elem.Item3;
            }
            TotalBox.Text = total.ToString();
        }

        private int GetTotalAmount()
        {
            int total = 0;
            foreach (var product in order.products)
                total += product.Item3;
            return total;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProcessedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ProcessedBox.Checked)
                order.status |= Status.Processed;
            else
                order.status ^= Status.Processed;
        }

        private void ShippedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShippedBox.Checked)
                order.status |= Status.Shipped;
            else
                order.status ^= Status.Shipped;
        }

        private void DoneBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DoneBox.Checked)
                order.status |= Status.Done;
            else
                order.status ^= Status.Done;
        }
    }
}

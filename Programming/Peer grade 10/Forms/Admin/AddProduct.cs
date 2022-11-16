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
    public partial class AddProduct : Form
    {
        /// <summary>
        /// Констурктор класса
        /// </summary>
        public AddProduct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Кнопка назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            int price = 0;
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Введите название товара");
                return;
            }
            foreach (var product in Welcome.shop.products)
            {
                if (NameTextBox.Text == product.name)
                {
                    MessageBox.Show("Товар с таким именем уже есть");
                    return;
                }
            }
            if (!int.TryParse(PriceTextBox.Text, out price) || price <= 0)
            {
                MessageBox.Show("Цена должна быть целым положительным числом");
                return;
            }
            Welcome.shop.products.Add(new Product(NameTextBox.Text, price));
            MessageBox.Show("Товар был успешно добавлен");
        }
    }
}

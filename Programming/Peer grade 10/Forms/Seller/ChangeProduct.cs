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
    public partial class ChangeProduct : Form
    {
        public ChangeProduct()
        {
            InitializeComponent();
            UpdateProduct();
        }

        private Product GetCurrentProduct()
        {
            foreach (var product in Welcome.shop.products)
            {
                if (ProductBox.Text == product.name)
                    return product;
            }
            return null;
        }

        private void UpdateProduct()
        {
            ProductBox.Items.Clear();
            foreach (var product in Welcome.shop.products)
                ProductBox.Items.Add(product.name);

            Product currentProduct = GetCurrentProduct();
            if (currentProduct == null)
                return;

            NameTextBox.Text = currentProduct.name;
            PriceTextBox.Text = currentProduct.price.ToString();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProductBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product currentProduct = GetCurrentProduct();
            if (currentProduct is null)
                return;

            NameTextBox.Text = currentProduct.name;
            PriceTextBox.Text = currentProduct.price.ToString();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            int price = 0;
            Product currentProduct = GetCurrentProduct();

            if (currentProduct is null)
            {
                MessageBox.Show("Товар с таким названием не найден");
                return;
            }
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Введите название товара");
                return;
            }
            if (NameTextBox.Text != ProductBox.Text)
            {
                foreach (var product in Welcome.shop.products)
                {
                    if (NameTextBox.Text == product.name)
                    {
                        MessageBox.Show("Товар с таким именем уже есть");
                        return;
                    }
                }
            }
            if (!int.TryParse(PriceTextBox.Text, out price))
            {
                MessageBox.Show("Цена должна быть целым числом");
                return;
            }

            ProductBox.Text = NameTextBox.Text;
            currentProduct.name = NameTextBox.Text;
            currentProduct.price = price;
            UpdateProduct();
            MessageBox.Show("Товар успешно изменен");
        }
    }
}

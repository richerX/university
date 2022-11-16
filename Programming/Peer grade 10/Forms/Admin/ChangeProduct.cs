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
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ChangeProduct()
        {
            InitializeComponent();
            UpdateProduct();
        }

        #region Обновление DataGrid и основная функция

        /// <summary>
        /// Получить выбранный товар
        /// </summary>
        /// <returns></returns>
        private Product GetCurrentProduct()
        {
            foreach (var product in Welcome.shop.products)
            {
                if (ProductBox.Text == product.name)
                    return product;
            }
            return null;
        }

        /// <summary>
        /// Обновление значений списка
        /// </summary>
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

        /// <summary>
        /// Кнопка изменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!int.TryParse(PriceTextBox.Text, out price) || price <= 0)
            {
                MessageBox.Show("Цена должна быть целым положительным числом");
                return;
            }

            ProductBox.Text = NameTextBox.Text;
            currentProduct.name = NameTextBox.Text;
            currentProduct.price = price;
            UpdateProduct();
            MessageBox.Show("Товар успешно изменен");
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
            Close();
        }

        /// <summary>
        /// Событие на изменение выбранного товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product currentProduct = GetCurrentProduct();
            if (currentProduct is null)
                return;

            NameTextBox.Text = currentProduct.name;
            PriceTextBox.Text = currentProduct.price.ToString();
        }

        #endregion
    }
}

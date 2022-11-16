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
    public partial class Cart : Form
    {
        /// <summary>
        /// Поля класса
        /// </summary>
        public Client client;
        public List<(Product, int, int)> products;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="client"></param>
        public Cart(Client client)
        {
            InitializeComponent();
            this.client = client;
            this.products = new List<(Product, int, int)>();

            PriceLabel.Text = "";
            AmountBox.Text = "";
            SumLabel.Text = "";
            UpdateProduct();
        }

        #region Обновление данных

        /// <summary>
        /// Получение выбранного товара
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
        /// Обноление выпадающего списка
        /// </summary>
        private void UpdateProduct()
        {
            ProductBox.Items.Clear();
            foreach (var product in Welcome.shop.products)
                ProductBox.Items.Add(product.name);
        }

        /// <summary>
        /// Обновление суммы заказа
        /// </summary>
        private void UpdateSum()
        {
            int price, amount;
            if (!int.TryParse(PriceLabel.Text, out price))
                SumLabel.Text = "";
            else if (!int.TryParse(AmountBox.Text, out amount))
                SumLabel.Text = "";
            else
                SumLabel.Text = $"{price * amount}";
        }

        /// <summary>
        /// Обновление корзины
        /// </summary>
        private void UpdateCart()
        {
            UserCart.Rows.Clear();
            UserCart.Columns.Clear();
            UpdateVisual(UserCart);
            UserCart.Columns.Add("Номер", "Номер");
            UserCart.Columns.Add("Название", "Название");
            UserCart.Columns.Add("Цена", "Цена");
            UserCart.Columns.Add("Количество", "Количество");
            UserCart.Columns.Add("Сумма", "Сумма");
            UserCart.Columns[0].Width = UserCart.Columns[0].Width / 2;
            int totalSum = 0;
            for (int i = 0; i < products.Count; i++)
            {
                totalSum += products[i].Item2 * products[i].Item3;
                UserCart.Rows.Add(new object[] { i + 1, products[i].Item1.name, products[i].Item2, products[i].Item3, products[i].Item2 * products[i].Item3 });
            }
            CartSum.Text = $"Сумма в корзине: {totalSum}   ";
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

        #region Кнопки

        /// <summary>
        /// Кнопка сформировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainButton_Click(object sender, EventArgs e)
        {
            if (products.Count == 0)
            {
                MessageBox.Show("Корзина пуста, невозможно сформировать заказ");
                return;
            }
            Welcome.shop.orders.Add(new Order(products, client));
            MessageBox.Show("Заказ был успшено сформирован");
            Close();
        }

        /// <summary>
        /// Кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            Product currentProduct = GetCurrentProduct();
            if (currentProduct is null)
                return;

            string name = currentProduct.name;
            int price = currentProduct.price;
            int amount = 0;
            if (!int.TryParse(AmountBox.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Некорректное количество");
                return;
            }

            int index = -1;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Item1.name == name)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                products.Add((currentProduct, amount, currentProduct.price));
            else
                products[index] = (currentProduct, products[index].Item2 + amount, currentProduct.price);
            UpdateCart();
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
        /// Смена количества
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmountBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSum();
        }

        /// <summary>
        /// Смена выбранного товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product currentProduct = GetCurrentProduct();
            if (currentProduct is null)
                return;

            PriceLabel.Text = currentProduct.price.ToString();
            UpdateSum();
        }

        #endregion
    }
}

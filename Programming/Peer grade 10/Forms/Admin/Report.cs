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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            UpdateProduct();
            UpdateGrid();
        }

        #region Обновление данных

        /// <summary>
        /// Обновление выпадающего поля
        /// </summary>
        private void UpdateProduct()
        {
            ProductBox.Items.Clear();
            foreach (var product in Welcome.shop.products)
                ProductBox.Items.Add(product.name);
        }

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
        /// Обновление DataGrid
        /// </summary>
        private void UpdateGrid()
        {
            ReportGrid.Rows.Clear();
            ReportGrid.Columns.Clear();
            UpdateVisual(ReportGrid);
            ReportGrid.Columns.Add("ID", "ID");
            ReportGrid.Columns.Add("ФИО", "ФИО");
            ReportGrid.Columns.Add("Дата", "Дата");
            ReportGrid.Columns.Add("Телефон", "Телефон");
            ReportGrid.Columns[0].Width = ReportGrid.Columns[0].Width / 3;

            var currentProduct = GetCurrentProduct();
            if (currentProduct != null)
            {
                foreach (Order order in Welcome.shop.orders)
                {
                    foreach (var product in order.products)
                    {
                        if (product.Item1.id == currentProduct.id)
                        {
                            ReportGrid.Rows.Add(new object[] { order.id, order.client.fullName, order.time, order.client.phoneNumber });
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Визуальное обновление DataGrid
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
        /// Смена выбранного товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product currentProduct = GetCurrentProduct();
            if (currentProduct == null)
                return;

            NameTextBox.Text = currentProduct.name;
            PriceTextBox.Text = currentProduct.price.ToString();
            UpdateGrid();
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

        #endregion 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Slider_App
{
    public partial class ProductWindow : Form
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        SliderAppMainWindow mainWindow;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parent"></param>
        public ProductWindow(SliderAppMainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
            ChangeNameParametrBox.SelectedIndex = 0;
            UpdateCategories();
        }

        /// <summary>
        /// Обновить категории в списках.
        /// </summary>
        private void UpdateCategories()
        {
            var boxes = new List<ComboBox>() { AddCategoryBox, ChangeNameCategoryBox,
                ChangePlaceCategoryBox, ChangePlaceNewCategoryBox, DeleteCategoryBox };
            foreach (var box in boxes)
            {
                box.Items.Clear();
                foreach (var category in mainWindow.GetAllCategories())
                    box.Items.Add(category.name);
                box.Items.Add("");
            }
            ChangePlaceNewCategoryBox.Items.Remove("");
        }

        /// <summary>
        /// Обновить товары в списках.
        /// </summary>
        private void UpdateProducts()
        {
            UpdateProduct(ChangeNameCategoryBox.Text, ChangeNameProductBox);
            UpdateProduct(ChangePlaceCategoryBox.Text, ChangePlaceProductBox);
            UpdateProduct(DeleteCategoryBox.Text, DeleteProductBox);
        }

        /// <summary>
        /// Вспомогательная функция обновления товаров.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="box"></param>
        private void UpdateProduct(string category, ComboBox box)
        {
            box.Items.Clear();
            if (category != "")
            {
                foreach (var product in mainWindow.GetCategoryByName(category).products)
                    box.Items.Add(product.name);
            }
        }

        /// <summary>
        /// Проверка уникальности имени.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool NameIsUnique(Category category, string name)
        {
            foreach (var product in category.products)
            {
                if (product.name == name)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Получитьб товар по имени.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        private Product GetProductByName(Category category, string productName)
        {
            foreach (var product in category.products)
            {
                if (product.name == productName)
                    return product;
            }
            return null;
        }



        ///
        /// Кнопки и автоматизации
        /// 

        /// <summary>
        /// Кнопка добавить товар.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, EventArgs e)
        {
            if (AddCategoryBox.Text == "")
                return;

            var category = mainWindow.GetCategoryByName(AddCategoryBox.Text);
            string name = AddNameText.Text;
            string article = AddArticleText.Text;
            string amount = AddAmountText.Text;
            string price = AddPriceText.Text;
            int amountInt, priceInt;

            if (category == null)
            {
                MessageBox.Show("Невозможно добавить в данную категорию");
                return;
            }
            if (name == "" || article == "" || amount == "" || price == "")
            {
                MessageBox.Show("Невозможно добавить товар с пустыми значениями");
                return;
            }
            if (!int.TryParse(amount, out amountInt) || !int.TryParse(price, out priceInt))
            {
                MessageBox.Show("Невозможно добавить товар с нечисловым количеством или ценой");
                return;
            }
            if (!NameIsUnique(category, name))
            {
                MessageBox.Show("Имя товара должно быть уникальным внутри категории");
                return;
            }

            var product = new Product(name, article, amountInt, priceInt);
            category.products.Add(product);
            MessageBox.Show("Товар был успешно добавлен");
            UpdateProducts();
        }

        /// <summary>
        /// Кнопка изменить товар.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeProductNameButton_Click(object sender, EventArgs e)
        {
            if (ChangeNameCategoryBox.Text == "")
                return;

            var category = mainWindow.GetCategoryByName(ChangeNameCategoryBox.Text);
            string productString = ChangeNameProductBox.Text;
            string parametr = ChangeNameParametrBox.Text;
            string newValue = ChangeNameText.Text;

            if (category == null)
            {
                MessageBox.Show("Невозможно изменить товар в данной категории");
                return;
            }
            if (productString == "" || parametr == "" || newValue == "")
            {
                MessageBox.Show("Невозможно изменить товар с пустыми значениями");
                return;
            }

            var product = GetProductByName(category, productString);
            if (product == null)
            {
                MessageBox.Show("Данный товар отсутствует в категории");
                return;
            }

            if (parametr == "Название")
            {
                foreach (var productIter in category.products)
                {
                    if (productIter == product)
                        continue;
                    if (productIter.name == newValue)
                    {
                        MessageBox.Show("Товар с таким именем уже есть в данной категории");
                        return;
                    }
                }
                product.name = newValue;
            }
            else if (parametr == "Артикул")
            {
                product.article = newValue;
            }
            else if (parametr == "Цена")
            {
                int priceInt;
                if (!int.TryParse(newValue, out priceInt))
                {
                    MessageBox.Show("Необходимо ввести числовое значение");
                    return;
                }
                product.price = priceInt;
            }
            else if (parametr == "Количество")
            {
                int amountInt;
                if (!int.TryParse(newValue, out amountInt))
                {
                    MessageBox.Show("Необходимо ввести числовое значение");
                    return;
                }
                product.amount = amountInt;
            }

            MessageBox.Show("Данные товара были успешно изменены");
            UpdateProducts();
        }

        /// <summary>
        /// Кнопка переместить товар.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeProductPlace_Click(object sender, EventArgs e)
        {
            if (ChangePlaceCategoryBox.Text == "")
                return;

            var category = mainWindow.GetCategoryByName(ChangePlaceCategoryBox.Text);
            string productString = ChangePlaceProductBox.Text;
            var newCategory = mainWindow.GetCategoryByName(ChangePlaceNewCategoryBox.Text);

            if (category == null || newCategory == null)
            {
                MessageBox.Show("Невозможно найти данную категорию");
                return;
            }
            if (productString == "")
            {
                MessageBox.Show("Невозможно переместить товар с пустыми значениями");
                return;
            }

            var product = GetProductByName(category, productString);
            if (product == null)
            {
                MessageBox.Show("Данный товар отсутствует в категории");
                return;
            }
            foreach (var productIter in newCategory.products)
            {
                if (productIter.name == product.name)
                {
                    MessageBox.Show("Товар с таким именем уже есть в категории");
                    return;
                }
            }

            category.products.Remove(product);
            newCategory.products.Add(product);
            MessageBox.Show("Товара был успешно перемещен");
            UpdateProducts();
        }

        /// <summary>
        /// Кнопка удалить товар.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProduct_Click(object sender, EventArgs e)
        {
            if (DeleteCategoryBox.Text == "")
                return;

            var category = mainWindow.GetCategoryByName(DeleteCategoryBox.Text);
            string productString = DeleteProductBox.Text;

            if (category == null)
            {
                MessageBox.Show("Невозможно найти данную категорию");
                return;
            }
            if (productString == "")
            {
                MessageBox.Show("Невозможно найти данный товар");
                return;
            }

            var product = GetProductByName(category, productString);
            if (product == null)
            {
                MessageBox.Show("Данный товар отсутствует в категории");
                return;
            }

            category.products.Remove(product);
            MessageBox.Show("Товар был успешно удален");
            UpdateProducts();
        }

        /// <summary>
        /// Скрытие лейблов и боксов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = true;
            if (AddCategoryBox.Text == "")
                value = false;

            AddLabel1.Visible = value;
            AddLabel2.Visible = value;
            AddLabel3.Visible = value;
            AddLabel4.Visible = value;

            AddNameText.Visible = value;
            AddArticleText.Visible = value;
            AddAmountText.Visible = value;
            AddPriceText.Visible = value;
        }

        /// <summary>
        /// Скрытие лейблов и боксов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeNameCategoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = true;
            if (ChangeNameCategoryBox.Text == "")
                value = false;

            ChangeNameLabel1.Visible = value;
            ChangeNameLabel2.Visible = value;
            ChangeNameLabel3.Visible = value;
            ChangeNameProductBox.Visible = value;
            ChangeNameParametrBox.Visible = value;
            ChangeNameText.Visible = value;

            UpdateProducts();
        }

        /// <summary>
        /// Скрытие лейблов и боксов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePlaceCategoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = true;
            if (ChangePlaceCategoryBox.Text == "")
                value = false;

            ChangePlaceLabel1.Visible = value;
            ChangePlaceLabel2.Visible = value;
            ChangePlaceProductBox.Visible = value;
            ChangePlaceNewCategoryBox.Visible = value;

            UpdateProducts();
        }

        /// <summary>
        /// Скрытие лейблов и боксов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCategoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = true;
            if (DeleteCategoryBox.Text == "")
                value = false;

            DeleteLabel1.Visible = value;
            DeleteProductBox.Visible = value;

            UpdateProducts();
        }
    }
}

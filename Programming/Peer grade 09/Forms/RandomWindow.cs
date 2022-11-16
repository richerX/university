using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Slider_App
{
    public partial class RandomWindow : Form
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        Random random = new Random();
        SliderAppMainWindow mainWindow;
        List<Category> storage = new List<Category>();
        List<Category> categories = new List<Category>();
        List<Product> products = new List<Product>();

        int minAmount = 1;
        int maxAmount = 1000;
        int minPrice = 100;
        int maxPrice = 20000;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parent"></param>
        public RandomWindow(SliderAppMainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
        }

        /// <summary>
        /// Кнопка генерация.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Click(object sender, EventArgs e)
        {
            int categoriesAmount = ScanBox(CategoriesBox);
            int productsAmount = ScanBox(ProductsBox);

            if (categoriesAmount < 1 || productsAmount < 1)
            {
                MessageBox.Show("Введите значения больше 0");
                return;
            }

            for (int i = 0; i < categoriesAmount; i++)
                AddCategory();

            for (int i = 0; i < productsAmount; i++)
                AddProduct();

            mainWindow.storage = storage;
            MessageBox.Show("Генерация прошла успешно");
            this.Close();
        }

        /// <summary>
        /// Проверка значение в TextBox.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private int ScanBox(TextBox box)
        {
            string value = box.Text;
            int valueInt = 0;
            int.TryParse(value, out valueInt);
            return valueInt;
        }

        /// <summary>
        /// Добавлене новой категории.
        /// </summary>
        private void AddCategory()
        {
            var newCategory = new Category(categories.Count + 1, GetCategoryName());
            int parentId = random.Next(0, categories.Count + 1);
            newCategory.parentId = parentId;
            if (parentId == 0)
                storage.Add(newCategory);
            else
                GetCategoryById(parentId).children.Add(newCategory);
            categories.Add(newCategory);
        }

        /// <summary>
        /// Добавление нового товара.
        /// </summary>
        private void AddProduct()
        {
            var newProduct = new Product(GetProductName(), GetArticle(), GetAmount(), GetPrice());
            int randomId = random.Next(1, categories.Count + 1);
            var category = GetCategoryById(randomId);
            category.products.Add(newProduct);
            products.Add(newProduct);
        }

        /// <summary>
        /// Создать новое имя категории.
        /// </summary>
        /// <returns></returns>
        private string GetCategoryName()
        {
            return $"Категория {categories.Count + 1}";
        }

        /// <summary>
        /// Создать новое имя товара.
        /// </summary>
        /// <returns></returns>
        private string GetProductName()
        {
            return $"Товар {products.Count + 1}";
        }

        /// <summary>
        /// Создать новой артикул.
        /// </summary>
        /// <returns></returns>
        private string GetArticle()
        {
            string answer = "";
            answer += random.Next(10, 100).ToString();
            answer += "-";
            answer += random.Next(100, 1000).ToString();
            answer += "-";
            answer += random.Next(100, 1000).ToString();
            return answer;
        }

        /// <summary>
        /// Создать рандомное количество.
        /// </summary>
        /// <returns></returns>
        private int GetAmount()
        {
            return random.Next(minAmount, maxAmount + 1);
        }

        /// <summary>
        /// Создать рандомную цену.
        /// </summary>
        /// <returns></returns>
        private int GetPrice()
        {
            return random.Next(minPrice, maxPrice + 1);
        }

        /// <summary>
        /// Получить категорию по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Category GetCategoryById(int id)
        {
            foreach (var category in categories)
            {
                if (category.id == id)
                    return category;
            }
            return null;
        }
    }
}

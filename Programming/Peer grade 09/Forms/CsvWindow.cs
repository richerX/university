using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Slider_App
{
    public partial class CsvWindow : Form
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        SliderAppMainWindow mainWindow;
        List<string> array = new List<string>();
        int parametr = 0;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parent"></param>
        public CsvWindow(SliderAppMainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
            SaveFileDialogCsv.Filter = "CSV(*.csv)|*.csv";
        }

        /// <summary>
        /// Сохранить отчет.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(ParametrBox.Text, out parametr)) 
            {
                MessageBox.Show("Некорректное значение");
                return;
            }
            array.Add("Путь,Артикул,Название товара,Количество товара");
            foreach(var category in mainWindow.storage)
                ProductSearch(category, category.name);

            if (SaveFileDialogCsv.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                using (StreamWriter writer = new StreamWriter(File.Open(SaveFileDialogCsv.FileName, FileMode.Create), Encoding.UTF8))
                {
                    foreach (var elem in array)
                        writer.WriteLine(elem);
                }
                MessageBox.Show("Файл был успешно сохранен");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл");
            }
        }

        /// <summary>
        /// Рекурсивный поиск товаров.
        /// </summary>
        /// <param name="currentCategory"></param>
        /// <param name="path"></param>
        private void ProductSearch(Category currentCategory, string path)
        {
            foreach (var product in currentCategory.products)
            {
                if (product.amount < parametr)
                    array.Add($"{path},{product.article},{product.name},{product.amount}");
            }
            foreach (var category in currentCategory.children)
                ProductSearch(category, path + $"/{category.name}");
        }
    }
}

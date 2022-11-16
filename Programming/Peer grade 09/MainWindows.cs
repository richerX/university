using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Slider_App
{
    public partial class SliderAppMainWindow : Form
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        public List<Category> storage = new List<Category>();

        /// <summary>
        /// Функция инициализации.
        /// </summary>
        public SliderAppMainWindow()
        {
            InitializeComponent();
            Size = new Size(1920, 1080);
            MinimumSize = Screen.PrimaryScreen.Bounds.Size / 2;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            saveFileDialog.Filter = "JSON(*.json)|*.json";
            Initiate();
        }

        /// <summary>
        /// Дополнительная функция инициализации.
        /// </summary>
        private void Initiate()
        {
            DataGrid.Columns.Add("Название товара", "Название товара");
            DataGrid.Columns.Add("Код", "Код");
            DataGrid.Columns.Add("Количество", "Количество");
            DataGrid.Columns.Add("Цена", "Цена");

            DataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 25, 26);
            DataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGrid.DefaultCellStyle.BackColor = Color.FromArgb(25, 25, 26);
            DataGrid.DefaultCellStyle.ForeColor = Color.White;
            DataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 25, 26);
            DataGrid.EnableHeadersVisualStyles = false;
            DataGrid.RowHeadersVisible = false;

            LoadStorage("storage.json");
            try
            {
                UpdateTree();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
        }



        ///
        /// БЛОК КНОПОК.
        /// 

        /// <summary>
        /// Кнопка открыть.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel || !openFileDialog.FileName.EndsWith(".json"))
                return;
            LoadStorage(openFileDialog.FileName);
        }

        /// <summary>
        /// Кнопка сохранить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            SaveStorage(saveFileDialog.FileName);
        }

        /// <summary>
        /// Кнопка управления разделами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Categories_Click(object sender, EventArgs e)
        {
            try
            {
                var window = new CategoryWindow(this);
                window.ShowDialog();
                UpdateTree();
                UpdateDataGrid();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
        }

        /// <summary>
        /// Кнопка управления товарами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Click(object sender, EventArgs e)
        {
            try 
            {
                var window = new ProductWindow(this);
                window.ShowDialog();
                UpdateTree();
                UpdateDataGrid();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
}

        /// <summary>
        /// Кнопка CSV.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CSV_Click(object sender, EventArgs e)
        {
            try
            {
                var window = new CsvWindow(this);
                window.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
        }

        /// <summary>
        /// Кнопка рандомная генерация.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Random_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("При рандомной генерации текущие данные полностью удаляются. Сохраните файл со складом перед тем, как проводить генерацию.");
                var window = new RandomWindow(this);
                window.ShowDialog();
                UpdateTree();
                UpdateDataGrid();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
        }

        /// <summary>
        /// Кнопка подсказка.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, EventArgs e)
        {
            try
            {
                var window = new HelpWindow(this);
                window.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
        }



        ///
        /// БЛОК ВСПОМОГАТЕЛЬНЫХ ФУНКЦИЙ.
        ///

        /// <summary>
        /// Обновление tree view.
        /// </summary>
        public void UpdateTree()
        {
            StorageTree.Nodes.Clear();
            for (int i = 0; i < storage.Count; i++)
            {
                StorageTree.Nodes.Add(new TreeNode(storage[i].name));
                UpdateTreeRecursively(StorageTree.Nodes[i], storage[i]);
            }
            StorageTree.ExpandAll();
        }

        /// <summary>
        /// Рекурсивное обновление tree view.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="currentCategory"></param>
        public void UpdateTreeRecursively(TreeNode currentNode, Category currentCategory)
        {
            foreach (Category elem in currentCategory.children)
            {
                var newNode = new TreeNode(elem.name);
                currentNode.Nodes.Add(newNode);
                UpdateTreeRecursively(newNode, elem);
            }
        }

        /// <summary>
        /// Обновление data grid.
        /// </summary>
        public void UpdateDataGrid()
        {
            Category currentCategory = GetCurrentCategory();
            DataGrid.Rows.Clear();
            CategoryText.Text = "";
            if (currentCategory != null)
            {
                foreach (Product product in currentCategory.products)
                    DataGrid.Rows.Add(product.GetValues());
                CategoryText.Text = currentCategory.name;
            }
        }



        /// <summary>
        /// Сохранения состояния склада.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveStorage(string filename)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(List<Category>));
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                    serializer.WriteObject(stream, storage);
            }
            catch
            {
                MessageBox.Show("При сохранении файла склада произошла ошибка");
            }
        }

        /// <summary>
        /// Загрузка состояния склада.
        /// </summary>
        /// <param name="filename"></param>
        public void LoadStorage(string filename)
        {
            try
            {
                var deserializer = new DataContractJsonSerializer(typeof(List<Category>));
                using (FileStream stream = new FileStream("storage.json", FileMode.Open))
                    storage = deserializer.ReadObject(stream) as List<Category>;
            }
            catch
            {
                MessageBox.Show("При загрузке файла склада произошла ошибка");
            }
        }



        /// <summary>
        /// Получить сейчас выбранную категорию.
        /// </summary>
        /// <returns></returns>
        public Category GetCurrentCategory()
        {
            if (StorageTree.SelectedNode == null && StorageTree.Nodes.Count == 0)
                return null;
            else if (StorageTree.SelectedNode == null)
                return GetCategoryByName(StorageTree.Nodes[0].Text);
            return GetCategoryByName(StorageTree.SelectedNode.Text);
        }

        /// <summary>
        /// Получить категорию по имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Category GetCategoryByName(string name)
        {
            foreach (var category in GetAllCategories())
            {
                if (category.name == name)
                    return category;
            }
            return null;
        }

        /// <summary>
        /// Получить категорию по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(int id)
        {
            foreach (var category in GetAllCategories())
            {
                if (category.id == id)
                    return category;
            }
            return null;
        }

        /// <summary>
        /// Получить все категории.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories()
        {
            List<Category> answer = new List<Category>();
            foreach (var elem in storage)
                GetAllCategoriesRecursively(ref answer, elem);
            return answer;
        }

        /// <summary>
        /// Рекурсивно получить все категории.
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="currentCategory"></param>
        public void GetAllCategoriesRecursively(ref List<Category> answer, Category currentCategory)
        {
            answer.Add(currentCategory);
            foreach (Category elem in currentCategory.children)
                GetAllCategoriesRecursively(ref answer, elem);
        }



        ///
        /// БЛОК СОБЫТИЙ.
        ///

        /// <summary>
        /// Событие выбора категории.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StorageTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                UpdateDataGrid();
            }
            catch
            {
                MessageBox.Show("Упс, что-то пошло не так...");
            }
        }

        /// <summary>
        /// Событие закрытия формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderAppMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveStorage("storage.json");
        }
    }
}

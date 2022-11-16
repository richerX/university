using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Slider_App
{
    public partial class CategoryWindow : Form
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        SliderAppMainWindow mainWindow;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parent"></param>
        public CategoryWindow(SliderAppMainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
            UpdateBoxes();
        }

        /// <summary>
        /// Обновление значений в боксах.
        /// </summary>
        public void UpdateBoxes()
        {
            List<ComboBox> boxes = new List<ComboBox>() { AddComboBox, ChangeNameComboBox,
            ChangePlaceComboBox, ChangePlaceComboBoxParent, DeleteComboBox };
            foreach (var box in boxes)
            {
                box.Items.Clear();
                foreach (var category in mainWindow.GetAllCategories())
                    box.Items.Add(category.name);
                box.Items.Add("");
                box.SelectedIndex = 0;
            }
            DeleteComboBox.Items.Remove("");
            mainWindow.UpdateTree();
        }

        /// <summary>
        /// Создать новый id.
        /// </summary>
        /// <returns></returns>
        private int GetNewId()
        {
            var categories = mainWindow.GetAllCategories();
            int maxId = 0;
            foreach (var category in categories)
            {
                if (category.id > maxId)
                    maxId = category.id;
            }
            return maxId + 1;
        }

        /// <summary>
        /// Проверка уникальности имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool NameIsUnique(string name)
        {
            var categories = mainWindow.GetAllCategories();
            foreach (var category in categories)
            {
                if (category.name == name)
                    return false;
            }
            return true;
        }



        ///
        /// Кнопки и автоматизации.
        ///

        /// <summary>
        /// Кнопка добавить категорию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategory_Click(object sender, EventArgs e)
        {
            var parentCategory = mainWindow.GetCategoryByName(AddComboBox.Text);
            if (AddText.Text == "" || !NameIsUnique(AddText.Text))
            {
                MessageBox.Show("Невозможно добавить раздел с таким именем");
                return;
            }
            if (AddComboBox.Text != "" && parentCategory == null)
            {
                MessageBox.Show("Такого раздела не существует");
                return;
            }

            if (parentCategory == null)
                mainWindow.storage.Add(new Category(GetNewId(), AddText.Text));
            else
                parentCategory.children.Add(new Category(GetNewId(), AddText.Text, parentCategory.id));

            UpdateBoxes();
        }

        /// <summary>
        /// Кнопка изменить категорию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeCategoryNameButton_Click(object sender, EventArgs e)
        {
            if (ChangeNameComboBox.Text == "")
                return;

            var category = mainWindow.GetCategoryByName(ChangeNameComboBox.Text);
            if (ChangeText.Text == "" || !NameIsUnique(ChangeText.Text))
            {
                MessageBox.Show("Невозможно изменить раздел с таким именем");
                return;
            }
            if (ChangeNameComboBox.Text != "" && category == null)
            {
                MessageBox.Show("Такого раздела не существует");
                return;
            }

            category.name = ChangeText.Text;
            UpdateBoxes();
        }

        /// <summary>
        /// Кнопка переместить категорию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeCategoryPlace_Click(object sender, EventArgs e)
        {
            if (ChangePlaceComboBox.Text == "")
                return;

            var currentCategory = mainWindow.GetCategoryByName(ChangePlaceComboBox.Text);
            var parentCategory = mainWindow.GetCategoryById(currentCategory.parentId);
            var newParentCategory = mainWindow.GetCategoryByName(ChangePlaceComboBoxParent.Text);

            if (currentCategory.children.Count > 0)
            {
                MessageBox.Show("Невозмонжо переместить категорию, у которой есть подкатегории");
                return;
            }
            if (ChangePlaceComboBox.Text != "" && currentCategory == null)
            {
                MessageBox.Show("Такой категории не существует");
                return;
            }
            if (ChangePlaceComboBoxParent.Text != "" && newParentCategory == null)
            {
                MessageBox.Show("Такой категории не существует");
                return;
            }
            if (currentCategory == newParentCategory)
            {
                MessageBox.Show("Текущая и родительская категории совпадают");
                return;
            }


            if (parentCategory == null)
                mainWindow.storage.Remove(currentCategory);
            else
                parentCategory.children.Remove(currentCategory);
            if (newParentCategory == null)
            {
                currentCategory.parentId = 0;
                mainWindow.storage.Add(currentCategory);
            }
            else
            {
                currentCategory.parentId = newParentCategory.id;
                newParentCategory.children.Add(currentCategory);
            }

            UpdateBoxes();
        }

        /// <summary>
        /// Кнопка удалить категорию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCategory_Click(object sender, EventArgs e)
        {
            var currentCategory = mainWindow.GetCategoryByName(DeleteComboBox.Text);
            if (currentCategory == null || currentCategory.children.Count > 0 || currentCategory.products.Count > 0)
            {
                MessageBox.Show("Невозможно удалить этот раздел");
                return;
            }

            if (currentCategory.parentId == 0)
                mainWindow.storage.Remove(currentCategory);
            else
                mainWindow.GetCategoryById(currentCategory.parentId).children.Remove(currentCategory);

            UpdateBoxes();
        }

        /// <summary>
        /// Скрытие элементов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = true;
            if (ChangeNameComboBox.Text == "")
                value = false;

            NameLabel2.Visible = value;
            ChangeText.Visible = value;
        }

        /// <summary>
        /// Скрытие элементов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePlaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool value = true;
            if (ChangePlaceComboBox.Text == "")
                value = false;

            ParentLabel.Visible = value;
            ChangePlaceComboBoxParent.Visible = value;
        }
    }
}

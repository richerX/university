using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Slider_App
{
    public partial class HelpWindow : Form
    {
        /// <summary>
        /// Поля класса.
        /// </summary>
        SliderAppMainWindow mainWindow;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parent"></param>
        public HelpWindow(SliderAppMainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
        }

        /// <summary>
        /// Кнопка закрытия формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategoryButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

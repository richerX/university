using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Slider_App
{
    public partial class Graph : Form
    {
        // Поля класса.
        string graphName;
        List<string> elements = new List<string>();
        List<double> elementsDouble = new List<double>();
        bool elementsAreDouble;
        PlotView graph = new PlotView();

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="graphName"></param>
        public Graph(List<string> elements, string graphName)
        {
            InitializeComponent();
            MinimumSize = new Size(500, 500);
            this.elements = elements;
            this.graphName = graphName;
            elementsAreDouble = false;
            saveFileDialog.Filter = "png(*.png)|*.png";
            Draw(new object(), new EventArgs());
        }

        /// <summary>
        /// Второй конструктор класса.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="graphName"></param>
        public Graph(List<double> elements, string graphName)
        {
            InitializeComponent();
            MinimumSize = new Size(500, 500);
            elementsDouble = elements;
            elementsAreDouble = true;
            this.graphName = graphName;
            saveFileDialog.Filter = "png(*.png)|*.png";
            Draw(new object(), new EventArgs());
        }

        /// <summary>
        /// Функция рисования гистограммы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Draw(object sender, EventArgs e)
        {
            // Создание графика.
            mainPanel.Controls.Clear();
            graph.Location = new Point(0, 0);
            graph.Size = new Size((int)(0.9 * Size.Width), (int)(0.8 * Size.Height));
            graph.Model = new PlotModel();
            graph.Model.Title = graphName;

            // Добавление подписей и значений.
            var categoryAxis = new CategoryAxis();
            var series = new ColumnSeries();
            foreach (var pair in GetDictionary())
            {
                categoryAxis.ActualLabels.Add(pair.Key);
                series.Items.Add(new ColumnItem(pair.Value));
            }

            // Вывод графика.
            series.FillColor = OxyColor.FromRgb(25, 25, 26);
            categoryAxis.Position = AxisPosition.Bottom;
            graph.Model.Axes.Add(categoryAxis);
            graph.Model.Series.Add(series);
            mainPanel.Controls.Add(graph);
        }

        /// <summary>
        /// Получения словаря значений для графика.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> GetDictionary()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            if (elementsAreDouble)
                return GetDictionaryDouble();
            foreach (var elem in elements)
            {
                if (!dictionary.ContainsKey(elem))
                    dictionary[elem] = 0;
                dictionary[elem] += 1;
            }
            return dictionary;
        }

        /// <summary>
        /// Получения словаря значений для графика.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> GetDictionaryDouble()
        {
            // Создание групп разделения.
            var numericKeys = GetNumericKeys();
            Dictionary<List<double>, int> groups = new Dictionary<List<double>, int>();
            foreach (var elem in numericKeys)
                groups[elem] = 0;

            // Подсчет значений по группам.
            foreach (var elem in elementsDouble)
            {
                foreach (var key in numericKeys)
                {
                    if (key.Contains(elem))
                    {
                        groups[key] += 1;
                        break;
                    }
                }
            }

            // Создание ответа.
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (var pair in groups)
                dictionary[GroupString(pair.Key)] = pair.Value;
            return dictionary;
        }

        /// <summary>
        /// Отбор значений в группы по показателю numericUpDown.
        /// </summary>
        /// <returns></returns>
        private List<List<double>> GetNumericKeys()
        {
            int groupLength = (int) numericUpDown.Value;
            var groups = new List<List<double>>();
            int currentLength = groupLength;
            foreach (var elem in UniqieDoubles(elementsDouble))
            {
                if (currentLength % groupLength == 0)
                    groups.Add(new List<double>() { elem });
                else
                    groups[^1].Add(elem);
                currentLength += 1;
            }
            groupLabel.Text = $"Всего групп: {groups.Count}";
            return groups;
        }

        /// <summary>
        /// Выборка уникальных элементов.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private List<double> UniqieDoubles(List<double> array)
        {
            var answer = new List<double>();
            foreach (var elem in array)
            {
                if (!answer.Contains(elem))
                    answer.Add(elem);
            }
            return answer;
        }

        /// <summary>
        /// Представление группы отбора в виде строки.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string GroupString(List<double> array)
        {
            double maximum = double.MinValue;
            double minimum = double.MaxValue;
            foreach (var elem in array)
            {
                if (elem > maximum)
                    maximum = elem;
                if (elem < minimum)
                    minimum = elem;
            }
            if (minimum == maximum)
                return $"{minimum}";
            return $"{minimum} - {maximum}";
        }

        /// <summary>
        /// Сохранения графика в виде png.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // Сохранение файла.
            var pngExporter = new PngExporter { Width = 2560, Height = 1440, Background = OxyColors.White };
            pngExporter.ExportToFile(graph.Model, saveFileDialog.FileName);
        }
    }
}

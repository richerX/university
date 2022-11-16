using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Slider_App
{
    public partial class Function : Form
    {
        // Поля класса.
        string functionName;
        List<double> elementsX;
        List<double> elementsY;
        PlotView graph = new PlotView();
        int length;

        /// <summary>
        /// Конструктор класс.
        /// </summary>
        /// <param name="elementsX"></param>
        /// <param name="elementsY"></param>
        /// <param name="functionName"></param>
        public Function(List<double> elementsX, List<double> elementsY, string functionName)
        {
            InitializeComponent();
            MinimumSize = new Size(500, 500);
            this.elementsX = elementsX;
            this.elementsY = elementsY;
            this.functionName = functionName;
            saveFileDialog.Filter = "png(*.png)|*.png";
            length = Math.Min(elementsX.Count, elementsY.Count);
            Draw(new object(), new EventArgs());
        }

        /// <summary>
        /// Функция прорисовки гистограммы.
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
            graph.Model.Title = functionName;

            // Добавление подписей и значений.
            var series = new StairStepSeries();
            foreach (var pair in GetDictionary())
                series.Points.Add(new DataPoint(pair.Key, pair.Value));

            // Вывод графика.
            series.Color = OxyColor.FromRgb(25, 25, 26);
            graph.Model.Series.Add(series);
            mainPanel.Controls.Add(graph);
        }

        /// <summary>
        /// Создает словарь пар точек x-y.
        /// </summary>
        /// <returns></returns>
        private Dictionary<double, double> GetDictionary()
        {
            Dictionary<double, double> total = new Dictionary<double, double>();
            Dictionary<double, int> amount = new Dictionary<double, int>();
            for (int i = 0; i < length; i++)
            {
                if (!total.ContainsKey(elementsX[i]))
                {
                    total[elementsX[i]] = 0;
                    amount[elementsX[i]] = 0;
                }
                total[elementsX[i]] += elementsY[i];
                amount[elementsX[i]] += 1;
            }

            Dictionary<double, double> answer = new Dictionary<double, double>();
            foreach (var pair in total)
                answer[pair.Key] = pair.Value / amount[pair.Key];
            return answer;
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

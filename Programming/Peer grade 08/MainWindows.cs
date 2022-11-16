using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

namespace Slider_App
{
    public partial class SliderAppMainWindow : Form
    {
        /// <summary>
        /// Функция запуска окна приложения.
        /// </summary>
        public SliderAppMainWindow()
        {
            InitializeComponent();
            // Задание границ окна.
            Size = new Size(1920, 1080);
            MinimumSize = Screen.PrimaryScreen.Bounds.Size / 2;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            OpenButton.ShortcutKeys = Keys.Control | Keys.O;
            InitialMessage();
        }

        /// <summary>
        /// Вступительное сообщение.
        /// </summary>
        private void InitialMessage()
        {
            string message = "";
            message += "Привет" + Environment.NewLine;
            message += Environment.NewLine;
            message += "Графики можно увеличивать с помощью колеса мышки" + Environment.NewLine;
            message += "Также графики можно передвигать с зажатой ПКМ" + Environment.NewLine;
            message += "Каждое окно можно увеличивать и уменьшать" + Environment.NewLine;
            message += Environment.NewLine;
            message += "Хорошего использования программы :)" + Environment.NewLine;
            MessageBox.Show(message);
        }



        ///
        /// БЛОК КНОПКИ "ОТКРЫТЬ".
        /// 

        /// <summary>
        /// Кнопка "Открыть".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel || !openFileDialog.FileName.EndsWith(".csv"))
                return;
            try
            {
                DataTable table = ReadFile(openFileDialog.FileName);
                DataGrid.Columns.Clear();
                DataGrid.Rows.Clear();
                foreach (var column in table.Columns)
                    DataGrid.Columns.Add(column.ToString(), column.ToString().ToUpper());
                foreach (DataRow row in table.Rows)
                    DataGrid.Rows.Add(row.ItemArray);
            }
            catch
            {
                MessageBox.Show("При открытии таблицы произошла ошибка.");
                DataGrid.Columns.Clear();
                DataGrid.Rows.Clear();
            }
        }

        /// <summary>
        /// Чтение csv файла с данными.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable ReadFile(string fileName)
        {
            DataTable result = new DataTable();
            using (TextFieldParser text = new TextFieldParser(fileName))
            {
                text.SetDelimiters(",");
                // Get column's names
                if (!text.EndOfData)
                {
                    string[] cells = text.ReadFields();
                    foreach (var cell in cells)
                        result.Columns.Add(cell);
                }
                // Get data
                while (!text.EndOfData)
                    result.Rows.Add(text.ReadFields());
            }
            return result;
        }



        ///
        /// БЛОК КНОПКИ "ГИСТОГРАММА СТОЛБЦЫ".
        /// 

        /// <summary>
        /// Кнопка "Гистограмма стобца".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graph_Click(object sender, EventArgs e)
        {
            int column;
            if (!int.TryParse(GraphBox.Text, out column) || column < 0 || column > DataGrid.Columns.Count)
            {
                IncorrectColumn();
                return;
            }
            // Создание графика.
            if (VerifyColumnValues(column))
            {
                var values = GetColumnValues(column);
                values.Sort();
                var form = new Graph(values, DataGrid.Columns[column - 1].HeaderText);
                form.Show();
            }
            else
            {
                var strings = GetColumnStrings(column);
                strings.Sort();
                var form = new Graph(strings, DataGrid.Columns[column - 1].HeaderText);
                form.Show();
            }
        }



        ///
        /// БЛОК КНОПКИ "ГРАФИК ДВУХ СТОБЦОВ".
        /// 

        /// <summary>
        /// Кнопка "График двух столбцов".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphTwo_Click(object sender, EventArgs e)
        {
            int column1, column2;
            if (!int.TryParse(GraphTwoBox1.Text, out column1) || column1 < 0 || column1 > DataGrid.Columns.Count || !VerifyColumnValues(column1))
            {
                IncorrectColumn();
                return;
            }
            if (!int.TryParse(GraphTwoBox2.Text, out column2) || column2 < 0 || column2 > DataGrid.Columns.Count || !VerifyColumnValues(column2))
            {
                IncorrectColumn();
                return;
            }
            // Создание графика.
            var values1 = GetColumnValues(column1).ToArray();
            var values2 = GetColumnValues(column2).ToArray();
            Array.Sort(values1, values2);
            Array.Sort(values1);
            string header = $"{DataGrid.Columns[column1 - 1].HeaderText} -> {DataGrid.Columns[column2 - 1].HeaderText}";
            var form = new Function(new List<double>(values1), new List<double>(values2), header);
            form.Show();
        }



        ///
        /// БЛОК КНОПКИ "СТАТИСТИЧЕСКИЕ ДАННЫЕ".
        /// 

        /// <summary>
        /// Кнопка "Статистические данные".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Statistics_Click(object sender, EventArgs e)
        {
            int column;
            if (!int.TryParse(StatisticsBox.Text, out column) || column < 0 || column > DataGrid.Columns.Count || !VerifyColumnValues(column))
            {
                IncorrectColumn();
                return;
            }

            List<double> numbers = GetColumnValues(column);
            string answer = "";
            answer += $"Столбец: {DataGrid.Columns[column - 1].HeaderText}" + Environment.NewLine;
            answer += Environment.NewLine;
            answer += $"Медиана: {GetMedian(numbers):f2}" + Environment.NewLine;
            answer += $"Дисперсия: {GetVariance(numbers):f2}" + Environment.NewLine;
            answer += $"Среднее значение: {GetAverage(numbers):f2}" + Environment.NewLine;
            answer += $"Среднекв. отклонение: {GetStandardDeviation(numbers):f2}" + Environment.NewLine;
            MessageBox.Show(answer);
        }

        /// <summary>
        /// Медиана набора чисел.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private double GetMedian(List<double> numbers)
        {
            numbers.Sort();
            int length = numbers.Count;
            if (length % 2 == 1)
                return numbers[length / 2];
            return (numbers[length / 2] + numbers[length / 2 - 1]) / 2;
        }

        /// <summary>
        /// Дисперсия набора чисел.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private double GetVariance(List<double> numbers)
        {
            double average = GetAverage(numbers);
            List<double> deviations = new List<double>();
            foreach (var elem in numbers)
                deviations.Add(Math.Pow(elem - average, 2));
            return GetAverage(deviations);
        }

        /// <summary>
        /// Среднее значение набора чисел.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private double GetAverage(List<double> numbers)
        {
            double sum = 0;
            foreach (var elem in numbers)
                sum += elem;
            return sum / numbers.Count;
        }

        /// <summary>
        /// Среднеквадратичное отклонение.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private double GetStandardDeviation(List<double> numbers)
        {
            return Math.Sqrt(GetVariance(numbers));
        }



        ///
        /// БЛОК ВСПОМОГАТЕЛЬНЫХ ФУНКЦИЙ.
        /// 

        /// <summary>
        /// Сообщение о некорректно выбранном столбце.
        /// </summary>
        private void IncorrectColumn()
        {
            MessageBox.Show("Некорректный столбец.");
        }

        /// <summary>
        /// Проверка столбца таблица.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool VerifyColumnValues(int column)
        {
            double number;
            for (int i = 0; i < DataGrid.Rows.Count - 1; i++)
            {
                if (!double.TryParse(DataGrid[column - 1, i].Value.ToString().Replace(".", ","), out number))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Получение значений столбца таблицы.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private List<double> GetColumnValues(int column)
        {
            List<double> answer = new List<double>();
            for (int i = 0; i < DataGrid.Rows.Count - 1; i++)
                answer.Add(double.Parse(DataGrid[column - 1, i].Value.ToString().Replace(".", ",")));
            return answer;
        }

        /// <summary>
        /// Поулчение строк столбца таблицы.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private List<string> GetColumnStrings(int column)
        {
            List<string> answer = new List<string>();
            for (int i = 0; i < DataGrid.Rows.Count - 1; i++)
                answer.Add(DataGrid[column - 1, i].Value.ToString());
            return answer;
        }
    }
}

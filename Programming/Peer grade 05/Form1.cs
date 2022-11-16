using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Fractals
{
    public partial class MainWindow : Form
    {
        //
        // Ввод переменных.
        //

        public string currentFractal = "";

        //
        // Функции инициализации.
        //

        // Запуск основного окна.
        public MainWindow()
        {
            // Инициализация.
            InitializeComponent();
            // Задание границ размеров окна.
            Size = Screen.PrimaryScreen.Bounds.Size * 2 / 3;
            Size = new Size(1920, 1080);
            MinimumSize = Screen.PrimaryScreen.Bounds.Size / 2;
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            // Добавление лого и обводка кнопок.
            LogoPicture.Image = Properties.Resources.tree;
            ButtonPythagorean.Image = Properties.Resources.button;
            ButtonKoch.Image = Properties.Resources.button;
            ButtonSierpinski.Image = Properties.Resources.button;
            ButtonSierpinskiTwo.Image = Properties.Resources.button;
            ButtonCantor.Image = Properties.Resources.button;
            // Выставление значений по умолчанию.
            ColorBoxBegin.SelectedItem = "Белый";
            ColorBoxEnd.SelectedItem = "Серый";
            LengthCheckBox.Checked = true;
        }

        //
        // Кнопки.
        //

        // Автоматическая перерисовка фрактала.
        public void RefreshGraphics(object sender, EventArgs e)
        {
            if (RedrawCheckBox.Checked)
            {
                switch (currentFractal)
                {
                    case ("Pythagorean"):
                        ButtonPythagorean_Click(sender, e);
                        break;
                    case ("Koch"):
                        ButtonKoch_Click(sender, e);
                        break;
                    case ("Sierpinski"):
                        ButtonSierpinski_Click(sender, e);
                        break;
                    case ("Sierpinski2"):
                        ButtonSierpinski2_Click(sender, e);
                        break;
                    case ("Cantor"):
                        ButtonCantor_Click(sender, e);
                        break;
                }
            }
        }

        // Нажатие кнопки "Дерево Пифагора"
        private void ButtonPythagorean_Click(object sender, EventArgs e)
        {
            // Вывод текста и создание графики.
            InfoTextBox.Text = ButtonPythagorean.Text;
            currentFractal = "Pythagorean";
            var graphics = GetGraphics();
            graphics.Clear(Color.FromArgb(25, 25, 26));

            // Создание фрактала и графики.
            var fractal = new PythagoreanFractal();
            fractal.graphics = graphics;
            fractal.windowHeight = CenterPanel.Height;
            fractal.colorBegin = GetColor(ColorBoxBegin.Text);
            fractal.colorEnd = GetColor(ColorBoxEnd.Text);

            // Ввод переменных общих для всех фракталов.
            fractal.steps = RecursionTrackBar.Value;
            fractal.gradientSteps = fractal.steps;
            if (LengthCheckBox.Checked)
            {
                int value = (int)((this.Size.Height - 100) / GetLengthRatio(CoeffTrackBar.Value));
                fractal.length = value;
                LengthLabel.Text = String.Format($"Длина: {value}");
                if (value > LengthTrackBar1.Maximum)
                    LengthTrackBar1.Value = LengthTrackBar1.Maximum;
                else if (value < LengthTrackBar1.Minimum)
                    LengthTrackBar1.Value = LengthTrackBar1.Minimum;
                else
                    LengthTrackBar1.Value = value;
            }
            else
                fractal.length = LengthTrackBar1.Value;

            // Ввод переменных для фрактала Пифагора.
            fractal.angleLeft = LeftAngleTrackBar.Value;
            fractal.angleRight = RightAngleTrackBar.Value;
            fractal.lengthCoefficient = CoeffTrackBar.Value / 100.0;

            // Рисование фрактала.
            float x = (this.Size.Width - 300) / 2;
            float y = 20;
            int currentStep = 0;
            double angle = 0;
            PointF point1 = new PointF(0, 0);
            PointF point2 = new PointF(0, 0);
            DrawFractal(fractal, x, y, currentStep, angle, point1, point2);
        }

        // Нажатие кнопки "Кривая Коха"
        private void ButtonKoch_Click(object sender, EventArgs e)
        {
            // Вывод текста, создание переменных и графики.
            InfoTextBox.Text = ButtonKoch.Text;
            currentFractal = "Koch";
            var graphics = GetGraphics();
            graphics.Clear(Color.FromArgb(25, 25, 26));

            // Создание фрактала и графики.
            var fractal = new KochFractal();
            fractal.graphics = graphics;
            fractal.windowHeight = CenterPanel.Height;
            fractal.colorBegin = GetColor(ColorBoxBegin.Text);
            fractal.colorEnd = GetColor(ColorBoxEnd.Text);

            // Ввод переменных общих для всех фракталов.
            fractal.steps = RecursionTrackBar.Value;
            fractal.gradientSteps = (int)(Math.Pow(4, fractal.steps - 1));
            if (LengthCheckBox.Checked)
            {
                int value = Size.Width - 400;
                fractal.length = value;
                LengthLabel.Text = String.Format($"Длина: {value}");
                if (value > LengthTrackBar1.Maximum)
                    LengthTrackBar1.Value = LengthTrackBar1.Maximum;
                else if (value < LengthTrackBar1.Minimum)
                    LengthTrackBar1.Value = LengthTrackBar1.Minimum;
                else
                    LengthTrackBar1.Value = value;
            }
            else
                fractal.length = LengthTrackBar1.Value;

            // Рисование фрактала.
            float x = 50;
            float y = Size.Height - 300;
            int currentStep = 0;
            double angle = 0;
            PointF point1 = new PointF(x + XOffsetTrackBar.Value, y + YOffsetTrackBar.Value);
            PointF point2 = new PointF(x + fractal.length + XOffsetTrackBar.Value, y + YOffsetTrackBar.Value);
            DrawFractal(fractal, x, y, currentStep, angle, point1, point2);
        }

        // Нажатие кнопки "Ковер Серпинского"
        private void ButtonSierpinski_Click(object sender, EventArgs e)
        {
            // Вывод текста, создание переменных и графики.
            InfoTextBox.Text = ButtonSierpinski.Text;
            currentFractal = "Sierpinski";
            var graphics = GetGraphics();
            graphics.Clear(Color.FromArgb(25, 25, 26));

            // Создание фрактала и графики.
            var fractal = new SierpinskiFractal();
            fractal.graphics = graphics;
            fractal.windowHeight = CenterPanel.Height;
            fractal.colorBegin = GetColor(ColorBoxBegin.Text);
            fractal.colorEnd = GetColor(ColorBoxEnd.Text);

            // Ввод переменных общих для всех фракталов.
            fractal.steps = RecursionTrackBar.Value;
            fractal.gradientSteps = fractal.steps;
            if (LengthCheckBox.Checked)
            {
                int value = Math.Min(Size.Width, Size.Height) - 300;
                fractal.length = value;
                LengthLabel.Text = String.Format($"Длина: {value}");
                if (value > LengthTrackBar1.Maximum)
                    LengthTrackBar1.Value = LengthTrackBar1.Maximum;
                else if (value < LengthTrackBar1.Minimum)
                    LengthTrackBar1.Value = LengthTrackBar1.Minimum;
                else
                    LengthTrackBar1.Value = value;
            }
            else
                fractal.length = LengthTrackBar1.Value;

            // Рисование фрактала.
            float x = (Size.Width - fractal.length - 300) / 2;
            float y = 75;
            int currentStep = 0;
            double angle = 0;
            PointF point1 = new PointF(0, 0);
            PointF point2 = new PointF(0, 0);
            DrawFractal(fractal, x, y, currentStep, angle, point1, point2);
        }

        // Нажатие кнопки "Треугольник Серпинского"
        private void ButtonSierpinski2_Click(object sender, EventArgs e)
        {
            // Вывод текста, создание переменных и графики.
            InfoTextBox.Text = ButtonSierpinskiTwo.Text;
            currentFractal = "Sierpinski2";
            var graphics = GetGraphics();
            graphics.Clear(Color.FromArgb(25, 25, 26));

            // Создание фрактала и графики.
            var fractal = new Sierpinski2Fractal();
            fractal.graphics = graphics;
            fractal.windowHeight = CenterPanel.Height;
            fractal.colorBegin = GetColor(ColorBoxBegin.Text);
            fractal.colorEnd = GetColor(ColorBoxEnd.Text);

            // Ввод переменных общих для всех фракталов.
            fractal.steps = RecursionTrackBar.Value;
            fractal.gradientSteps = fractal.steps;
            if (LengthCheckBox.Checked)
            {
                int value = Math.Min(Size.Width, Size.Height) - 300;
                fractal.length = value;
                LengthLabel.Text = String.Format($"Длина: {value}");
                if (value > LengthTrackBar1.Maximum)
                    LengthTrackBar1.Value = LengthTrackBar1.Maximum;
                else if (value < LengthTrackBar1.Minimum)
                    LengthTrackBar1.Value = LengthTrackBar1.Minimum;
                else
                    LengthTrackBar1.Value = value;
            }
            else
                fractal.length = LengthTrackBar1.Value;

            // Рисование фрактала.
            float x = (Size.Width - fractal.length - 300) / 2;
            float y = Size.Height - 300;
            int currentStep = 0;
            double angle = 0;
            PointF point1 = new PointF(0, 0);
            PointF point2 = new PointF(0, 0);
            DrawFractal(fractal, x, y, currentStep, angle, point1, point2);
        }

        // Нажатие кнопки "Множество Кантора"
        private void ButtonCantor_Click(object sender, EventArgs e)
        {
            // Вывод текста, создание переменных и графики.
            InfoTextBox.Text = ButtonCantor.Text;
            currentFractal = "Cantor";
            var graphics = GetGraphics();
            graphics.Clear(Color.FromArgb(25, 25, 26));

            // Создание фрактала и графики.
            var fractal = new CantorFractal();
            fractal.graphics = graphics;
            fractal.windowHeight = CenterPanel.Height;
            fractal.colorBegin = GetColor(ColorBoxBegin.Text);
            fractal.colorEnd = GetColor(ColorBoxEnd.Text);

            // Ввод переменных общих для всех фракталов.
            fractal.steps = RecursionTrackBar.Value;
            fractal.gradientSteps = fractal.steps;
            if (LengthCheckBox.Checked)
            {
                int value = Size.Width - 420;
                fractal.length = value;
                LengthLabel.Text = String.Format($"Длина: {value}");
                if (value > LengthTrackBar1.Maximum)
                    LengthTrackBar1.Value = LengthTrackBar1.Maximum;
                else if (value < LengthTrackBar1.Minimum)
                    LengthTrackBar1.Value = LengthTrackBar1.Minimum;
                else
                    LengthTrackBar1.Value = value;
            }
            else
                fractal.length = LengthTrackBar1.Value;

            // Ввод переменных специфических для данного фрактала.
            fractal.delta = DeltaTrackBar.Value;

            // Рисование фрактала.
            float x = 50;
            float y = 150;
            int currentStep = 0;
            double angle = 0;
            PointF point1 = new PointF(0, 0);
            PointF point2 = new PointF(0, 0);
            DrawFractal(fractal, x, y, currentStep, angle, point1, point2);
        }

        //
        // Трэк бары.
        //

        // Общий трэк-бар рекурсии фрактала.
        private void RecursionTrackBar_Scroll(object sender, EventArgs e)
        {
            RecursionLabel.Text = String.Format($"Рекурсия: {RecursionTrackBar.Value}");
            RefreshGraphics(sender, e);
        }

        // Общий трэк-бар длины фрактала.
        private void LengthTrackBar1_Scroll(object sender, EventArgs e)
        {
            LengthLabel.Text = String.Format($"Длина: {LengthTrackBar1.Value}");
            RefreshGraphics(sender, e);
        }

        // Общий трэк-бар начального цвета фрактала.
        private void ColorBoxBegin_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGraphics(sender, e);
        }

        // Общий трэк-бар конечного цвета фрактала.
        private void ColorBoxEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGraphics(sender, e);
        }

        // Пифагоровский трэк-бар левого угла фрактала.
        private void LeftAngleTrackBar_Scroll(object sender, EventArgs e)
        {
            LeftAngleLabel.Text = String.Format($"Угол влево: {LeftAngleTrackBar.Value}");
            if (currentFractal == "Pythagorean")
                RefreshGraphics(sender, e);
        }

        // Пифагоровский трэк-бар правого угла фрактала.
        private void RightAngleTrackBar_Scroll(object sender, EventArgs e)
        {
            RightAngleLabel.Text = String.Format($"Угол вправо: {RightAngleTrackBar.Value}");
            if (currentFractal == "Pythagorean")
                RefreshGraphics(sender, e);
        }

        // Пифагоровский трэк-бар коэффицента уменьшения длины фрактала.
        private void CoeffTrackBar_Scroll(object sender, EventArgs e)
        {
            CoeffLabel.Text = String.Format($"Коэффициент: {(CoeffTrackBar.Value / 100.0):f2}");
            if (currentFractal == "Pythagorean")
                RefreshGraphics(sender, e);
        }

        // Канторовский трэк-бар дельта-коэффицента фрактала.
        private void DeltaTrackBar_Scroll(object sender, EventArgs e)
        {
            DeltaLabel.Text = String.Format($"Дельта: {DeltaTrackBar.Value}");
            if (currentFractal == "Cantor")
                RefreshGraphics(sender, e);
        }

        // Трэк-бар увелечения фрактала.
        private void ScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            ScaleLabel.Text = String.Format($"Увеличение: х{ScaleTrackBar.Value}");
            RefreshGraphics(sender, e);
        }

        // Трэк-бар смещения по х.
        private void XOffsetTrackBar_Scroll(object sender, EventArgs e)
        {
            XOffsetLabel.Text = String.Format($"Смещение Х: {XOffsetTrackBar.Value}");
            RefreshGraphics(sender, e);
        }

        // Трэк-бар смещения по y.
        private void YOffsetTrackBar_Scroll(object sender, EventArgs e)
        {
            YOffsetLabel.Text = String.Format($"Смещение Y: {YOffsetTrackBar.Value}");
            RefreshGraphics(sender, e);
        }

        // Сохранения фрактала.
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            // Ввод панели для скриншота.
            var panel = CenterPanel;

            // Ввод переменных для создания скриншота.
            int width = panel.Size.Width;
            int height = panel.Size.Height;
            Graphics panelGraphics = panel.CreateGraphics();
            Bitmap screenshot = new Bitmap(width, height, panelGraphics);
            Graphics graphics = Graphics.FromImage(screenshot);

            // Создание скриншота.
            int x = panel.Location.X + this.Location.X + 10;
            int y = panel.Location.Y + this.Location.Y + 30;
            var screenshotSize = new Size(width, height);
            graphics.CopyFromScreen(x, y, 0, 0, screenshotSize);

            // Сохранение скриншота.
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPEG Image (.jpeg)|*.jpeg";
            save.Filter += "|Png Image(.png) | *.png";
            save.Filter += "|Bitmap Image (.bmp)|*.bmp";
            save.ShowDialog();
            screenshot.Save(save.FileName);
        }

        //
        // Вспомогательные функции.
        //

        // Расшифровка цвета.
        public Color GetColor(string input)
        {
            switch (input)
            {
                case "Белый":
                    return Color.White;
                case "Синий":
                    return Color.FromArgb(51, 102, 255);
                case "Зеленый":
                    return Color.FromArgb(0, 204, 153);
                case "Красный":
                    return Color.FromArgb(255, 51, 102);
                case "Фиолетовый":
                    return Color.FromArgb(153, 0, 255);
                case "Желтый":
                    return Color.FromArgb(204, 204, 51);
                case "Оранжевый":
                    return Color.FromArgb(255, 153, 51);
                case "Серый":
                    return Color.Gray;
                case "Невидимый":
                    return Color.FromArgb(25, 25, 26);
                default:
                    return Color.White;
            }
        }

        // Авто-длина для дерева Пифагора.
        public double GetLengthRatio(int input)
        {
            // {x = 1.25, y = 4.5}
            // {x = 5.00, y = 1.4}
            double x = input / 100.0;
            return 2 / (x - 0.65) + 0.95;
        }
    
        // Создание области графики.
        public Graphics GetGraphics()
        {
            var graphics = CenterPanel.CreateGraphics();
            graphics.ScaleTransform(ScaleTrackBar.Value, ScaleTrackBar.Value);
            return graphics;
        }
    
        // Рисование фрактала.
        public void DrawFractal(Fractal fractal, float x, float y, int currentStep, double angle, PointF point1, PointF point2)
        {
            fractal.Draw(x + XOffsetTrackBar.Value, y + YOffsetTrackBar.Value, currentStep, angle, point1, point2);
        }
    }
}

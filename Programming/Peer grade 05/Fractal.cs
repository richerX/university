using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Fractals
{
    // Фрактал.
    public abstract class Fractal
    {
        public Graphics graphics;
        public int length;
        public int steps;
        public int gradientSteps;
        public int windowHeight;
        public Color colorBegin;
        public Color colorEnd;
        public PointF pointOne = new PointF(0, 0);
        public PointF pointTwo = new PointF(0, 0);

        // Создание градиента.
        public Color GetGradient(int index)
        {
            if (gradientSteps == 1)
                return colorBegin;
            var red = colorBegin.R + (colorEnd.R - colorBegin.R) * index / (gradientSteps - 1);
            var blue = colorBegin.B + (colorEnd.B - colorBegin.B) * index / (gradientSteps - 1);
            var green = colorBegin.G + (colorEnd.G - colorBegin.G) * index / (gradientSteps - 1);
            return Color.FromArgb(red, green, blue);
        }

        // Рисование фрактала.
        public abstract void Draw(float x, float y, int currentStep, double angle, PointF point1, PointF point2);
    }

    // Дерево Пифагора.
    class PythagoreanFractal : Fractal
    {
        // Ввод переменных класса.
        public double angleRight;
        public double angleLeft;
        public double lengthCoefficient;

        // Рекурсивная функция рисования фрактала.
        public override void Draw(float x, float y, int currentStep, double angle, PointF point1, PointF point2)
        {
            if (currentStep < steps)
            {
                int newLength = (int)(length / Math.Pow(lengthCoefficient, currentStep));
                int x1 = (int)(x + newLength * Math.Sin(angle * Math.PI * 2 / 360.0));
                int y1 = (int)(y + newLength * Math.Cos(angle * Math.PI * 2 / 360.0));
                var color = GetGradient(currentStep);
                graphics.DrawLine(new Pen(color), x, windowHeight - y, x1, windowHeight - y1);
                Draw(x1, y1, currentStep + 1, angle + angleRight, point1, point2);
                Draw(x1, y1, currentStep + 1, angle - angleLeft, point1, point2);
            }
        }
    }

    // Кривая Коха.
    class KochFractal : Fractal
    {
        public int colorCount = 0;

        // Рекурсивная функция рисования фрактала.
        public override void Draw(float x, float y, int currentStep, double angle, PointF point1, PointF point2)
        {
            if (currentStep < steps)
            {
                // Ввод нужных точек
                var left = new PointF((2 * point1.X + point2.X) / 3, (2 * point1.Y + point2.Y) / 3);
                var right = new PointF((2 * point2.X + point1.X) / 3, (2 * point2.Y + point1.Y) / 3);
                var apex = GetApex(left, right);

                // Рисование фрактала
                if (currentStep == steps - 1)
                {
                    var color = GetGradient(colorCount);
                    graphics.DrawLine(new Pen(color), point1, left);
                    graphics.DrawLine(new Pen(color), point2, right);
                    graphics.DrawLine(new Pen(color), left, apex);
                    graphics.DrawLine(new Pen(color), right, apex);
                    colorCount += 1;
                }

                // Рекурсивный вызов функции
                Draw(x, y, currentStep + 1, angle, point1, left);
                Draw(x, y, currentStep + 1, angle, left, apex);
                Draw(x, y, currentStep + 1, angle, apex, right);
                Draw(x, y, currentStep + 1, angle, right, point2);
            }
        }

        public PointF GetApex(PointF point1, PointF point2)
        {
            double sine = -Math.Sqrt(3) / 2;
            double cosine = 0.5;
            double turnedX = (point2.X - point1.X) * cosine - (point2.Y - point1.Y) * sine;
            double turnedY = (point2.X - point1.X) * sine + (point2.Y - point1.Y) * cosine;
            return new PointF((float)(point1.X + turnedX), (float)(point1.Y + turnedY));
        }
    }

    // Ковер Серпинского.
    class SierpinskiFractal : Fractal
    {
        // Рекурсивная функция рисования фрактала.
        public override void Draw(float x, float y, int currentStep, double angle, PointF point1, PointF point2)
        {
            if (currentStep < steps)
            {
                int newLength = (int)(this.length / Math.Pow(3, currentStep));

                // Рисование квадрата.
                int specialX = (int)(x + newLength / 3);
                int specialY = (int)(y + newLength / 3);
                var color = GetGradient(currentStep);
                graphics.FillRectangle(new SolidBrush(color), specialX, specialY, newLength / 3, newLength / 3);

                // Рекурсивный вызов функции.
                int newX, newY;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i != 1 || j != 1)
                        {
                            newX = (int)(x + newLength * i / 3);
                            newY = (int)(y + newLength * j / 3);
                            Draw(newX, newY, currentStep + 1, angle, point1, point2);
                        }
                    }
                }
            }
        }
    }

    // Треугольник Серпинского.
    class Sierpinski2Fractal : Fractal
    {   
        // Рисование изначального треугольника.
        public void InitialDraw(float x, float y)
        {
            var left = new PointF(x, y);
            var right = new PointF(x + length, y);
            var apex = GetUpperApex(left, right);
            var color = GetGradient(0);
            graphics.DrawLine(new Pen(color), left, right);
            graphics.DrawLine(new Pen(color), left, apex);
            graphics.DrawLine(new Pen(color), right, apex);
        }

        // Рекурсивная функция рисования фрактала.
        public override void Draw(float x, float y, int currentStep, double angle, PointF point1, PointF point2)
        {
            if (currentStep < steps)
            {
                // Рисование треугольника
                if (currentStep == 0)
                    InitialDraw(x, y);
                float newLength = (float)(length / Math.Pow(2, currentStep));
                var left = GetUpperApex(new PointF(x, y), new PointF(x + newLength / 2, y));
                var right = GetUpperApex(new PointF(x + newLength / 2, y), new PointF(x + newLength, y));
                var apex = GetDownApex(left, right);
                var color = GetGradient(currentStep);
                graphics.DrawLine(new Pen(color), left, right);
                graphics.DrawLine(new Pen(color), left, apex);
                graphics.DrawLine(new Pen(color), right, apex);

                // Рекурсивный вызов
                Draw(x, y, currentStep + 1, angle, point1, point2);
                Draw(left.X, left.Y, currentStep + 1, angle, point1, point2);
                Draw(x + newLength / 2, y, currentStep + 1, angle, point1, point2);
            }
        }
    
        public PointF GetDownApex(PointF left, PointF right)
        {
            float newX = left.X + (right.X - left.X) / 2;
            float newY = (float)(left.Y + Math.Sqrt(3) * (right.X - left.X) / 2);
            return new PointF(newX, newY);
        }

        public PointF GetUpperApex(PointF left, PointF right)
        {
            float newX = left.X + (right.X - left.X) / 2;
            float newY = (float)(left.Y - Math.Sqrt(3) * (right.X - left.X) / 2);
            return new PointF(newX, newY);
        }
    }

    // Множество Кантора.
    class CantorFractal : Fractal
    {

        public int delta;
        public int height = 30;

        // Рекурсивная функция рисования фрактала.
        public override void Draw(float x, float y, int currentStep, double angle, PointF point1, PointF point2)
        {
            if (currentStep < steps)
            {
                // Рисование отрезка
                int newLength = (int)(length / Math.Pow(3, currentStep));
                var color = GetGradient(currentStep);
                graphics.FillRectangle(new SolidBrush(color), x, y, newLength, height);

                // Рекурсивный вызов
                Draw(x, y + delta + height, currentStep + 1, angle, point1, point2);
                Draw(x + newLength * 2 / 3, y + delta + height, currentStep + 1, angle, point1, point2);
            }
        }
    }
}



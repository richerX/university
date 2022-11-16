using System;
using System.IO;
using System.Text;

namespace Matrix_Calculator
{
    class Program
    {
        /*
         * Далее расположены все дополнительные функции,
         * помогающие в создании матрицы n * m.
         */

        // Считывание или создание матрицы n * m.
        public static double[,] ReadMatrix(int height, int width)
        {
            // Ввод переменных, вывод сообщения.
            var matrix = new double[height, width];
            Console.WriteLine($"МАТРИЦА {height} * {width}");
            Console.WriteLine("1. Считывание из консоли");
            Console.WriteLine("2. Рандомная генерация");
            Console.WriteLine("3. Считывание из файла");
            int userInput;
            do
            {
                Console.Write("Введите номер операции: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 3 || userInput < 1);
            Console.WriteLine();
            // Методы создания/считывания матрицы.
            switch (userInput)
            {
                case 1:
                    matrix = ConsoleMatrix(height, width);
                    break;
                case 2:
                    matrix = RandomMatrix(height, width);
                    break;
                case 3:
                    matrix = FileMatrix(height, width);
                    break;
            }
            Console.WriteLine();
            return matrix;
        }

        // Считывание матрицы из консоли n * m.
        public static double[,] ConsoleMatrix(int height, int width)
        {
            // Ввод переменных, вывод сообщения.
            Console.WriteLine("Числа необходимо вводить через пробел");
            Console.WriteLine($"Длина строки (ширина матрицы) = {width}");
            var matrix = new double[height, width];
            var array = new double[width];
            // Считывание height строк матрицы, каждая длины width.
            for (int i = 0; i < height; i++)
            {
                while (true)
                {
                    Console.Write($"Введите {i + 1} строку: ");
                    try
                    {
                        array = Array.ConvertAll(Console.ReadLine().Trim().Split(), Double.Parse);
                        if (array.Length == width)
                            break;
                    }
                    catch {;}
                }
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = array[j];
                }
            }
            return matrix;
        }

        // Создание рандомной матрицы n * m.
        public static double[,] RandomMatrix(int height, int width)
        {
            // Ввод переменных.
            double minimum, maximum;
            do
            {
                Console.Write("Введите минимальное  значение: ");
            } while (!double.TryParse(Console.ReadLine(), out minimum));
            do
            {
                Console.Write("Введите максимальное значение: ");
            } while (!double.TryParse(Console.ReadLine(), out maximum) || maximum < minimum);
            // Создание матрицы с рандомными значениями.
            var random = new Random();
            double delta = maximum - minimum;
            var matrix = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = minimum + delta * random.NextDouble();
                }
            }
            return matrix;
        }

        // Считывание матрицы из файла n * m.
        public static double[,] FileMatrix(int height, int width)
        {
            // Ввод переменных.
            var matrix = new double[height, width];
            var array = new double[width];
            var lines = new string[height];
            string line;
            string path;
            bool isCorrect;
            // D:\test.txt
            while (true)
            {
                isCorrect = true;
                // Ввод пути к файлу.
                do
                {
                    Console.Write("Введите полный путь к файлу: ");
                    path = Console.ReadLine();
                } while (!File.Exists(path));
                // Считывание всех строк файла.
                try
                {
                    lines = File.ReadAllLines(path);
                }
                catch
                {
                    Console.WriteLine("Неверное количество строк в файле");
                    continue;
                }
                // Разбор каждой строки файла.
                for (int i = 0; i < height; i++)
                {
                    line = lines[i];
                    try
                    {
                        array = Array.ConvertAll(line.Trim().Split(), Double.Parse);
                        if (array.Length != width)
                        {
                            Console.WriteLine($"Ошибка в строке #{i + 1}");
                            isCorrect = false;
                            break;
                        }
                        for (int j = 0; j < width; j++)
                        {
                            matrix[i, j] = array[j];
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Ошибка в файле");
                        isCorrect = false;
                        break;
                    }
                }
                // isCorrect = true, если весь процесс прошел корректно.
                if (isCorrect)
                    break;
            }
            return matrix;
        }

        /*
         * Далее расположены все дополнительные функции,
         * создающие различные наборы матриц.
         */

        // Получить матрицу.
        public static double[,] GetMatrix()
        {
            // Ввод переменных.
            int height, width;
            do
            {
                Console.Write("Введите высоту матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out height) || height < 1);
            do
            {
                Console.Write("Введите ширину матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out width) || width < 1);
            Console.WriteLine();
            return ReadMatrix(height, width);
        }

        // Получить квадратную матрицу.
        public static double[,] GetQuadraticMatrix()
        {
            // Ввод переменных.
            int height;
            do
            {
                Console.Write("Введите размерность квадратной матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out height) || height < 1);
            Console.WriteLine();
            return ReadMatrix(height, height);
        }

        // Получить две матрицы одинакового размера.
        public static Tuple<double[,], double[,]> GetTwoSameMatrixes()
        {
            // Ввод переменных.
            int height, width;
            do
            {
                Console.Write("Введите высоту матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out height) || height < 1);
            do
            {
                Console.Write("Введите ширину матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out width) || width < 1);
            Console.WriteLine();
            return Tuple.Create(ReadMatrix(height, width), ReadMatrix(height, width));
        }

        // Получить две матрицы для умножения.
        public static Tuple<double[,], double[,]> GetTwoMultiMatrixes()
        {
            // Ввод переменных.
            // Высота второй матрицы будет равна ширине первой.
            int height, width, width2;
            do
            {
                Console.Write("Введите высоту первой матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out height) || height < 1);
            do
            {
                Console.Write("Введите ширину первой матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out width) || width < 1);
            do
            {
                Console.Write("Введите ширину второй матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out width2) || width2 < 1);
            Console.WriteLine();
            return Tuple.Create(ReadMatrix(height, width), ReadMatrix(width, width2));
        }

        // Получить две матрицы для решения СЛАУ.
        public static Tuple<double[,], double[,]> GetSystemMatrixes()
        {
            // Ввод переменных.
            int dimension;
            do
            {
                Console.Write("Введите размерность матрицы: ");
            } while (!int.TryParse(Console.ReadLine(), out dimension) || dimension < 1);
            Console.WriteLine();
            return Tuple.Create(ReadMatrix(dimension, dimension), ReadMatrix(dimension, 1));
        }

        // Вывод матрицы.
        public static void DisplayMatrix(double[,] matrix, string message)
        {
            // Размерность матрицы.
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            Console.WriteLine(message);
            // Вывод каждого элемента матрицы.
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    Console.Write($"{matrix[i, j],-8:f2} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /*
         * Далее расположены все основные операции.
         */

        // Операция #1 - след матрицы.
        public static void FindTrace()
        {
            // Создание необходимой матрицы.
            var matrix = GetQuadraticMatrix();
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            double answer = 0;
            // Подсчёт следа матрицы.
            for (int i = 0; i < matrixHeight; i++)
            {
                answer += matrix[i, i];
            }
            DisplayMatrix(matrix, "МАТРИЦА");
            Console.WriteLine($"След матрицы = {answer,1:f2}");
            Console.WriteLine();
        }

        // Операция #2 - транспонирование матрицы.
        public static void Transpose()
        {
            // Создание необходимой матрицы.
            var matrix = GetMatrix();
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            // Создание транспонированной матрицы.
            var newMatrix = new double[matrixWidth, matrixHeight];
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    newMatrix[j, i] = matrix[i, j];
                }
            }
            DisplayMatrix(matrix, "ИЗНАЧАЛЬАНАЯ МАТРИЦА");
            DisplayMatrix(newMatrix, "ТРАНСПОНИРОВАННАЯ МАТРИЦА");
        }

        // Операция #3 - сумма двух матриц.
        public static void Sum()
        {
            // Создание необходимых матриц.
            var twoMatrixes = GetTwoSameMatrixes();
            var matrixOne = twoMatrixes.Item1;
            var matrixTwo = twoMatrixes.Item2;
            int matrixHeight = matrixOne.GetLength(0);
            int matrixWidth = matrixOne.GetLength(1);
            var newMatrix = new double[matrixHeight, matrixWidth];
            // Подсчёт суммы матриц.
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    newMatrix[i, j] = matrixOne[i, j] + matrixTwo[i, j];
                }
            }
            DisplayMatrix(matrixOne, "ПЕРВАЯ МАТРИЦА");
            DisplayMatrix(matrixTwo, "ВТОРАЯ МАТРИЦА");
            DisplayMatrix(newMatrix, "СУММА МАТРИЦ");

        }

        // Операция #4 - разность двух матриц.
        public static void Subtraction()
        {
            // Создание необходимой матрицы.
            var twoMatrixes = GetTwoSameMatrixes();
            var matrixOne = twoMatrixes.Item1;
            var matrixTwo = twoMatrixes.Item2;
            int matrixHeight = matrixOne.GetLength(0);
            int matrixWidth = matrixOne.GetLength(1);
            var newMatrix = new double[matrixHeight, matrixWidth];
            // Подсчёт разности матриц.
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    newMatrix[i, j] = matrixOne[i, j] - matrixTwo[i, j];
                }
            }
            DisplayMatrix(matrixOne, "ПЕРВАЯ МАТРИЦА");
            DisplayMatrix(matrixTwo, "ВТОРАЯ МАТРИЦА");
            DisplayMatrix(newMatrix, "РАЗНОСТЬ МАТРИЦ");

        }

        // Операция #5 - умножение матриц.
        public static void Multiplication()
        {
            // Создание необходимой матрицы.
            var twoMatrixes = GetTwoMultiMatrixes();
            var matrixOne = twoMatrixes.Item1;
            int matrixOneHeight = matrixOne.GetLength(0);
            int matrixOneWidth = matrixOne.GetLength(1);
            var matrixTwo = twoMatrixes.Item2;
            int matrixTwoHeight = matrixTwo.GetLength(0);
            int matrixTwoWidth = matrixTwo.GetLength(1);
            // Подсчёт произведения матриц.
            var newMatrix = new double[matrixOneHeight, matrixTwoWidth];
            double value = 0;
            for (int i = 0; i < matrixOneHeight; i++)
            {
                for (int j = 0; j < matrixTwoWidth; j++)
                {
                    value = 0;
                    for (int k = 0; k < matrixOneWidth; k++)
                    {
                        value += matrixOne[i, k] * matrixTwo[k, j];
                    }
                    newMatrix[i, j] = value;
                }
            }
            DisplayMatrix(matrixOne, "ПЕРВАЯ МАТРИЦА");
            DisplayMatrix(matrixTwo, "ВТОРАЯ МАТРИЦА");
            DisplayMatrix(newMatrix, "ПРОИЗВЕДЕНИЕ МАТРИЦ");
        }

        // Операция #6 - умножение матрицы на число.
        public static void ValueMultiplication()
        {
            // Создание необходимой матрицы.
            var matrix = GetMatrix();
            int matrixHeight = matrix.GetLength(0);
            int matrixWidth = matrix.GetLength(1);
            var newMatrix = new double[matrixHeight, matrixWidth];
            // Ввод числа.
            double value;
            do
            {
                Console.Write("Введите число, на которое надо умножить матрицу: ");
            } while (!double.TryParse(Console.ReadLine(), out value));
            Console.WriteLine();
            // Подсчёт произведения.
            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    newMatrix[i, j] = matrix[i, j] * value;
                }
            }
            DisplayMatrix(matrix, "ИЗНАЧАЛЬАНАЯ МАТРИЦА");
            DisplayMatrix(newMatrix, "УМНОЖЕННАЯ МАТРИЦА");
        }

        // Операция #7 - умножение матрицы на число.
        public static void Determinant()
        {
            // Создание необходимой матрицы.
            var matrix = GetQuadraticMatrix();
            DisplayMatrix(matrix, "ИЗНАЧАЛЬАНАЯ МАТРИЦА");
            // Подсчёт определителя матрицы.
            Console.WriteLine($"Определитель матрицы = {CalculateDeterminant(matrix)}");
            Console.WriteLine();
        }
        
        // Операция #7.1 - рекурсивное вычисление определителя
        // матрицы с помощью разложения по первой строке
        public static double CalculateDeterminant(double[,] matrix)
        {
            // Ввод переменных.
            double value = 0;
            int dimension = matrix.GetLength(0);
            // Если размерность матрицы равна 1,
            // то её определетиль равен ее элементу.
            if (dimension == 1)
                return matrix[0, 0];
            // Разложение матрицы по первой строке.
            for (int column = 0; column < dimension; column++)
            {
                value += Math.Pow(-1, column) * matrix[0, column] * CalculateDeterminant(Minor(matrix, 0, column));
            }
            return value;
        }

        // Операция #7.2 - минор матрицы
        public static double[,] Minor(double[,] matrix, int row, int column)
        {
            // Ввод переменных.
            int dimension = matrix.GetLength(0);
            var minor = new double[dimension - 1, dimension - 1];
            int minorRow = 0, minorColumn = 0;
            // Создания минора матрицы, без строки 'row' и столбца 'column'.
            // Алгоритм пробегается по изначальной матрицы
            // и пропускает все элементы в нужной строке и столбце.
            for (int matrixRow = 0; matrixRow < dimension; matrixRow++)
            {
                minorColumn = 0;
                for (int matrixColumn = 0; matrixColumn < dimension; matrixColumn++)
                {
                    if (matrixRow == row || matrixColumn == column)
                        continue;
                    minor[minorRow, minorColumn] = matrix[matrixRow, matrixColumn];
                    minorColumn += 1;
                }
                if (matrixRow != row)
                    minorRow += 1;
            }
            return minor;
        }

        // Операция #8 - решить СЛАУ.
        public static void SolveSystem()
        {
            // Создание необходимой матрицы.
            var twoMatrixes = GetSystemMatrixes();
            var matrix = twoMatrixes.Item1;
            var column = twoMatrixes.Item2;
            int dimension = matrix.GetLength(0);
            DisplayMatrix(matrix, "ИЗНАЧАЛЬАНАЯ МАТРИЦА");
            DisplayMatrix(column, "ИЗНАЧАЛЬНЫЕ ОТВЕТЫ");
            // Метод Гаусса.
            double ratio;
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    ratio = matrix[j, i] / matrix[i, i];
                    if (i != j && ratio != 0)
                    {
                        for (int k = 0; k < dimension; k++)
                        {
                            matrix[j, k] -= ratio * matrix[i, k];
                        }
                        column[j, 0] -= ratio * column[i, 0];
                    }
                }
            }
            // Нормирвание.
            for (int i = 0; i < dimension; i++)
            {
                if (matrix[i, i] != 0)
                {
                    column[i, 0] /= matrix[i, i];
                    matrix[i, i] = 1;
                }
            }
            DisplayMatrix(matrix, "ИЗМЕНЕННАЯ МАТРИЦА");
            DisplayMatrix(column, "РЕШЕНИЕ СЛАУ");
        }

        /*
         * Далее расположен основной блок программы,
         * обеспечивающий взаимодействие с пользователем.
         */

        static void Main(string[] args)
        {
            // Ввод переменных.
            int userInput;
            string operationsFilePath = "Operations.txt";
            while (true)
            {
                // ВЫбор операции.
                Console.WriteLine(File.ReadAllText(operationsFilePath, Encoding.UTF8));
                do
                {
                    Console.Write("Введите номер операции: ");
                } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 9 || userInput < 1);
                Console.WriteLine();
                // Разбор всех операций.
                try
                {
                    switch (userInput)
                    {
                        // Нахождение следа матрицы.
                        case 1:
                            FindTrace();
                            break;

                        // Транспонирование матрицы.
                        case 2:
                            Transpose();
                            break;

                        // Сумма двух матриц.
                        case 3:
                            Sum();
                            break;

                        // Разность двух матриц.
                        case 4:
                            Subtraction();
                            break;

                        // Произведение двух матриц.
                        case 5:
                            Multiplication();
                            break;

                        // Умножение матрицы на число.
                        case 6:
                            ValueMultiplication();
                            break;

                        // Нахождение определителя матрицы.
                        case 7:
                            Determinant();
                            break;

                        // Решение СЛАУ.
                        case 8:
                            SolveSystem();
                            break;

                        // Выйти из программы.
                        case 9:
                            Console.WriteLine("Спасибо за использование моей программы! Удачи и хорошей проверки!");
                            Console.WriteLine();
                            Console.Write("Для продолжения нажмите любую клавишу...");
                            Console.ReadLine();
                            return;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Упс, что-то пошло не так (O_o)");
                    Console.WriteLine();
                    Console.WriteLine("КОД ОШИБКИ");
                    Console.WriteLine(exception);
                    Console.WriteLine();
                }
                // Завершающие циклическое сообщение.
                Console.Write("Для продолжения нажмите любую клавишу...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}

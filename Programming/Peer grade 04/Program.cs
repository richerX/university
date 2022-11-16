using System;
using System.Collections.Generic;
using System.IO;

namespace Storage
{
    class Program
    {
        /* 
         * Тут расположены дополнительные функции,
         * помогающие работе программы.
         */
        
        // Считать целое положительное число.
        public static int ReadPositiveInt(string message)
        {
            int userInput;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput <= 0);
            return userInput;
        }

        // Считать положительное число.
        public static double ReadPositiveDouble(string message)
        {
            double userInput;
            do
            {
                Console.Write(message);
            } while (!double.TryParse(Console.ReadLine(), out userInput) || userInput <= 0);
            return userInput;
        }



        /* 
         * Тут расположены функции, отвечающие
         * за работу с консолью.
         */

        // Основная функции работы со складом через консоль.
        public static void ConsoleMain()
        {
            // Основной цикл программы.
            while (true)
            {
                // Создание склада.
                int capacity = ReadPositiveInt("Введите вместимость склада: ");
                double pricePerContainer = ReadPositiveDouble("Введите цену хранения одного контейнера: ");
                var warehouse = new Warehouse(capacity, pricePerContainer);
                int userInput;
                Console.Clear();
                // Работа со складом.
                while (true)
                {
                    // ВЫбор операции.
                    Console.WriteLine(warehouse);
                    Console.WriteLine();
                    Console.WriteLine("1. Добавить контейнер.");
                    Console.WriteLine("2. Удалить контейнер.");
                    Console.WriteLine("3. Вывести подробную информацию о складе.");
                    Console.WriteLine("4. Добавить рандомный контейнер.");
                    Console.WriteLine("5. Завершить работу с этим складом.");
                    do
                    {
                        Console.Write("Введите номер операции: ");
                    } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 5 || userInput < 1);
                    Console.WriteLine();
                    // Разбор всех операций.
                    switch (userInput)
                    {
                        case 1:
                            ConsoleAddContainer(ref warehouse);
                            break;
                        case 2:
                            ConsoleDeleteContainer(ref warehouse);
                            break;
                        case 3:
                            ConsoleWarehouseInfo(warehouse);
                            break;
                        case 4:
                            ConsoleAddRandomContainer(ref warehouse);
                            break;
                        case 5:
                            ConsoleWarehouseInfo(warehouse);
                            Console.WriteLine();
                            return;
                    }
                    Console.Write("Нажмите ENTER для продолжения...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        // Функции добавления контейнера через консоль.
        public static void ConsoleAddContainer(ref Warehouse warehouse)
        {
            // Ввод переменных.
            var boxes = new List<Box>();
            int numberOfBoxes = ReadPositiveInt("Введите количество ящиков в контейнере: ");
            Console.WriteLine();
            double weight, pricePerKilo;
            // Считывание ящиков.
            for (int i = 0; i < numberOfBoxes; i++)
            {
                weight = ReadPositiveDouble($"Введите вес ящика №{i + 1}: ");
                pricePerKilo = ReadPositiveDouble($"Введите цену за килограмм ящика №{i + 1}: ");
                boxes.Add(new Box(weight, pricePerKilo));
                Console.WriteLine();
            }
            // Добавление контейнера.
            if (warehouse.AddContainer(new Container(boxes)))
            {
                Console.WriteLine("Контейнер был успешно добавлен.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Контейнер не был добавлен, так как это не является рентабельным.");
                Console.WriteLine();
            }
        }

        // Функции удаления контейнера через консоль.
        public static void ConsoleDeleteContainer(ref Warehouse warehouse)
        {
            // Ввод переменных.
            int userInput;
            if (warehouse.GetContainers().Count == 0)
            {
                Console.WriteLine("Склад пуст");
                Console.WriteLine();
            }
            // Удаление контейнера.
            else
            {
                warehouse.GetInfo();
                do
                {
                    Console.Write("Введите номер контейнера: ");
                } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > warehouse.GetContainers().Count || userInput < 1);
                warehouse.DeleteContainer(userInput - 1);
                Console.WriteLine();
                Console.WriteLine("Контейнер был успешно удалён.");
                Console.WriteLine();
            }
        }

        // Функции вывода информации о складе через консоль.
        public static void ConsoleWarehouseInfo(Warehouse warehouse)
        {
            Console.WriteLine(warehouse);
            var containers = warehouse.GetContainers();
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"      |");
                Console.WriteLine($"      |---------> №{i + 1} {containers[i]}");
                var boxes = containers[i].GetBoxes();
                for (int j = 0; j < boxes.Count; j++)
                {
                    Console.WriteLine($"      |         |");
                    Console.WriteLine($"      |         |---------> №{j + 1} {boxes[j]}");
                }
                Console.WriteLine($"      |");
            }
            Console.WriteLine();
        }

        // Функции добавления рандомного контейнера через консоль.
        public static void ConsoleAddRandomContainer(ref Warehouse warehouse)
        {
            // Ввод переменных.
            var random = new Random();
            var boxes = new List<Box>();
            int numberOfBoxes = random.Next(2, 10);
            double weight, pricePerKilo;
            // Создание рандомного контейнера.
            for (int i = 0; i < numberOfBoxes; i++)
            {
                weight = random.Next(2, 10);
                pricePerKilo = random.Next(1, 100);
                boxes.Add(new Box(weight, pricePerKilo));
            }
            // Добавление рандомного контейнера.
            if (warehouse.AddContainer(new Container(boxes)))
            {
                Console.WriteLine("Контейнер был успешно добавлен.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Контейнер не был добавлен, так как это не является рентабельным.");
                Console.WriteLine();
            }
        }



        /* 
         * Тут расположены функции, отвечающие
         * за работу с файлами.
         */

        // Основная функции работы со складом через файлы.
        public static void FileMain()
        {
            // Приветственное сообщение.
            Console.WriteLine(@"Все файлы должны находиться в папке ...\bin\Debug\netcoreapp3.1\warehouse");
            Console.WriteLine("В файле warehouse.txt находится информация о складе - capacity и pricePerContainer в двух разных строках.");
            Console.WriteLine("В файле containers.txt находится информация о контейнерах - пример смотри в файле.");
            Console.WriteLine("В файле operations.txt находится информация об операциях - пример смотри в файле.");
            Console.WriteLine("В конце каждого файла обязатльно должна быть пустая строка.");
            Console.WriteLine();
            Console.Write("Нажмите ENTER когда файлы будут готовы...");
            Console.ReadLine();
            Console.Clear();

            // Считывание файла warehouse.txt.
            int capacity = 0;
            double pricePerContainer = 0;
            bool flagWarehouse = ReadWarehouseTxt(ref capacity, ref pricePerContainer);
            // Считывание файла containers.txt.
            var containers = new List<Container>();
            bool flagContainers = ReadContainersTxt(ref containers);
            // Считывание файла operations.txt.
            var operations = new List<string[]>();
            bool flagOperations = ReadOperationsTxt(ref operations);
            // Проверка файлов на ошибки.
            if (!flagWarehouse || !flagContainers || !flagOperations)
            {
                if (!flagWarehouse)
                    Console.WriteLine("Ошибка в файле warehouse.txt");
                if (!flagContainers)
                    Console.WriteLine("Ошибка в файле containers.txt");
                if (!flagOperations)
                    Console.WriteLine("Ошибка в файле operations.txt");
                Console.WriteLine();
                Console.Write("Нажмите ENTER для продолжения...");
                return;
            }
            // Выполнить все действия из файла operations.txt.
            var warehouse = new Warehouse(capacity, pricePerContainer);
            if (!EvaluateOperations(ref warehouse, containers, operations))
            {
                Console.WriteLine("Ошибка при исполнении containers.txt");
                Console.WriteLine();
                Console.Write("Нажмите ENTER для продолжения...");
                return;
            }

            // Финальное сообщение.
            Console.WriteLine("Все файлы были прочитаны и все действия были выполнены.");
            ConsoleWarehouseInfo(warehouse);
            Console.Write("Нажмите ENTER для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        // Считывание файла warehouse.txt.
        public static bool ReadWarehouseTxt(ref int capacity, ref double pricePerContainer)
        {
            // Ввод переменных.
            capacity = 0;
            pricePerContainer = 0;
            var lines = File.ReadAllLines("warehouse" + Path.DirectorySeparatorChar + "warehouse.txt");
            // Разбор ошибок в формате файла.
            if (lines.Length != 2)
                return false;
            if (!int.TryParse(lines[0], out capacity))
                return false;
            if (!double.TryParse(lines[1], out pricePerContainer))
                return false;
            if (capacity < 1 || pricePerContainer <= 0)
                return false;
            return true;
        }

        // Считывание файла containers.txt.
        public static bool ReadContainersTxt(ref List<Container> containers)
        {
            // Ввод переменных.
            var lines = File.ReadAllLines("warehouse" + Path.DirectorySeparatorChar + "containers.txt");
            string[] splittedLine;
            string line;
            double weight, pricePerKilo;
            var boxes = new List<Box>();
            // Чтение файла.
            for (int i = 4; i < lines.Length; i++)
            {
                splittedLine = lines[i].Split(']');
                boxes.Clear();
                for (int j = 0; j < splittedLine.Length - 1; j++)
                {
                    line = splittedLine[j].Substring(1, splittedLine[j].Length - 1);
                    if (!double.TryParse(line.Split('|')[0], out weight))
                        return false;
                    if (!double.TryParse(line.Split('|')[1], out pricePerKilo))
                        return false;
                    if (weight <= 0 || pricePerKilo <= 0)
                        return false;
                    boxes.Add(new Box(weight, pricePerKilo));
                }
                containers.Add(new Container(boxes));
            }
            return true;
        }

        // Считывание файла operations.txt.
        public static bool ReadOperationsTxt(ref List<string[]> operations)
        {
            // Разбор ошибок в формате файла.
            var lines = File.ReadAllLines("warehouse" + Path.DirectorySeparatorChar + "operations.txt");
            int index;
            string operation;
            // Чтение файла.
            for (int i = 4; i < lines.Length; i++)
            {
                if (lines[i].Split().Length != 2)
                    return false;
                operation = lines[i].Split()[0];
                if (!int.TryParse(lines[i].Split()[1], out index))
                    return false;
                if (operation != "add" && operation != "delete")
                    return false;
                if (index < 1)
                    return false;
                operations.Add(new string[2] { operation, index.ToString() });
            }
            return true;
        }

        // Выполнить операции из файла operations.txt.
        public static bool EvaluateOperations(ref Warehouse warehouse, List<Container> containers, List<string[]> operations)
        {
            // Выполнение операций.
            foreach (var operation in operations)
            {
                try
                {
                    if (operation[0] == "add")
                        warehouse.AddContainer(containers[int.Parse(operation[1]) - 1]);
                    else if (operation[0] == "delete")
                        warehouse.DeleteContainer(int.Parse(operation[1]) - 1);
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }



        /* 
         * Тут расположен main блок программы.
         */

        static void Main(string[] args)
        {
            // Приветственное сообщение.
            Console.WriteLine("Привет, это склад овощей :)");
            Console.Write("Давай начнем?");
            Console.ReadLine();
            Console.Clear();

            while (true)
            {
                // Выбор способа считывания информации.
                int readWay;
                Console.WriteLine("Для начала нужно выбрать способ считывания информации.");
                Console.WriteLine("1. Cчитывать все из консоли.");
                Console.WriteLine("2. Считывать все из файла.");
                Console.WriteLine("3. Выйти из программы.");
                do
                {
                    Console.Write("Введите номер операции: ");
                } while (!int.TryParse(Console.ReadLine(), out readWay) || readWay > 3 || readWay < 1);
                Console.WriteLine();

                // Разбор всех операций.
                try
                {
                    switch (readWay)
                    {
                        case 1:
                            ConsoleMain();
                            break;
                        case 2:
                            FileMain();
                            break;
                        case 3:
                            Console.WriteLine("Спасибо за использование моей программы! Удачи и хорошей проверки!");
                            Console.WriteLine();
                            Console.Write("Для продолжения нажмите любую клавишу...");
                            Console.ReadLine();
                            return;
                    }
                }
                // Отлов неожиданных ошибок.
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

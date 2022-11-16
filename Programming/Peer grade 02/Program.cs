using System;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.ComTypes;

namespace FileManager
{
    class Program
    {
        // Чтение текстового файла по его имени и кодировке.
        public static string ReadFile(string filepath, Encoding userEncoding)
        {
            return File.ReadAllText(filepath, encoding: userEncoding);
        }

        // Получение всех текстовых файлов в текузей директории.
        public static List<string> GetTxtFiles(string path)
        {
            // Сбор всех текстовых файлов в текущей директории.
            var files = Directory.GetFiles(path);
            int index;
            string filename;
            var txtFiles = new List<string>();
            // Отбор текстовых файлов.
            foreach (var file in files)
            {
                index = file.LastIndexOf(Path.DirectorySeparatorChar);
                filename = file.Substring(index + 1, file.Length - index - 1);
                if (filename.EndsWith(".txt"))
                {
                    txtFiles.Add(filename);
                }
            }
            return txtFiles;
        }

        // Проверка массива строк, что их можно перевести в числа.
        public static bool VerifyNumbers(string[] fileStrings)
        {
            int number;
            foreach (var elem in fileStrings)
            {
                if (!int.TryParse(elem, out number))
                {
                    return false;
                }
            }
            return true;
        }

        // Универсальная функция вывода массива.
        public static void PrintArray(IList array)
        {
            int length = array.Count;
            Console.Write("[");
            for (int i = 0; i < length - 1; i++)
            {
                Console.Write($"{array[i]}, ");
            }
            if (length > 0)
            {
                Console.Write($"{array[length - 1]}");
            }
            Console.WriteLine("]");
        }

        // Проверка корректности имени папки.
        public static bool directoryIsCorrect(string directoryName)
        {
            foreach (var elem in Path.GetInvalidFileNameChars())
            {
                if (directoryName.Contains(elem))
                {
                    return false;
                }
            }
            return true;
        }

        // Операция #1: Выбор диска.
        public static void ChangeCurrentDisk(ref string path)
        {
            // Сбор всех дисков.
            var drives = new List<DriveInfo>();
            // Пробег по всем дискам и вывод основной информации.
            foreach (var drive in DriveInfo.GetDrives())
            {
                drives.Add(drive);
                Console.WriteLine("Имя диска:        " + drive.Name);
                Console.WriteLine("Корневой каталог: " + drive.RootDirectory);
                Console.WriteLine("Размер диска:     " + drive.TotalSize);
                Console.WriteLine("Свободное место:  " + drive.AvailableFreeSpace);
                Console.WriteLine("Файловая система: " + drive.DriveFormat);
                Console.WriteLine();
            }
            // Выбор и вывод всех дисков компьютера.
            for (int i = 0; i < drives.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {drives[i].Name} ({drives[i].RootDirectory})");
            }
            int userInput;
            do
            {
                Console.Write("Введите номер диска: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > drives.Count || userInput < 1);
            path = drives[userInput - 1].RootDirectory.ToString();
        }

        // Операция #2: Выбор директории.
        public static void ChangeCurrentDirectory(ref string path)
        {
            // Вывод всех директорий текущего пути.
            var directories = Directory.GetDirectories(path);
            if (directories.Length == 0)
            {
                Console.WriteLine("По текущему пути не найдено директорий.");
                return;
            }
            int index;
            for (int i = 0; i < directories.Length; i++)
            {
                index = directories[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {directories[i].Substring(index + 1, directories[i].Length - index - 1)}");
            }
            // Выбор директории.
            int userInput;
            do
            {
                Console.Write("Введите номер директории: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > directories.Length || userInput < 1);
            path = directories[userInput - 1];
        }

        // Операция #3: Список файлов.
        public static void ListFiles(string path)
        {
            // Вывод всех директорий текущего пути.
            var files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                Console.WriteLine("По текущему пути не найдено файлов.");
                return;
            }
            int index;
            for (int i = 0; i < files.Length; i++)
            {
                index = files[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {files[i].Substring(index + 1, files[i].Length - index - 1)}");
            }
        }

        // Операция #4: Вывод текстового файла.
        public static void PrintTxtFile(string path)
        {
            // Cбор всех текстовых файлов.
            var txtFiles = GetTxtFiles(path);
            if (txtFiles.Count == 0)
            {
                Console.WriteLine("По текущему пути не найдено текстовых файлов.");
                return;
            }
            // Вывод списка текстовых файлов.
            for (int i = 0; i < txtFiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {txtFiles[i]}");
            }
            // Выбор текстового файла.
            int userInput;
            do
            {
                Console.Write("Введите номер текстового файла: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > txtFiles.Count || userInput < 1);
            Console.WriteLine();
            // Выбор кодировки.
            int userInputEncoding;
            Console.WriteLine("1. UTF8");
            Console.WriteLine("2. ASCII");
            Console.WriteLine("3. Unicode");
            do
            {
                Console.Write("Введите номер кодировки: ");
            } while (!int.TryParse(Console.ReadLine(), out userInputEncoding) || userInputEncoding > 3 || userInputEncoding < 1);
            Console.WriteLine();
            // Вывод файла.
            switch (userInputEncoding)
            {
                case 1:
                    Console.WriteLine($"[FILENAME: {txtFiles[userInput - 1]} | ENCODING: UTF8]");
                    Console.WriteLine(ReadFile(path + Path.DirectorySeparatorChar + txtFiles[userInput - 1], Encoding.UTF8));
                    break;
                case 2:
                    Console.WriteLine($"[FILENAME: {txtFiles[userInput - 1]} | ENCODING: ASCII]");
                    Console.WriteLine(ReadFile(path + Path.DirectorySeparatorChar + txtFiles[userInput - 1], Encoding.ASCII));
                    break;
                case 3:
                    Console.WriteLine($"[FILENAME: {txtFiles[userInput - 1]} | ENCODING: Unicode]");
                    Console.WriteLine(ReadFile(path + Path.DirectorySeparatorChar + txtFiles[userInput - 1], Encoding.Unicode));
                    break;
            }
        }

        // Операция #5: Копирование файла.
        public static void CopyFile(string path)
        {
            // Сбор всех файлов текущей директории.
            var files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                Console.WriteLine("По текущему пути не найдено файлов.");
                return;
            }
            // Вывод всех файлов текущей директории.
            int index;
            for (int i = 0; i < files.Length; i++)
            {
                index = files[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {files[i].Substring(index + 1, files[i].Length - index - 1)}");
            }
            // Выбор файла в текущей директории.
            int userInput;
            do
            {
                Console.Write("Введите номер файла для копирования: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > files.Length || userInput < 1);
            index = files[userInput - 1].LastIndexOf(Path.DirectorySeparatorChar);
            string newFilename = "COPY " + files[userInput - 1].Substring(index + 1, files[userInput - 1].Length - index - 1);
            // Копирование файла.
            File.Copy(files[userInput - 1], path + "//" + newFilename);
            Console.WriteLine("Была создана копия файла.");
        }

        // Операция #6: Перемещение файла.
        public static void MoveFile(string path)
        {
            // Сбор всех файлов текущей директории
            var files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                Console.WriteLine("По текущему пути не найдено файлов.");
                return;
            }
            // Вывод всех файлов текущей директории
            int index;
            for (int i = 0; i < files.Length; i++)
            {
                index = files[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {files[i].Substring(index + 1, files[i].Length - index - 1)}");
            }
            // Выбор файла в текущей директории
            int userInput;
            do
            {
                Console.Write("Введите номер файла для копирования: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > files.Length || userInput < 1);
            // Ввод пути для копирования
            string pathMoveTo;
            do
            {
                Console.Write("Введите путь для перемещения файла: ");
                pathMoveTo = Console.ReadLine().Trim();
            } while (!Directory.Exists(pathMoveTo));
            // Перемещение файла.
            try
            {
                index = files[userInput - 1].LastIndexOf(Path.DirectorySeparatorChar);
                string filename = files[userInput - 1].Substring(index + 1, files[userInput].Length - index - 1);
                File.Move(files[userInput - 1], pathMoveTo + Path.DirectorySeparatorChar + filename);
                Console.WriteLine("Файл был перемещен.");
            }
            catch
            {
                Console.WriteLine("При перемещении произошла ошибка. " +
                    "Скорее всего программе нужны права администратора для перемещения.");
            }
        }

        // Операция #7: Удаление файла.
        public static void DeleteFile(string path)
        {
            // Сбор всех файлов текущей директории.
            var files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                Console.WriteLine("По текущему пути не найдено файлов.");
                return;
            }
            // Вывод всех файлов текущей директории.
            int index;
            for (int i = 0; i < files.Length; i++)
            {
                index = files[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {files[i].Substring(index + 1, files[i].Length - index - 1)}");
            }
            // Выбор файла в текущей директории.
            int userInput;
            do
            {
                Console.Write("Введите номер файла для копирования: ");
            } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > files.Length || userInput < 1);
            // Удаление файла.
            try
            {
                File.Delete(files[userInput - 1]);
                Console.WriteLine("Файл был удален.");
            }
            catch
            {
                Console.WriteLine("При удалении произошла ошибка. " +
                    "Скорее всего программе нужны права администратора для перемещения.");
            }
        }

        // Операция #8: Создание текстового файла.
        public static void CreateTxtFile(string path)
        {
            // Ввод имени файла.
            Console.Write("Введите имя файла: ");
            string filename = Console.ReadLine().Trim();
            // Выбор кодировки.
            int userInputEncoding;
            Console.WriteLine("1. UTF8");
            Console.WriteLine("2. ASCII");
            Console.WriteLine("3. Unicode");
            do
            {
                Console.Write("Введите номер кодировки: ");
            } while (!int.TryParse(Console.ReadLine(), out userInputEncoding) || userInputEncoding > 3 || userInputEncoding < 1);
            // Ввод текста файла.
            Console.Write("Введите текст файла: ");
            string text = Console.ReadLine();
            // Создание файла.
            File.Create(path + Path.DirectorySeparatorChar + filename + ".txt").Close();
            File.WriteAllText(path + Path.DirectorySeparatorChar + filename + ".txt", text);
            Console.WriteLine("Файл был успешно создан.");
        }

        // Операция #9: Вывод нескольких текстовых файлов.
        public static void PrintTxtFiles(string path)
        {
            // Cбор всех текстовых файлов.
            var txtFiles = GetTxtFiles(path);
            if (txtFiles.Count == 0)
            {
                Console.WriteLine("По текущему пути не найдено текстовых файлов.");
                return;
            }
            // Вывод списка текстовых файлов.
            for (int i = 0; i < txtFiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {txtFiles[i]}");
            }
            // Выбор текстовых файлов.
            string[] fileStrings;
            int[] fileNumbers;
            while (true)
            {
                Console.Write("Введите номера текстовых файлов через пробел: ");
                fileStrings = Console.ReadLine().Split();
                if (VerifyNumbers(fileStrings))
                {
                    fileNumbers = fileStrings.Select(x => int.Parse(x)).ToArray();
                    if (fileNumbers.Max() <= txtFiles.Count && fileNumbers.Min() > 0)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
            // Выбор кодировки.
            int userInputEncoding;
            Console.WriteLine("1. UTF8");
            Console.WriteLine("2. ASCII");
            Console.WriteLine("3. Unicode");
            do
            {
                Console.Write("Введите номер кодировки: ");
            } while (!int.TryParse(Console.ReadLine(), out userInputEncoding) || userInputEncoding > 3 || userInputEncoding < 1);
            Console.WriteLine();
            // Вывод файлов.
            foreach (var number in fileNumbers)
            {
                switch (userInputEncoding)
                {
                    case 1:
                        Console.WriteLine($"[FILENAME: {txtFiles[number - 1]} | ENCODING: UTF8]");
                        Console.WriteLine(ReadFile(path + Path.DirectorySeparatorChar + txtFiles[number - 1], Encoding.UTF8));
                        break;
                    case 2:
                        Console.WriteLine($"[FILENAME: {txtFiles[number - 1]} | ENCODING: ASCII]");
                        Console.WriteLine(ReadFile(path + Path.DirectorySeparatorChar + txtFiles[number - 1], Encoding.ASCII));
                        break;
                    case 3:
                        Console.WriteLine($"[FILENAME: {txtFiles[number - 1]} | ENCODING: Unicode]");
                        Console.WriteLine(ReadFile(path + Path.DirectorySeparatorChar + txtFiles[number - 1], Encoding.Unicode));
                        break;
                }
                Console.WriteLine();
            }
        }

        // Операция #10: Вернуться на одну директорию назад.
        public static void ChangeToParentDirectory(ref string path)
        {
            if (Directory.GetParent(path) is null)
            {
                Console.WriteLine("Не удается перейти в родительский каталог.");
                return;
            }
            path = Directory.GetParent(path).ToString();
            Console.WriteLine("Успешно перешел в родительский каталог.");
        }

        // Операция #11: Вывести информацию о текущей директории.
        public static void DirectoryInfo(string path)
        {
            var info = new DirectoryInfo(path);
            Console.WriteLine($"[Основная информация о директории]");
            Console.WriteLine($"Имя директории  - {info.Name}");
            Console.WriteLine($"Имя родителя    - {info.Parent}");
            Console.WriteLine($"Корневая папка  - {info.Root}");
            Console.WriteLine($"Атрибуты        - {info.Attributes}");
            Console.WriteLine($"Время создания  - {info.CreationTime}");
            Console.WriteLine($"Время изменения - {info.LastWriteTime}");
            Console.WriteLine();

            int index;
            var directories = Directory.GetDirectories(path);
            Console.WriteLine($"[Папки в директории]");
            if (directories.Length == 0)
            {
                Console.WriteLine("Нет папок в директории");
            }
            for (int i = 0; i < directories.Length; i++)
            {
                index = directories[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {directories[i].Substring(index + 1, directories[i].Length - index - 1)}");
            }
            Console.WriteLine();

            index = -1;
            var files = Directory.GetFiles(path);
            Console.WriteLine($"[Файлы в директории]");
            if (files.Length == 0)
            {
                Console.WriteLine("Нет файлов в директории");
            }
            for (int i = 0; i < files.Length; i++)
            {
                index = files[i].LastIndexOf(Path.DirectorySeparatorChar);
                Console.WriteLine($"{i + 1}. {files[i].Substring(index + 1, files[i].Length - index - 1)}");
            }
        }
        
        // Операция #12: Вывести информацию о текущей директории.
        public static void CreateDirectory(string path)
        {
            string directoryName;
            do
            {
                Console.Write("Введите название папки: ");
                directoryName = Console.ReadLine();
            } while (!directoryIsCorrect(directoryName));
            try
            {
                Directory.CreateDirectory(path + Path.DirectorySeparatorChar + directoryName);
                Console.WriteLine("Директория была успешно создана.");
            }
            catch
            {
                Console.WriteLine("Во время создания директории произошла ошибка.");
            }
        }

        static void Main(string[] args)
        {
            int userInput;
            string currentPath = Directory.GetCurrentDirectory();
            string operationsFilePath = currentPath + Path.DirectorySeparatorChar + "AllOperations.txt";
            do
            {
                Console.WriteLine($"Текущее положение - {currentPath}");
                Console.WriteLine(ReadFile(operationsFilePath, Encoding.UTF8));
                do
                {
                    Console.Write("Введите номер операции: ");
                } while (!int.TryParse(Console.ReadLine(), out userInput) || userInput > 13 || userInput < 1);
                Console.WriteLine();
                switch (userInput)
                {
                    // Выбор диска.
                    case 1:
                        ChangeCurrentDisk(ref currentPath);
                        break;
                    // Выбор директории.
                    case 2:
                        ChangeCurrentDirectory(ref currentPath);
                        break;
                    // Список файлов.
                    case 3:
                        ListFiles(currentPath);
                        break;
                    // Вывод текстового файла (opt: Кодировка).
                    case 4:
                        PrintTxtFile(currentPath);
                        break;
                    // Копирование файла.
                    case 5:
                        CopyFile(currentPath);
                        break;
                    // Перемещение файла.
                    case 6:
                        MoveFile(currentPath);
                        break;
                    // Удаление файла.
                    case 7:
                        DeleteFile(currentPath);
                        break;
                    // Создание текстового файла (opt: Кодировка).
                    case 8:
                        CreateTxtFile(currentPath);
                        break;
                    // Вывод нескольких текстовых файлов (opt: Кодировка).
                    case 9:
                        PrintTxtFiles(currentPath);
                        break;
                    // Вернуться на одну директорию назад.
                    case 10:
                        ChangeToParentDirectory(ref currentPath);
                        break;
                    // Вывести информацию о текущей директории.
                    case 11:
                        DirectoryInfo(currentPath);
                        break;
                    // Создание директории
                    case 12:
                        CreateDirectory(currentPath);
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine();
            } while (userInput != 13);
            Console.WriteLine("Спасибо за использование моей программы! Удачи!");
            Console.WriteLine();
            Console.Write("Для продолжения нажмите любую клавишу...");
            Console.ReadLine();
        }
    }
}

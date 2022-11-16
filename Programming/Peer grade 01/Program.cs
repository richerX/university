using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bulls_and_Cows
{
    class Program
    {   
        // Определяет находиться ли элемент в массиве
        // Возвращает true, если находиться
        // Возвращает false, в противоположном случае
        public static bool ElementInArray(int[] array, int element)
        {
            foreach (int digit in array)
            {
                if (digit == element)
                {
                    return true;
                }
            }
            return false;
        }

        // Создает рандомную цифру от 0 до 9,
        // которой нет в массиве
        // Возвращает цифру
        public static int CreateNextDigit(int[] array)
        {
            int next_digit;
            Random rnd = new Random();
            do
            {
                next_digit = rnd.Next(0, 10);
            } while (ElementInArray(array, next_digit));
            return next_digit;
        }

        // Создает рандомное четырехзначное число
        // без общих цифр
        // Возвращает это число
        public static int GenerateNumber()
        {
            int answer = 0;
            int digit;
            int[] used_digits = new int[4];
            Random rnd = new Random();
            // создает первую цифру числа от 0 до 9
            digit = rnd.Next(1, 10);
            answer += digit * 1000;
            used_digits[0] = digit;
            // создает оставшиеся три цифры числа
            for (int i = 0; i < 3; i++)
            {
                digit = CreateNextDigit(used_digits);
                answer += digit * (int)Math.Pow(10, 2 - i);
                used_digits[i + 1] = digit;
                //  Console.WriteLine($"{used_digits[0]} {used_digits[1]} {used_digits[2]} {used_digits[3]}");
            }
            // возврат четырехзначного числа с разными цифрами
            return answer;
        }

        // Создает массив со всеми цифрами числа
        // Возвращает этот массив
        public static int[] DecompositionInArray(int number)
        {
            int[] digits = new int[4];
            for (int i = 0; i < 4; i++)
            {
                digits[i] = number % 10;
                number /= 10;
            }
            return digits;
        }

        // Считает кол-во быков в числе,
        // которое ввел пользователь
        // Возвращает кол-во быков
        public static int CountBulls(int user_number, int guess_number)
        {
            int answer = 0;
            for (int i = 0; i < 4; i++)
            {
                if (user_number % 10 == guess_number % 10)
                {
                    answer += 1;
                }
                user_number /= 10;
                guess_number /= 10;
            }
            return answer;
        }

        // Считает кол-во общих цифр в двух числах
        // Используется для подсчета коров
        // Коровы + Быки = Кол-во общих цифр
        // Возвращет кол-во общих цифр
        public static int CountCommonDigits(int user_number, int guess_number)
        {
            int[] user_digits = DecompositionInArray(user_number);
            int[] guess_digits = DecompositionInArray(guess_number);
            int answer = 0;
            int digit_in;
            int user_digit;
            for (int i = 0; i < 4; i++)
            {
                digit_in = 0;
                user_digit = user_digits[i];
                foreach (int guess_digit in guess_digits)
                {
                    if (user_digit == guess_digit)
                    {
                        digit_in = 1;
                        break;
                    }
                }
                answer += digit_in;
            }
            return answer;
        }

        // Состоит из двух основных циклов
        // Внешний для прекращения/продолжения игры
        // Внутренний для проведения игры "Быки и Коровы"
        // Весь вывод/ввод происходит в консоле
        // Ничего не возвращает
        static void Main(string[] args)
        {
            Console.WriteLine("Привет! Это игра в 'Быки и Коровы'. Правила такие:");
            Console.WriteLine("1. Я загадываю число");
            Console.WriteLine("2. Ты пытаешься его угадать");
            Console.WriteLine("3. На каждую твою попытку, я буду называть кол-во Быков и Коров");
            Console.WriteLine("4. Играем, пока ты не угадаешь");
            Console.WriteLine("* Бык    - нужная цифра на нужном месте");
            Console.WriteLine("* Корова - нужная цифра на ненужном месте");
            Console.WriteLine("Давай начинать :)");
            do
            {

                int user_number;
                int guess_number = GenerateNumber();
                int number_of_tries = 0;
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine($"Я загадал число, начинай угадывать");
                // Console.WriteLine($"Загаданное число - {guess_number}");
                Console.WriteLine();
                // do while, пока пользователь не угадает число
                do
                {
                    // do while, пока пользователь не введет корректное значение
                    do
                    {
                        Console.Write("Введите 4-значное число: ");
                    } while (!int.TryParse(Console.ReadLine(), out user_number) || user_number > 9999 || user_number < 1000);
                    number_of_tries += 1;
                    Console.WriteLine($"Быки   - {CountBulls(user_number, guess_number)}");
                    Console.WriteLine($"Коровы - {CountCommonDigits(user_number, guess_number) - CountBulls(user_number, guess_number)}");
                    Console.WriteLine();
                } while (user_number != guess_number);
                Console.WriteLine($"Поздравляю, ты угадал! Попыток использовано - {number_of_tries}");
                Console.WriteLine("Для выхода нажмите ESC, для продолжения - ENTER");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}

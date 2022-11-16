using System;

namespace MainSpace
{
    class Messages
    {
        /// <summary>
        /// Зеленое сообщение.
        /// </summary>
        /// <param name="message"></param>
        public static void Correct(string message) =>
            ShowColorMessage(message, ConsoleColor.Green);

        /// <summary>
        /// Красное сообщение.
        /// </summary>
        /// <param name="message"></param>
        public static void Wrong(string message) =>
            ShowColorMessage(message, ConsoleColor.Red);

        /// <summary>
        /// Желтое сообщение.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message) =>
            ShowColorMessage(message, ConsoleColor.Yellow);

        /// <summary>
        /// Темно-фиолетовое сообщение.
        /// </summary>
        /// <param name="message"></param>
        public static void Menu(string message) =>
            ShowColorMessage(message, ConsoleColor.White);

        /// <summary>
        /// Вывод сообщения на экран в определнном цвете.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        private static void ShowColorMessage(string message, ConsoleColor color)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}

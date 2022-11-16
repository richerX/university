using System;

namespace MainSpace
{
    class CursorMenu
    {
        /// <summary>
        /// Ввод переменных класса.
        /// </summary>
        private int position;
        private string title;
        private string cursor;
        private string skip = "";
        private string[] commands;
        private int titleHeight = 2;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="cursor"></param>
        /// <param name="commands"></param>
        public CursorMenu(string title, string cursor, string[] commands)
        {
            this.position = 0;
            this.title = title;
            this.cursor = cursor + " ";
            this.commands = commands;
            for (int i = 0; i < this.cursor.Length + 1; i++)
                skip += " ";
        }

        /// <summary>
        /// Вывод меню на экран.
        /// </summary>
        public void Show()
        {
            Console.Clear();
            this.position = 0;
            Messages.Info(title + Environment.NewLine);
            for (int i = 0; i < commands.Length; i++)
            {
                if (i == 0)
                    Messages.Menu(cursor + commands[i]);
                else
                    Messages.Menu(skip + commands[i]);
            }
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 2);
        }

        /// <summary>
        /// Обновление положения курсора.
        /// </summary>
        /// <param name="newPosition"></param>
        public void UpdateCursor(int newPosition)
        {
            if (newPosition > commands.Length - 1)
                newPosition = 0;
            if (newPosition < 0)
                newPosition = commands.Length - 1;
            Console.SetCursorPosition(0, position + titleHeight);
            Messages.Menu(skip + commands[position] + " ");
            Console.SetCursorPosition(0, newPosition + titleHeight);
            Messages.Menu(cursor + commands[newPosition] + " ");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 2);
            position = newPosition;
        }

        /// <summary>
        /// Выбор команды.
        /// </summary>
        /// <returns></returns>
        public int Select()
        {
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, commands.Length + titleHeight + 1);
                        return position;
                    case ConsoleKey.UpArrow:
                        UpdateCursor(position - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        UpdateCursor(position + 1);
                        break;
                }
            }
        }

        /// <summary>
        /// Обрамление заголовка.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string FrameString(string line)
        {
            int length = line.Length + 4;
            string answer = "";

            answer += skip;
            for (int i = 0; i < length; i++)
                answer += "_";
            answer += Environment.NewLine;

            answer += skip;
            answer += "|";
            for (int i = 0; i < length - 2; i++)
                answer += " ";
            answer += "|";
            answer += Environment.NewLine;

            answer += skip;
            answer += "| ";
            answer += line;
            answer += " |";
            answer += Environment.NewLine;

            answer += skip;
            answer += "|";
            for (int i = 0; i < length - 2; i++)
                answer += "_";
            answer += "|";
            answer += Environment.NewLine;

            return answer;

        }
    }
}

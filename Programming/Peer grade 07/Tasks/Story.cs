using System;
using System.Collections.Generic;
using System.Text;

namespace MainSpace
{
    class Story : MainTask
    {
        /// <summary>
        /// Ввод переменных.
        /// </summary>
        public List<User> users;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="creationDate"></param>
        /// <param name="status"></param>
        public Story(string name, DateTime creationDate, string status = "Открытая задача") : base(name, creationDate, status)
        {
            this.name = name;
            this.creationDate = creationDate;
            this.status = status;
            this.users = new List<User>();
        }

        /// <summary>
        /// Перегрузка метода
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string answer = $"Story | {name} | {creationDate} | {status} | ";
            answer += "Исполнители: ";
            for (int i = 0; i < users.Count - 1; i++)
            {
                answer += $"{users[i]}, ";
            }
            if (users.Count > 0)
                answer += $"{users[users.Count - 1]}";
            return answer;
        }
    }
}

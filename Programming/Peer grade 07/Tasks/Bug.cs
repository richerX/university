using System;
using System.Collections.Generic;
using System.Text;

namespace MainSpace
{
    class Bug : MainTask
    {
        /// <summary>
        /// Ввод переменных.
        /// </summary>
        public User user;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="creationDate"></param>
        /// <param name="status"></param>
        public Bug(string name, DateTime creationDate, string status = "Открытая задача") : base(name, creationDate, status)
        {
            this.name = name;
            this.creationDate = creationDate;
            this.status = status;
        }

        /// <summary>
        /// Перегрузка метода.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Bug | {name} | {creationDate} | {status} | Исполнитель: {user}";
        }
    }
}

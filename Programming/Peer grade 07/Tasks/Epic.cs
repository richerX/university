using System;
using System.Collections.Generic;
using System.Text;

namespace MainSpace
{
    class Epic : MainTask
    {
        /// <summary>
        /// Ввод переменных.
        /// </summary>
        public List<MainTask> subtasks;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="creationDate"></param>
        /// <param name="status"></param>
        public Epic(string name, DateTime creationDate, string status = "Открытая задача") : base(name, creationDate, status)
        {
            this.name = name;
            this.creationDate = creationDate;
            this.status = status;
            this.subtasks = new List<MainTask>();
        }

        /// <summary>
        /// Подробный вывод информации о задачу.
        /// </summary>
        /// <returns></returns>
        public string Specification()
        {
            string answer = $"Epic | {name} | {creationDate} | {status}" + Environment.NewLine;
            foreach (var subtask in subtasks)
            {
                answer += $"     |     " + Environment.NewLine;
                answer += $"     |----> {subtask}" + Environment.NewLine;
            }
            return answer;
        }

        /// <summary>
        /// Перегрузка метода.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Epic | {name} | {creationDate} | {status}";
        }
    }
}

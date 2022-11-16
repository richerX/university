using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MainSpace
{
    class Program
    {
        /// <summary>
        /// Ввод массивов с пользователями и проектами.
        /// </summary>
        static List<User> allUsers = new List<User>();
        static List<Project> allProjects = new List<Project>();

        /*
         *  БЛОК ОСНОВНОГО МЕНЮ.
         */

        /// <summary>
        /// Блок основного меню.
        /// </summary>
        public static void Menu()
        {
            var title = " <> Основное меню";
            var cursor = ">>";
            var commands = File.ReadAllLines($"Operations" + Path.DirectorySeparatorChar + "Main.txt", Encoding.UTF8);
            CursorMenu menu = new CursorMenu(title, cursor, commands);
            while (true)
            {
                menu.Show();
                switch (menu.Select() + 1)
                {
                    case 1:
                        UsersMenu();
                        break;
                    case 2:
                        ProjectsMenu();
                        break;
                    case 3:
                        TasksMenu();
                        break;
                    case 4:
                        return;
                }
            }
        }

        /*
         *  БЛОК МЕНЮ "РАБОТА С ПОЛЬЗОВАТЕЛЯМИ".
         */

        /// <summary>
        /// Меню "работа с пользователями".
        /// </summary>
        public static void UsersMenu()
        {
            var title = " << Работа с пользователями";
            var cursor = ">>";
            var commands = File.ReadAllLines($"Operations" + Path.DirectorySeparatorChar + "UsersMenu.txt", Encoding.UTF8);
            CursorMenu menu = new CursorMenu(title, cursor, commands);
            while (true)
            {
                menu.Show();
                switch (menu.Select() + 1)
                {
                    case 1:
                        CreateUser();
                        break;
                    case 2:
                        ShowUsers();
                        break;
                    case 3:
                        DeleteUser();
                        break;
                    case 4:
                        return;
                }
            }
        }

        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        public static void CreateUser()
        {
            // Введение нового имени.
            string name = RequestString("Введите имя нового пользователя: ").Trim();
            if (!CorrectName(name, false))
            {
                Messages.Wrong("Некорректное имя пользователя.");
                Console.WriteLine();
                Messages.Info("В имени пользователя нельзя использовать пробелы, дополнительные символы и т.д.");
                ContinueMessage();
                return;
            }
            // Проверка - существуют ли уже такой пользователь.
            foreach (var user in allUsers)
            {
                if (user.name == name)
                {
                    Messages.Wrong("Пользователь с таким именем уже существует.");
                    ContinueMessage();
                    return;
                }
            }
            // Добавление нового пользователя.
            allUsers.Add(new User(name));
            Messages.Correct($"Пользователь {name} был успешно добавлен.");
            ContinueMessage();
        }

        /// <summary>
        /// Список всех пользователей.
        /// </summary>
        public static void ShowUsers()
        {
            if (allUsers.Count == 0)
                Messages.Wrong("Пользователи отсутствуют.");
            for (int i = 0; i < allUsers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allUsers[i]}");
            }
            ContinueMessage();
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        public static void DeleteUser()
        {
            // Отбор исключений.
            if (allUsers.Count == 0)
            {
                Messages.Wrong("Пользователи отсутствуют.");
                ContinueMessage();
                return;
            }
            // Вывод всех пользователей.
            for (int i = 0; i < allUsers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allUsers[i]}");
            }
            Console.WriteLine();
            // Выбор пользователя для удаления.
            int index = RequestInt("Введите номер пользователя, которого хотите удалить: ");
            if (index < 1 || index > allUsers.Count)
            {
                Messages.Wrong("Пользователя с таким номером не существует.");
                ContinueMessage();
                return;
            }
            // Финальное сообщение.
            Messages.Correct($"Пользователь с именем {allUsers[index - 1].name} был успешно удален.");
            TotalDeleteUser(allUsers[index - 1].name);
            allUsers.RemoveAt(index - 1);
            ContinueMessage();
        }

        /// <summary>
        /// Удаления пользователя из всех задач.
        /// </summary>
        /// <param name="username"></param>
        public static void TotalDeleteUser(string username)
        {
            // Перебор всех проектов.
            foreach (var project in allProjects)
            {
                // Перебор всех задач.
                foreach (var task in project.tasks)
                {
                    // Удаление пользователя из подзадач.
                    if (task.GetType() == typeof(Epic))
                    {
                        Epic currentTask = (Epic)task;
                        foreach (var subtusk in currentTask.subtasks)
                        {
                            if (subtusk.GetType() == typeof(Story))
                            {
                                Story currentSubtask = (Story)subtusk;
                                for (int i = 0; i < currentSubtask.users.Count; i++)
                                {
                                    if (currentSubtask.users[i].name == username)
                                    {
                                        currentSubtask.users.RemoveAt(i);
                                        break;
                                    }
                                }
                            }

                            if (subtusk.GetType() == typeof(Task))
                            {
                                Task currentSubtask = (Task)subtusk;
                                if (currentSubtask.user != null && currentSubtask.user.name == username)
                                    currentSubtask.user = null;
                            }

                            if (subtusk.GetType() == typeof(Bug))
                            {
                                Bug currentSubtask = (Bug)subtusk;
                                if (currentSubtask.user != null && currentSubtask.user.name == username)
                                    currentSubtask.user = null;
                            }
                        }
                    }
                    // Удаление пользователя из задачи.
                    if (task.GetType() == typeof(Story))
                    {
                        Story currentTask = (Story)task;
                        for (int i = 0; i < currentTask.users.Count; i++)
                        {
                            if (currentTask.users[i].name == username)
                            {
                                currentTask.users.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    // Удаление пользователя из задачи.
                    if (task.GetType() == typeof(Task))
                    {
                        Task currentTask = (Task)task;
                        if (currentTask.user != null && currentTask.user.name == username)
                            currentTask.user = null;
                    }
                    // Удаление пользователя из задачи.
                    if (task.GetType() == typeof(Bug))
                    {
                        Bug currentTask = (Bug)task;
                        if (currentTask.user != null && currentTask.user.name == username)
                            currentTask.user = null;
                    }
                }
            }
        }

        /*
         *  БЛОК МЕНЮ "РАБОТА С ПРОЕКТАМИ".
         */

        /// <summary>
        /// Меню "работа с проектами".
        /// </summary>
        public static void ProjectsMenu()
        {
            var title = " << Работа с проектами";
            var cursor = ">>";
            var commands = File.ReadAllLines($"Operations" + Path.DirectorySeparatorChar + "ProjectsMenu.txt", Encoding.UTF8);
            CursorMenu menu = new CursorMenu(title, cursor, commands);
            while (true)
            {
                menu.Show();
                switch (menu.Select() + 1)
                {
                    case 1:
                        CreateProject();
                        break;
                    case 2:
                        ShowProjects();
                        break;
                    case 3:
                        RenameProject();
                        break;
                    case 4:
                        DeleteProject();
                        break;
                    case 5:
                        return;
                }
            }
        }

        /// <summary>
        /// Создание нового проекта.
        /// </summary>
        public static void CreateProject()
        {
            // Введение имени нового проекта.
            string name = RequestString("Введите имя нового проекта: ").Trim();
            if (!CorrectName(name))
            {
                Messages.Wrong("Некорректное имя проекта.");
                ContinueMessage();
                return;
            }
            // Проверка - существует ли такой же проект.
            foreach (var project in allProjects)
            {
                if (project.name == name)
                {
                    Messages.Wrong("Проект с таким именем уже существует.");
                    ContinueMessage();
                    return;
                }
            }
            // Введение вместимости проекта.
            int capacity = RequestInt("Введите максимальное кол-во задач для проекта: ");
            if (capacity < 1)
            {
                Messages.Wrong("Недопустимое значение максимального кол-ва задач для проекта.");
                ContinueMessage();
                return;
            }
            // Добавление проекта.
            allProjects.Add(new Project(name, new List<MainTask>(), capacity));
            Messages.Correct($"Проект {name} был успешно добавлен.");
            ContinueMessage();
        }

        /// <summary>
        /// Вывод всех проектов.
        /// </summary>
        public static void ShowProjects()
        {
            if (allProjects.Count == 0)
                Messages.Wrong("Проекты отсутствуют.");
            for (int i = 0; i < allProjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allProjects[i].Specification()}");
                if (i != allProjects.Count - 1)
                {
                    Messages.Info(@"   /_\     /_\     /_\     /_\     /_\     /_\     /_\     /_\     /_\     /_\     /_\");
                    Console.WriteLine();
                }
            }
            ContinueMessage();
        }

        /// <summary>
        /// Переименование проекта.
        /// </summary>
        public static void RenameProject()
        {
            // Проверка исключения.
            if (allProjects.Count == 0)
            {
                Messages.Wrong("Проекты отсутствуют.");
                ContinueMessage();
                return;
            }
            // Вывод всех проектов.
            for (int i = 0; i < allProjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allProjects[i]}");
            }
            // Выбор проекта для переименования.
            Console.WriteLine();
            int index = RequestInt("Введите номер проекта, который хотите переименовать: ");
            if (index < 1 || index > allProjects.Count)
            {
                Messages.Wrong("Проекта с таким номером не существует.");
                ContinueMessage();
                return;
            }
            // Введение нового имени проекта и проверка его уникальности.
            string newName = RequestString("Введите новое имя проекта: ");
            foreach (var project in allProjects)
            {
                if (project.name == newName)
                {
                    Messages.Wrong("Проект с таким именем уже существует.");
                    ContinueMessage();
                    return;
                }
            }
            // Финальное сообщение.
            Messages.Correct($"Проект был успешно переименован {allProjects[index - 1].name} -> {newName}.");
            allProjects[index - 1].name = newName;
            ContinueMessage();
        }

        /// <summary>
        /// Удаление проекта.
        /// </summary>
        public static void DeleteProject()
        {
            // Проверка исключения.
            if (allProjects.Count == 0)
            {
                Messages.Wrong("Проекты отсутствуют.");
                ContinueMessage();
                return;
            }
            // Вывод всех проектов.
            for (int i = 0; i < allProjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allProjects[i]}");
            }
            // Выбор проекта.
            Console.WriteLine();
            int index = RequestInt("Введите номер проекта, который хотите удалить: ");
            if (index < 1 || index > allProjects.Count)
            {
                Messages.Wrong("Проекта с таким номером не существует.");
                ContinueMessage();
                return;
            }
            // Финальное сообщение.
            Messages.Correct($"Проект с именем {allProjects[index - 1].name} был успешно удален.");
            allProjects.RemoveAt(index - 1);
            ContinueMessage();
        }

        /*
         *  БЛОК МЕНЮ "РАБОТА С ЗАДАЧАМИ В ПРОЕКТЕ".
         */

        /// <summary>
        /// Меню "работа с задачами в проекте".
        /// </summary>
        public static void TasksMenu()
        {
            Project currentProject = new Project("skip", new List<MainTask>(), 10);
            if (!SelectProject(ref currentProject))
                return;
            var title = $" << Работа с задачами в проекте {currentProject.name}";
            var cursor = ">>";
            var commands = File.ReadAllLines($"Operations" + Path.DirectorySeparatorChar + "TasksMenu.txt", Encoding.UTF8);
            CursorMenu menu = new CursorMenu(title, cursor, commands);
            while (true)
            {
                menu.Show();
                switch (menu.Select() + 1)
                {
                    case 1:
                        AddTask(currentProject);
                        break;
                    case 2:
                        DeleteTask(currentProject);
                        break;
                    case 3:
                        EmptySelection();
                        break;
                    case 4:
                        ShowTasks(currentProject);
                        break;
                    case 5:
                        SelectStatus(currentProject);
                        break;
                    case 6:
                        GroupByStatus(currentProject);
                        break;
                    case 7:
                        EmptySelection();
                        break;
                    case 8:
                        AddUser(currentProject);
                        break;
                    case 9:
                        RemoveUser(currentProject);
                        break;
                    case 10:
                        EmptySelection();
                        break;
                    case 11:
                        AddEpicSubtask(currentProject);
                        break;
                    case 12:
                        RemoveEpicSubtask(currentProject);
                        break;
                    case 13:
                        AddEpicUser(currentProject);
                        break;
                    case 14:
                        RemoveEpicUser(currentProject);
                        break;
                    case 15:
                        EmptySelection();
                        break;
                    case 16:
                        return;
                }
            }
        }

        /// <summary>
        /// Выбор проекта для работы с ним.
        /// </summary>
        /// <param name="currentProject"></param>
        /// <returns></returns>
        public static bool SelectProject(ref Project currentProject)
        {
            // Проверка исключений.
            if (allProjects.Count == 0)
            {
                Messages.Wrong("Проекты отсутствуют.");
                ContinueMessage();
                return false;
            }
            for (int i = 0; i < allProjects.Count; i++)
                Console.WriteLine($"{i + 1}. {allProjects[i]}");
            // Выбор проекта.
            Console.WriteLine();
            int index = RequestInt("Выбирите номер проекта: ");
            if (index < 1 || index > allProjects.Count)
            {
                Messages.Wrong("Проекта с таким номером не существует.");
                ContinueMessage();
                return false;
            }
            currentProject = allProjects[index - 1];
            return true;
        }

        /// <summary>
        /// Добавление задачи в проект.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void AddTask(Project currentProject)
        {
            // Проверка исключения.
            if (currentProject.tasks.Count == currentProject.capacity)
            {
                Messages.Wrong("Невозможно добавить задачу, так как проект заполнен.");
                ContinueMessage();
                return;
            }
            // Выбор типа задачи.
            Console.WriteLine($"1. Epic");
            Console.WriteLine($"2. Story");
            Console.WriteLine($"3. Task");
            Console.WriteLine($"4. Bug");
            Console.WriteLine();
            int index = RequestInt("Выберите задачу, которую вы хотите добавить: ");
            if (index < 0 || index > 4)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Введение имени задачи.
            string name = RequestString("Введите имя новой задачи: ").Trim();
            if (!CorrectName(name))
            {
                Messages.Wrong("Некорректное имя задачи.");
                ContinueMessage();
                return;
            }
            // Проверка имени задачи.
            foreach (var task in currentProject.tasks)
            {
                if (task.name == name)
                {
                    Messages.Wrong("Задача с таким именем уже существует.");
                    ContinueMessage();
                    return;
                }
            }
            // Создание задачи.
            var creationDate = DateTime.Now;
            if (index == 1)
                currentProject.AddTask(new Epic(name, creationDate));
            else if (index == 2)
                currentProject.AddTask(new Story(name, creationDate));
            else if (index == 3)
                currentProject.AddTask(new Task(name, creationDate));
            else if (index == 4)
                currentProject.AddTask(new Bug(name, creationDate));
            // Финальное сообщение.
            Messages.Correct("Задача была успешно добавлена.");
            Console.WriteLine();
            Messages.Info("Добавить и изменить исполнителей можно в соотвествующем пункте меню.");
            Messages.Info("Изменить статус задачи также можно в соотвествующем пункте меню.");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void DeleteTask(Project currentProject)
        {
            // Обработка исключения.
            if (currentProject.tasks.Count == 0)
            {
                Messages.Wrong("Задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Выбор задачи.
            for (int i = 0; i < currentProject.tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {currentProject.tasks[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, которую вы хотите удалить: ");
            if (index < 1 || index > currentProject.tasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Финальное сообщение.
            Messages.Correct($"Задача с именем {currentProject.tasks[index - 1].name} была успешно удалена.");
            currentProject.tasks.RemoveAt(index - 1);
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Вывод всех задач.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void ShowTasks(Project currentProject)
        {
            // Обработка исключения.
            if (currentProject.tasks.Count == 0)
            {
                Messages.Wrong("Задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Финальное сообщение.
            Console.WriteLine(currentProject.Specification());
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Смена статуса задачи.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void SelectStatus(Project currentProject)
        {
            // Обработка исключения.
            if (currentProject.tasks.Count == 0)
            {
                Messages.Wrong("Задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Выбор задачи.
            for (int i = 0; i < currentProject.tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {currentProject.tasks[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, статус которой вы хотите изменить: ");
            if (index < 1 || index > currentProject.tasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Выбор нового статуса.
            Console.WriteLine("1. Открытая задача.");
            Console.WriteLine("2. Задача в работе.");
            Console.WriteLine("3. Завершенная задача.");
            Console.WriteLine();
            int statusIndex = RequestInt("Выберите номер нового статуса: ");
            if (statusIndex < 1 || statusIndex > 3)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Смена статуса и финальное сообщение.
            Messages.Correct($"Статус задачи с именем {currentProject.tasks[index - 1].name} была успешно изменен.");
            if (statusIndex == 1)
                currentProject.tasks[index - 1].status = "Открытая задача";
            else if (statusIndex == 2)
                currentProject.tasks[index - 1].status = "Задача в работе";
            else if (statusIndex == 3)
                currentProject.tasks[index - 1].status = "Завершенная задача";
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Вывод задач по статусу.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void GroupByStatus(Project currentProject)
        {
            // Обработка исключения.
            if (currentProject.tasks.Count == 0)
            {
                Messages.Wrong("Задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Вывод открытых задач.
            Console.WriteLine("Открытые задачи:");
            foreach (var task in currentProject.tasks)
            {
                if (task.status == "Открытая задача")
                    Console.WriteLine(task);
            }
            Console.WriteLine();
            // Вывод задач в работе.
            Console.WriteLine("Задачи в работе:");
            foreach (var task in currentProject.tasks)
            {
                if (task.status == "Задача в работе")
                    Console.WriteLine(task);
            }
            Console.WriteLine();
            // Вывод завершенных задач.
            Console.WriteLine("Завершенные задачи:");
            foreach (var task in currentProject.tasks)
            {
                if (task.status == "Завершенная задача")
                    Console.WriteLine(task);
            }
            Console.WriteLine();
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Добавления исполнителя в задачу.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void AddUser(Project currentProject)
        {
            // Проверка исключений.
            if (currentProject.tasks.Count == 0)
            {
                Messages.Wrong("Задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            if (allUsers.Count == 0)
            {
                Messages.Wrong("Не существует ни одного пользователя.");
                ContinueMessage();
                return;
            }
            // Выбор задачи для добавления пользователя.
            for (int i = 0; i < currentProject.tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {currentProject.tasks[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, в которую вы хотите добавить пользователя: ");
            if (index < 1 || index > currentProject.tasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Отбор случаев с некорректно выбранной задачей.
            MainTask chosenTask = currentProject.tasks[index - 1];
            if (chosenTask.GetType() == typeof(Epic))
            {
                Messages.Wrong("Невозможно назначить исполнителя на Epic задачу.");
                ContinueMessage();
                return;
            }
            // Отбор исключений.
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                if (localTask.user != null)
                {
                    Messages.Wrong("На эту задачу уже назначен исполнитель.");
                    ContinueMessage();
                    return;
                }
            }
            if (chosenTask.GetType() == typeof(Bug))
            {
                Bug localTask = (Bug)chosenTask;
                if (localTask.user != null)
                {
                    Messages.Wrong("На эту задачу уже назначен исполнитель.");
                    ContinueMessage();
                    return;
                }
            }
            // Выбор пользователя для добавления в задачу.
            for (int i = 0; i < allUsers.Count; i++)
                Console.WriteLine($"{i + 1}. {allUsers[i]}");
            Console.WriteLine();
            int indexUser = RequestInt("Выберите номер пользователя, которого вы хотите добавить: ");
            if (indexUser < 1 || indexUser > allUsers.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            User chosenUser = allUsers[indexUser - 1];
            // Добавление пользователя.
            if (chosenTask.GetType() == typeof(Story))
            {
                Story localTask = (Story)chosenTask;
                foreach (var user in localTask.users)
                {
                    if (user.name == chosenUser.name)
                    {
                        Messages.Wrong("Пользователь уже назначен на задачу.");
                        ContinueMessage();
                        return;
                    }
                }
                localTask.users.Add(chosenUser);
            }
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                localTask.user = chosenUser;
            }
            if (chosenTask.GetType() == typeof(Bug))
            {
                Bug localTask = (Bug)chosenTask;
                localTask.user = chosenUser;
            }
            // Финальное сообщение.
            Messages.Correct($"В задачу {chosenTask.name} был добавлен исполнитель {chosenUser.name}");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void RemoveUser(Project currentProject)
        {
            // Проверка исключений.
            if (currentProject.tasks.Count == 0)
            {
                Messages.Wrong("Задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Выбор задачи для удаления пользователя.
            for (int i = 0; i < currentProject.tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {currentProject.tasks[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, из которой вы хотите удалить исполнителя: ");
            if (index < 1 || index > currentProject.tasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Отбор случаев с некорректно выбранной задачей.
            MainTask chosenTask = currentProject.tasks[index - 1];
            if (chosenTask.GetType() == typeof(Epic))
            {
                Messages.Wrong("У Epic задачи нет исполнителей.");
                ContinueMessage();
                return;
            }
            // Отбор исключений.
            if (chosenTask.GetType() == typeof(Story))
            {
                Story localTask = (Story)chosenTask;
                if (localTask.users.Count == 0)
                {
                    Messages.Wrong("У этой задачи нет исполнителей.");
                    ContinueMessage();
                    return;
                }
            }
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                if (localTask.user == null)
                {
                    Messages.Wrong("На эту задачу не назначен исполнитель.");
                    ContinueMessage();
                    return;
                }
            }
            if (chosenTask.GetType() == typeof(Bug))
            {
                Bug localTask = (Bug)chosenTask;
                if (localTask.user == null)
                {
                    Messages.Wrong("На эту задачу не назначен исполнитель.");
                    ContinueMessage();
                    return;
                }
            }
            // Выбор пользователя для удаления из задачи Story.
            if (chosenTask.GetType() == typeof(Story))
            {
                Story localTask = (Story)chosenTask;
                for (int i = 0; i < localTask.users.Count; i++)
                    Console.WriteLine($"{i + 1}. {allUsers[i]}");
                Console.WriteLine();
                int indexUser = RequestInt("Выберите номер пользователя, которого вы хотите удалить: ");
                if (indexUser < 1 || indexUser > localTask.users.Count)
                {
                    Messages.Wrong("Некорректный номер.");
                    ContinueMessage();
                    return;
                }
                localTask.users.RemoveAt(indexUser - 1);
            }
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                localTask.user = null;
            }
            if (chosenTask.GetType() == typeof(Bug))
            {
                Bug localTask = (Bug)chosenTask;
                localTask.user = null;
            }
            // Финальное сообщение.
            Messages.Correct($"У задачи {chosenTask.name} был удалён исполнитель.");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Добавление подзадачи в эпическую задачу.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void AddEpicSubtask(Project currentProject)
        {
            // Выборка всех эпических задач.
            List<Epic> allEpic = new List<Epic>();
            foreach (var task in currentProject.tasks)
            {
                if (task.GetType() == typeof(Epic))
                    allEpic.Add((Epic)task);
            }
            // Проверка исключений.
            if (allEpic.Count == 0)
            {
                Messages.Wrong("Эпические задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Выбор задачи для добавления подзадачи.
            for (int i = 0; i < allEpic.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, в которую вы хотите добавить подзадачу: ");
            if (index < 1 || index > allEpic.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Выбор подзадачи.
            Console.WriteLine($"1. Story");
            Console.WriteLine($"2. Task");
            Console.WriteLine();
            int indexSubtask = RequestInt("Выберите подзадачу, которую вы хотите добавить: ");
            if (indexSubtask < 0 || indexSubtask > 2)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Создание имени подзадачи.
            string name = RequestString("Введите имя новой подзадачи: ").Trim();
            foreach (var task in allEpic[index - 1].subtasks)
            {
                if (task.name == name)
                {
                    Messages.Wrong("Подзадача с таким именем уже существует.");
                    ContinueMessage();
                    return;
                }
            }
            // Создание и добавление подзадачи.
            var creationDate = DateTime.Now;
            if (indexSubtask == 1)
                allEpic[index - 1].subtasks.Add(new Story(name, creationDate));
            else if (indexSubtask == 2)
                allEpic[index - 1].subtasks.Add(new Task(name, creationDate));
            // Финальное сообщение.
            Messages.Correct("Задача была успешно добавлена.");
            Console.WriteLine();
            Messages.Info("Добавить и изменить исполнителей можно в соотвествующем пункте меню.");
            Messages.Info("Изменить статус задачи также можно в соотвествующем пункте меню.");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Удаление подзадачи из эпической задачи.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void RemoveEpicSubtask(Project currentProject)
        {
            // Выборка всех эпических задач.
            List<Epic> allEpic = new List<Epic>();
            foreach (var task in currentProject.tasks)
            {
                if (task.GetType() == typeof(Epic))
                    allEpic.Add((Epic)task);
            }
            // Проверка исключений.
            if (allEpic.Count == 0)
            {
                Messages.Wrong("Эпические задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Выбор задачи для удаления подзадачи.
            for (int i = 0; i < allEpic.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, из которой вы хотите удалить подзадачу: ");
            if (index < 1 || index > allEpic.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Проверка исключений.
            if (allEpic[index - 1].subtasks.Count == 0)
            {
                Messages.Wrong("У данной эпической задачи отсутствуют подзадачи.");
                Messages.Wrong("Бесполезная какая-то. Удали её, а то раздражает :_:.");
                ContinueMessage();
                return;
            }
            // Выбор подзадачи для удаления.
            for (int i = 0; i < allEpic[index - 1].subtasks.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[index - 1].subtasks[i]}");
            Console.WriteLine();
            int indexSubtask = RequestInt("Выберите номер подзадачи, которую вы хотите удалить: ");
            if (indexSubtask < 1 || indexSubtask > allEpic[index - 1].subtasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Удаление подзадачи и финальное сообщение.
            allEpic[index - 1].subtasks.RemoveAt(indexSubtask - 1);
            Messages.Correct("Подзадача была успешно удалена.");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Добавление исполнителя в эпическую задачу..
        /// </summary>
        /// <param name="currentProject"></param>
        public static void AddEpicUser(Project currentProject)
        {
            // Выборка всех эпических задач.
            List<Epic> allEpic = new List<Epic>();
            foreach (var task in currentProject.tasks)
            {
                if (task.GetType() == typeof(Epic))
                    allEpic.Add((Epic)task);
            }
            // Проверка исключений.
            if (allEpic.Count == 0)
            {
                Messages.Wrong("Эпические задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            if (allUsers.Count == 0)
            {
                Messages.Wrong("Не существует ни одного пользователя.");
                ContinueMessage();
                return;
            }
            // Выбор задачи для выбора подзадачи.
            for (int i = 0; i < allEpic.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, в которую вы хотите добавить исполнителя: ");
            if (index < 1 || index > allEpic.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Проверка исключений.
            if (allEpic[index - 1].subtasks.Count == 0)
            {
                Messages.Wrong("У данной эпической задачи отсутствуют подзадачи.");
                ContinueMessage();
                return;
            }
            // Выбор подзадачи для добавления пользователя.
            for (int i = 0; i < allEpic[index - 1].subtasks.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[index - 1].subtasks[i]}");
            Console.WriteLine();
            int indexSubtask = RequestInt("Выберите номер подзадачи, в которую вы хотите добавить исполнителя: ");
            if (indexSubtask < 1 || indexSubtask > allEpic[index - 1].subtasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Отбор случаев с некорректно выбранной задачей.
            MainTask chosenTask = allEpic[index - 1].subtasks[indexSubtask - 1];
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                if (localTask.user != null)
                {
                    Messages.Wrong("На эту задачу уже назначен исполнитель.");
                    ContinueMessage();
                    return;
                }
            }
            // Выбор пользователя для добавления в задачу.
            for (int i = 0; i < allUsers.Count; i++)
                Console.WriteLine($"{i + 1}. {allUsers[i]}");
            Console.WriteLine();
            int indexUser = RequestInt("Выберите номер пользователя, которого вы хотите добавить: ");
            if (indexUser < 1 || indexUser > allUsers.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            User chosenUser = allUsers[indexUser - 1];
            // Добавление пользователя.
            if (chosenTask.GetType() == typeof(Story))
            {
                Story localTask = (Story)chosenTask;
                foreach (var user in localTask.users)
                {
                    if (user.name == chosenUser.name)
                    {
                        Messages.Wrong("Пользователь уже назначен на задачу.");
                        ContinueMessage();
                        return;
                    }
                }
                localTask.users.Add(chosenUser);
            }
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                localTask.user = chosenUser;
            }
            // Финальное сообщение.
            Messages.Correct($"В задачу {chosenTask.name} был добавлен исполнитель {chosenUser.name}");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Удаление исполнителя из эпической задачи.
        /// </summary>
        /// <param name="currentProject"></param>
        public static void RemoveEpicUser(Project currentProject)
        {
            // Выборка всех эпических задач.
            List<Epic> allEpic = new List<Epic>();
            foreach (var task in currentProject.tasks)
            {
                if (task.GetType() == typeof(Epic))
                    allEpic.Add((Epic)task);
            }
            // Проверка исключений.
            if (allEpic.Count == 0)
            {
                Messages.Wrong("Эпические задачи в проекте отсутствуют.");
                ContinueMessage();
                return;
            }
            // Выбор задачи для выбора подзадачи.
            for (int i = 0; i < allEpic.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[i]}");
            Console.WriteLine();
            int index = RequestInt("Выберите номер задачи, в которую вы хотите добавить исполнителя: ");
            if (index < 1 || index > allEpic.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Проверка исключений.
            if (allEpic[index - 1].subtasks.Count == 0)
            {
                Messages.Wrong("У данной эпической задачи отсутствуют подзадачи.");
                ContinueMessage();
                return;
            }
            // Выбор подзадачи для удаления пользователя.
            for (int i = 0; i < allEpic[index - 1].subtasks.Count; i++)
                Console.WriteLine($"{i + 1}. {allEpic[index - 1].subtasks[i]}");
            Console.WriteLine();
            int indexSubtask = RequestInt("Выберите номер подзадачи, из которой вы хотите удалить исполнителя: ");
            if (indexSubtask < 1 || indexSubtask > allEpic[index - 1].subtasks.Count)
            {
                Messages.Wrong("Некорректный номер.");
                ContinueMessage();
                return;
            }
            // Отбор случаев с некорректно выбранной подзадачей.
            MainTask chosenTask = allEpic[index - 1].subtasks[indexSubtask - 1];
            if (chosenTask.GetType() == typeof(Story))
            {
                Story localTask = (Story)chosenTask;
                if (localTask.users.Count == 0)
                {
                    Messages.Wrong("У этой задачи нет исполнителей.");
                    ContinueMessage();
                    return;
                }
            }
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                if (localTask.user == null)
                {
                    Messages.Wrong("На эту задачу не назначен исполнитель.");
                    ContinueMessage();
                    return;
                }
            }
            // Выбор пользователя для удаления из задачи Story.
            if (chosenTask.GetType() == typeof(Story))
            {
                Story localTask = (Story)chosenTask;
                for (int i = 0; i < localTask.users.Count; i++)
                    Console.WriteLine($"{i + 1}. {allUsers[i]}");
                Console.WriteLine();
                int indexUser = RequestInt("Выберите номер пользователя, которого вы хотите удалить: ");
                if (indexUser < 1 || indexUser > localTask.users.Count)
                {
                    Messages.Wrong("Некорректный номер.");
                    ContinueMessage();
                    return;
                }
                localTask.users.RemoveAt(indexUser - 1);
            }
            if (chosenTask.GetType() == typeof(Task))
            {
                Task localTask = (Task)chosenTask;
                localTask.user = null;
            }
            // Финальное сообщение.
            Messages.Correct($"У задачи {chosenTask.name} был удалён исполнитель.");
            ContinueMessage();
            return;
        }

        /// <summary>
        /// Сообщение - заглушка.
        /// </summary>
        public static void EmptySelection()
        {
            Console.WriteLine("Ну, сделай нормальный выбор, че ты начинаешь?");
            ContinueMessage();
            return;
        }

        /*
         *  БЛОК ДОПОЛНИТЕЛЬНЫХ МЕТОДОВ.
         */

        /// <summary>
        /// Сообщение "Для продолжения нажмите..."
        /// </summary>
        public static void ContinueMessage()
        {
            Console.WriteLine();
            Console.Write("Для продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Сообщение с выводом об ошибке.
        /// </summary>
        /// <param name="exception"></param>
        public static void ErrorMessage(Exception exception)
        {
            Console.WriteLine();
            Console.WriteLine("КОД ОШИБКИ");
            Console.WriteLine(exception);
        }

        /// <summary>
        /// Считывание строки от пользователя.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string RequestString(string message)
        {
            string answer;
            do
            {
                Console.Write(message);
                answer = Console.ReadLine();
            } while (answer == "");
            Console.WriteLine();
            return answer;
        }

        /// <summary>
        /// Считывание числа от ползователя.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int RequestInt(string message)
        {
            int answer;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out answer));
            Console.WriteLine();
            return answer;
        }

        /// <summary>
        /// Проверка корректности введенного имени.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spaceIsCorrect"></param>
        /// <returns></returns>
        public static bool CorrectName(string name, bool spaceIsCorrect = true)
        {
            foreach (var symbol in name)
            {
                if (!CorrectSymbol(symbol, spaceIsCorrect))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка корректности символа в введеном имени.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="spaceIsCorrect"></param>
        /// <returns></returns>
        public static bool CorrectSymbol(char symbol, bool spaceIsCorrect = true)
        {
            if ('a' <= symbol && symbol <= 'z')
                return true;
            if ('A' <= symbol && symbol <= 'Z')
                return true;
            if ('а' <= symbol && symbol <= 'я')
                return true;
            if ('А' <= symbol && symbol <= 'Я')
                return true;
            if ('0' <= symbol && symbol <= '9')
                return true;
            if (symbol == ' ' && spaceIsCorrect)
                return true;
            return false;
        }

        /// <summary>
        /// Функция сохранения пользователей и проектов.
        /// </summary>
        public static void Save()
        {
            // Символ-разделитель директорий.
            char separator = Path.DirectorySeparatorChar;
            // Пересоздание директорий.
            DirectoryInfo projectDirectory = new DirectoryInfo("Save");
            if (projectDirectory.Exists)
                projectDirectory.Delete(true);
            Directory.CreateDirectory("Save");
            Directory.CreateDirectory("Save" + separator + "Projects");
            // Сохранение пользователей.
            string path = "Save" + separator + "users.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var user in allUsers)
                    sw.WriteLine(user.name);
            }
            // Сохранение проектов.
            foreach (var project in allProjects)
            {
                path = "Save" + separator + "Projects" + separator + project.name + ".txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(project.capacity);
                    // Сохранение задач проекта.
                    foreach (MainTask task in project.tasks)
                    {
                        sw.WriteLine($"T {task}");
                        if (task.GetType() == typeof(Epic))
                        {
                            Epic localTask = (Epic)task;
                            foreach (var subtask in localTask.subtasks)
                                sw.WriteLine($"S {subtask}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Функция загрузки пользователей и проектов.
        /// </summary>
        public static void Load()
        {
            // Символ-разделитель директорий и чиста массивов.
            char separator = Path.DirectorySeparatorChar;
            allUsers.Clear();
            allProjects.Clear();
            // Загрузка пользователей.
            string path = "Save" + separator + "users.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                string user;
                while ((user = sr.ReadLine()) != null)
                {
                    allUsers.Add(new User(user));
                }
            }
            // Загрузка проектов.
            path = "Save" + separator + "Projects";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var item in dir.GetFiles())
            {
                string projectName = item.Name.Substring(0, item.Name.Length - 4);
                List<MainTask> projectTasks = new List<MainTask>();
                int projectCapacity;
                // Загрузка проекта.
                using (StreamReader sr = new StreamReader(item.FullName))
                {
                    projectCapacity = int.Parse(sr.ReadLine());
                    string task;
                    Epic lastEpic = new Epic("skip", new DateTime());
                    // Загрузка задач проекта.
                    while ((task = sr.ReadLine()) != null)
                    {
                        if (task.Split()[1] == "Epic")
                        {
                            lastEpic = (Epic)ParseTask(task);
                            projectTasks.Add(lastEpic);
                        }
                        else if (task.Split()[0] == "S")
                            lastEpic.subtasks.Add(ParseTask(task));
                        else
                            projectTasks.Add(ParseTask(task));
                    }
                }
                allProjects.Add(new Project(projectName, projectTasks, projectCapacity));
            }
        }

        /// <summary>
        /// Вспомогательная функция для прочтения
        /// одной строки из сохранения проекта.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static MainTask ParseTask(string task)
        {
            // Введение основных переменных.
            string taskType = task.Split()[1];
            string[] taskArray = task.Split("|");
            // Дальше идет парсинг задачи в зависимости от типа.
            // T Epic | Build monument | 12.02.2021 14:02:04 | Открытая задача
            if (taskType == "Epic")
            {
                string name = taskArray[1].Trim();
                string date = taskArray[2].Trim();
                string status = taskArray[3].Trim();
                // Создание задачи.
                return new Epic(name, DateTime.Parse(date), status);
            }
            // T Story | Create iron | 12.02.2021 14:02:04 | Открытая задача | Исполнители: Mark, Ivan
            else if (taskType == "Story")
            {
                string name = taskArray[1].Trim();
                string date = taskArray[2].Trim();
                string status = taskArray[3].Trim();
                string[] users = taskArray[4].Trim().Split();
                // Создание задачи.
                Story answer = new Story(name, DateTime.Parse(date), status);
                for (int i = 1; i < users.Length; i++)
                {
                    answer.users.Add(new User(users[i].Trim(',')));
                }
                return answer;
            }
            // T Task | Create copper | 12.02.2021 14:02:04 | Открытая задача | Исполнитель: Mark
            else if (taskType == "Task")
            {
                string name = taskArray[1].Trim();
                string date = taskArray[2].Trim();
                string status = taskArray[3].Trim();
                string user = taskArray[4].Trim();
                // Создание задачи.
                Task answer = new Task(name, DateTime.Parse(date), status);
                if (user.Split().Length == 2)
                    answer.user = new User(user.Split()[1]);
                return answer;
            }
            // T Bug | Unknown buggy | 12.02.2021 14:02:04 | Открытая задача | Исполнитель:
            else
            {
                string name = taskArray[1].Trim();
                string date = taskArray[2].Trim();
                string status = taskArray[3].Trim();
                string user = taskArray[4].Trim();
                // Создание задачи.
                Bug answer = new Bug(name, DateTime.Parse(date), status);
                if (user.Split().Length == 2)
                    answer.user = new User(user.Split()[1]);
                return answer;
            }
        }

        /*
         *  БЛОК МЕТОДА MAIN.
         */

        /// <summary>
        /// Основной метод программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            bool continueOperation = true;
            while (continueOperation)
            {
                try
                {
                    Load();
                    Messages.Info(File.ReadAllText("Title.txt", Encoding.UTF8));
                    ContinueMessage();
                    Menu();
                    continueOperation = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Упс, что-то пошло не так (O_o)");
                    ErrorMessage(exception);
                    ContinueMessage();
                }
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Save();
        }
    }
}

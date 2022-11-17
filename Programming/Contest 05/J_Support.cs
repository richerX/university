using System;
using System.Collections.Generic;

public class Support
{
    private readonly List<Task> tasks = new List<Task>();

    public IEnumerable<Task> Tasks => tasks;

    // open | <текст обращени€> Ц открыть новое обращение и получить его идентификационный номер;
    public int OpenTask(string text)
    {
        tasks.Add(new Task(tasks.Count + 1, text));
        return tasks.Count;
    }

    // close | <id обращени€> | <ответ технической поддержки на обращение> Ц ответить на обращение и закрыть его;
    public void CloseTask(int id, string answer)
    {
        tasks[id - 1].Answer = answer;
        tasks[id - 1].IsResolved = true;
    }

    // get unresolved Ц получить информацию обо всех необработанных обращени€;
    public List<Task> GetAllUnresolvedTasks()
    {
        var unresolvedTasks = new List<Task>();
        foreach (var elem in tasks)
        {
            if (!elem.IsResolved)
                unresolvedTasks.Add(elem);
        }
        return unresolvedTasks;
    }

    // close unresolved | <ответ технической поддержки на все необработанные обращени€> Ц ответить на все необработанные обращени€ и закрыть их;
    public void CloseAllUnresolvedTasksWithDefaultAnswer(string answer)
    {
        foreach (var elem in tasks)
        {
            if (!elem.IsResolved)
                CloseTask(elem.Id, answer);
        }
    }

    // info | <id обращени€> Ц получить информацию об обращении по идентификационному номеру;
    public string GetTaskInfo(int id)
    {
        return tasks[id - 1].ToString();
    }
}
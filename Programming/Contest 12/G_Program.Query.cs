using System;
using System.Collections.Generic;
using System.Linq;

public partial class Program
{
    // 1. Сформировать группы юзеров по их имени, предварительно отсортировав их по возрасту в убывающем порядке
    // (от большего к меньшему).

    // 2. Отфильтровать группы: оставить в группах только тех пользователей, чей возраст строго больше 10.

    // 3. Из каждой группы взять m пользователей, предварительно отсортировав по Id,
    // и вывести на экран сумму возрастов таких пользователей.

    private static IEnumerable<IGrouping<string, User>> GetGroups(List<User> users)
    {
        var sortedByAge = from t in users orderby t.age descending select t;
        var groups = from t in sortedByAge group t by t.name;
        var sortedGroups = from t in groups from t2 in t where t2.age > 10 group t2 by t2.name;
        return groups;
    }

    private static List<int> GetQueryResult(IEnumerable<IGrouping<string, User>> groupsLinq, int m)
    {
        var answer = new List<int>();
        var groups = groupsLinq.ToDictionary(x => x.Key);
        List<User> usersList = groupsLinq.SelectMany(x => x).ToList();
        Dictionary<string, List<User>> users = new Dictionary<string, List<User>>();

        foreach (var user in usersList)
        {
            if (!users.ContainsKey(user.name))
                users.Add(user.name, new List<User>());
            if (user.age > 10)
                users[user.name].Add(user);
        }

        List<User> currentList;
        int currentCount;
        foreach (var key in users.Keys)
        {
            currentCount = 0;
            currentList = users[key];
            currentList.Sort((x, y) => x.id.CompareTo(y.id));

            int length = Math.Min(m, currentList.Count);
            for (int i = 0; i < length; i++)
                currentCount += currentList[i].age;
            answer.Add(currentCount);

            /*
            Console.WriteLine(key);
            foreach (var user in currentList)
                Console.WriteLine($"---->  {user}");
            Console.WriteLine(currentCount);
            Console.WriteLine();
            */
        }

        return answer;
    }
}
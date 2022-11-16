using System;
using System.Collections.Generic;
using System.Linq;

public partial class Program
{
    // Сортируем по возрастанию лексикографическому города и создаём на основе их группы.
    // Далее сортируем такие группы по количеству в них пользователей. Сортировка убавающая
    // Берём пять первых групп, не включая первую.
    // В каждой такой группе необходимо сгруппировать пользователей по имени, и уже каждую такую группу преобразовать в число, равное среднему возрасту этой группы.

    private static double GetAverage(IEnumerable<User> usersLinq)
    {
        // Сортируем по возрастанию лексикографическому города
        var users = usersLinq.ToList();
        users.Sort((x, y) => x.city.CompareTo(y.city));

        // и создаём на основе их группы.
        Dictionary<string, List<User>> usersByTown = new Dictionary<string, List<User>>();
        foreach (var user in users)
        {
            if (!usersByTown.ContainsKey(user.city))
                usersByTown.Add(user.city, new List<User>());
            usersByTown[user.city].Add(user);
        }

        // Далее сортируем такие группы по количеству в них пользователей. Сортировка убавающая
        var usersByTownSorted = new List<(string, List<User>)>();
        foreach (var key in usersByTown.Keys)
            usersByTownSorted.Add((key, usersByTown[key]));
        usersByTownSorted.Sort((x, y) => (-(x.Item2.Count)).CompareTo(-(y.Item2.Count)));

        // Берём пять первых групп, не включая первую.
        int length = Math.Min(5, usersByTownSorted.Count);
        var pickedGroups = new List<(string, List<User>)>();
        for (int i = 1; i < length; i++)
            pickedGroups.Add(usersByTownSorted[i]);

        // В каждой такой группе необходимо сгруппировать пользователей по имени,
        List<int> ages = new List<int>();
        foreach (var group in pickedGroups)
            ages.Add(GetPodgroupMaxAge(group.Item2));

        // Имея коллекцию чисел, её необходимо перевернуть и отбросить одинаковые значения
        Dictionary<int, int> uniqueAges = new Dictionary<int, int>();
        foreach (var age in ages)
            if (!uniqueAges.ContainsKey(age))
                uniqueAges.Add(age, 0);

        // преобразовать в число, равное среднему возрасту этой группы.
        double answer = 0;
        foreach (var number in uniqueAges.Keys)
            answer += number;

        if (uniqueAges.Keys.Count == 0)
            return 0;
        return answer / uniqueAges.Keys.Count;
    }

    public static int GetPodgroupMaxAge(List<User> users)
    {
        Dictionary<string, int> names = new Dictionary<string, int>();
        foreach (var user in users)
        {
            if (!names.ContainsKey(user.name))
                names.Add(user.name, 0);
            names[user.name] += user.age;
        }

        int answer = 0;
        foreach (var key in names.Keys)
            if (names[key] > answer)
                answer = names[key];

        return answer;
    }

}
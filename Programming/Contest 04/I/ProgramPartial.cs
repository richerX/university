using System.Security.Cryptography.X509Certificates;

partial class Program
{
    private static int ElementCount(string line, char elem)
    {
        int answer = 0;
        foreach (var sign in line)
        {
            if (sign == elem)
            {
                answer += 1;
            }
        }
        return answer;
    }

    private static char GetMinimumElement(string input)
    {
        int minimum = int.MaxValue;
        char minimumElement = 'a';
        int current;
        foreach (var elem in input)
        {
            current = ElementCount(input, elem);
            if (current < minimum)
            {
                minimum = current;
                minimumElement = elem;
            }
        }
        return minimumElement;
    }

    private static char GetMaximumElement(string input)
    {
        int maximum = int.MinValue;
        char maximumElement = 'z';
        int current;
        foreach (var elem in input)
        {
            current = ElementCount(input, elem);
            if (current > maximum)
            {
                maximum = current;
                maximumElement = elem;
            }
        }
        return maximumElement;
    }

    private static string Encrypt(string input)
    {
        string answer = "";
        char minimumElement = GetMinimumElement(input);
        char maximumElement = GetMaximumElement(input);
        foreach (var elem in input)
        {
            if (elem == minimumElement || elem == maximumElement)
            {
                if (elem == minimumElement)
                {
                    answer += maximumElement;
                }
                else
                {
                    answer += minimumElement;
                }
            }
            else
            {
                answer += elem;
            }
        }
        return answer;
    }
}

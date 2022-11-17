partial class Program
{
    private static bool Validate(string input, out int num)
    {
        if (!int.TryParse(input, out num) || num < 0)
        {
            return false;
        }
        return true;
    }

    private static int RecurrentFunction(int n)
    {
        int answer = 3;
        for (int i = 0; i < n; i++)
        {
            answer = 2 * (answer + 1);
        }
        return answer;
    }
}
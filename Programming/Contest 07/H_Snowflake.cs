using System;

public class Snowflake
{
    public int n;
    public int rays;
    public string[,] matrix;

    public Snowflake(int widthAndHeight, int raysCount)
    {
        if (widthAndHeight < 1 || widthAndHeight % 2 == 0)
            throw new ArgumentException("Incorrect input");
        if (raysCount < 4 || (raysCount & (raysCount - 1)) != 0)
            throw new ArgumentException("Incorrect input");
        n = widthAndHeight;
        rays = raysCount;
        matrix = new string[n, n];
        Fill();
    }

    public override string ToString()
    {
        string answer = "";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n - 1; j++)
                answer += $"{matrix[i, j]} ";
            answer += $"{matrix[i, n - 1]}";
            if (i != n - 1)
                answer += Environment.NewLine;
        }
        return answer;
    }

    public void Fill()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                matrix[i, j] = " ";
        }

        int average = n / 2;
        for (int i = 0; i < n; i++)
        {
            matrix[i, average] = "*";
            matrix[average, i] = "*";
        }

        int count = 4;
        for (int i = 0; i <= n / 2; i += 2)
        {
            if (count >= rays)
                break;

            // Левый
            SpecialFill(i, n / 2 - i, n / 2, -1, -1);
            SpecialFill(i, n / 2 - i, n / 2, -1, 1);

            // Право
            SpecialFill(i, n / 2 + i, n / 2, 1, -1);
            SpecialFill(i, n / 2 + i, n / 2, 1, 1);

            // Верх
            SpecialFill(i, n / 2, n / 2 - i, -1, -1);
            SpecialFill(i, n / 2, n / 2 - i, 1, -1);

            // Низ
            SpecialFill(i, n / 2, n / 2 + i, -1, 1);
            SpecialFill(i, n / 2, n / 2 + i, 1, 1);

            if (i == 0)
                count += 4;
            else
                count += 8;
        }
    }

    public void SpecialFill(int i, int x, int y, int dx, int dy)
    {
        while (0 <= x && x < n && 0 <= y && y < n)
        {
            matrix[y, x] = "*";
            x += dx;
            y += dy;
        }
    }
}
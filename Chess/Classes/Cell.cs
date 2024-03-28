using System;
using UnityEngine;
using static Constants;


public class Cell
{
    public int i, j;

    public Cell(int i, int j)
    {
        this.i = i;
        this.j = j;
    }

    // Примагничивание к ближайшей клетке
    public static void Centralize(Transform transform)
    {
        MoveToCoord(transform, GetCellCoord(transform.position));
    }

    public static void MoveToCoord(Transform transform, Vector2 finalPosition)
    {
        Vector2 currentPosition = (Vector2)transform.position;
        Vector2 shift = new Vector2(finalPosition[0] - currentPosition[0], finalPosition[1] - currentPosition[1]);
        transform.Translate(shift);
    }

    public static Cell GetCell(Transform transform)
    {
        return InternalSearch(transform.position).Item1;
    }

    public static Vector2 GetCellCoord(Cell cell)
    {
        return GetCellCoord(cell.i, cell.j);
    }

    public static Vector2 GetCellCoord(int i, int j)
    {
        return new Vector2(initialCorner + i * delta, initialCorner + j * delta);
    }

    public static Vector2 GetCellCoord(Vector2 position)
    {
        return InternalSearch(position).Item2;
    }

    public static string GetCellName(Cell cell)
    {
        return GetCellName(cell.i, cell.j);
    }

    public static string GetCellName(int i, int j)
    {
        return $"{(char)('A' + i)}{j + 1}";
    }

    // Поиск ближайшей клетки
    private static Tuple<Cell, Vector2> InternalSearch(Vector2 initial)
    {
        float x = initialCorner, y = initialCorner;
        Vector2 current = new Vector2(x, y);
        float minimum = GetDistance(initial, current);
        var answer = new Tuple<Cell, Vector2>(new Cell(0, 0), current);

        for (int j = 0; j < 8; j++)
        {
            x = initialCorner;
            for (int i = 0; i < 8; i++)
            {
                current = new Vector2(x, y);
                if (GetDistance(initial, current) < minimum)
                {
                    minimum = GetDistance(initial, current);
                    answer = new Tuple<Cell, Vector2>(new Cell(i, j), current);
                }
                x += delta;
            }
            y += delta;
        }

        return answer;
    }

    public static float GetDistance(Vector2 vector1, Vector2 vector2)
    {
        double x = Math.Pow(vector1[0] - vector2[0], 2);
        double y = Math.Pow(vector1[1] - vector2[1], 2);
        return (float)Math.Sqrt(x + y);
    }
}

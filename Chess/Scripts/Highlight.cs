using System.Collections.Generic;
using UnityEngine;
using static Constants;
using static Cell;
using static Validator;


public static class Highlight
{
    public static List<Cell> highlights = new List<Cell>();

    public static void HighlightClear()
    {
        highlights.Clear();
        Update();
    }

    public static void HighlightUpdate(Transform transform)
    {
        Cell startCell = GetCell(transform);
        Validator.logging = false;
        highlights.Clear();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var current = new Cell(i, j);
                if (ValidMove(new Move(transform, startCell, current)))
                    highlights.Add(current);
            }
        }
        Validator.logging = true;
        Update();
    }

    private static void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject gameObject = GameObject.Find($"{GetCellName(i, j)}");
                MeshRenderer meshRender = gameObject.GetComponent<MeshRenderer>();
                if ((i + j) % 2 == 1)
                    meshRender.material.color = darkDefault;
                else
                    meshRender.material.color = lightDefault;
                foreach (var elem in highlights)
                {
                    if (elem.i == i && elem.j == j)
                    {
                        if ((i + j) % 2 == 1)
                            meshRender.material.color = darkOk;
                        else
                            meshRender.material.color = lightOk;
                    }
                }
            }
        }
    }
}

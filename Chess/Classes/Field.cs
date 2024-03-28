using UnityEngine;
using static Constants;
using static Cell;


public static class Field
{
    public static Piece[,] field = new Piece[8, 8];

    public static void AddPieceToField(Piece piece, Cell cell)
    {
        if (cell.i == -1 || cell.j == -1)
            Debug.Log($"[AddPieceToField] Incorrect cell, piece = {piece}, i = {cell.i}, j = {cell.j}");
        else
            field[cell.i, cell.j] = piece;
    }

    public static void MovePieceOnField(Cell start, Cell finish)
    {
        field[finish.i, finish.j] = field[start.i, start.j];
        field[start.i, start.j] = Piece.none;
    }

    public static void PrintField()
    {
        string str = "";
        for (int j = 7; j > -1; j--)
        {
            for (int i = 0; i < 8; i++)
            {
                str += field[i, j].ToString().PadRight(15, ' ');
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    public static void CreateUI()
    {
        Shader squareShader = Shader.Find("Unlit/Color");
        Transform parent = GameObject.Find($"Board").transform;

        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                gameObject.transform.localScale = new Vector2(delta, delta);
                gameObject.layer = 2;

                Transform square = gameObject.transform;
                square.parent = parent;
                square.name = GetCellName(i, j);
                square.position = new Vector2(initialCorner + delta * i, initialCorner + delta * j);

                MeshRenderer meshRender = square.gameObject.GetComponent<MeshRenderer>();
                meshRender.material = new Material(squareShader);

                if ((i + j) % 2 == 1)
                    meshRender.material.color = darkDefault;
                else
                    meshRender.material.color = lightDefault;
            }
        }
    }

    public static void CreateBoardUI()
    {
        Shader squareShader = Shader.Find("Unlit/Color");
        Transform parent = GameObject.Find($"Board").transform;
        float size = 8.15f;

        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        gameObject.transform.localScale = new Vector2(delta * size, delta * size);
        gameObject.layer = 1;

        Transform square = gameObject.transform;
        square.parent = parent;
        square.name = "Main Board";
        square.position = new Vector2(0, 0);

        MeshRenderer meshRender = square.gameObject.GetComponent<MeshRenderer>();
        meshRender.material = new Material(squareShader);
        meshRender.material.color = boardDefault;
        meshRender.material.renderQueue = 1500;
    }
}

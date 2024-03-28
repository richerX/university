using System;
using UnityEngine;
using static Constants;
using static Field;
using static Cell;
using static Game;
using static Validator;


public class Move
{
    public Transform transform;
    public Cell startCell, targetCell;
    public int dx, dy;

    public Move(Transform transform, Cell startCell, Cell targetCell)
    {
        this.transform = transform;
        this.startCell = startCell;
        this.targetCell = targetCell;
        dx = targetCell.i - startCell.i;
        dy = targetCell.j - startCell.j;
    }

    public void RunMove()
    {
        if (ValidMove(this))
        {
            sounds[0].Play();
            ChangeTurn();
            EatEnemyPiece();
            MovePieceOnField(startCell, targetCell);
            CastlingMoveProcessing();
            Debug.Log($"[✔] {transform.name} ({GetCellName(startCell)} -> {GetCellName(targetCell)})");
        }
        else
        {
            var log = ValidDegubLog();
            MoveToCoord(transform, GetCellCoord(startCell));
            Centralize(transform);
            Debug.Log($"[Х] {transform.name} ({GetCellName(startCell)} -> {GetCellName(targetCell)})\n" + log);
        }
        Debug.Log("\n");
    }

    public void CastlingMoveProcessing()
    {
        if (transform.name == "WhiteKing" && whiteCastling && dx == 2)
        {
            MovePieceOnField(new Cell(7, 0), new Cell(5, 0));
            MoveToCoord(GameObject.Find("WhiteRook2").transform, GetCellCoord(5, 0));
        }

        if (transform.name == "BlackKing" && blackCastling && dx == 2)
        {
            MovePieceOnField(new Cell(7, 7), new Cell(5, 7));
            MoveToCoord(GameObject.Find("BlackRook2").transform, GetCellCoord(5, 7));
        }

        if (transform.name == "WhiteRook2" || transform.name == "WhiteKing")
            whiteCastling = false;
        if (transform.name == "BlackRook2" || transform.name == "BlackKing")
            blackCastling = false;
    }

    private void EatEnemyPiece()
    {
        if (field[targetCell.i, targetCell.j] != Piece.none)
        {
            sounds[1].Play();
            GameObject enemyPiece = GameObject.Find(field[targetCell.i, targetCell.j].ToString());

            if (enemyPiece.transform.name.Contains("King"))
            {
                ChangeTurn();
                TurnTextBoxScript.text = $"{currentTurn} Win!";
                gameIsOn = false;
            }

            var currentParking = enemyPiece.transform.name.Contains("Black") ? blackParking : whiteParking;
            var currentDelta = enemyPiece.transform.name.Contains("Black") ? -delta : delta;
            MoveToCoord(enemyPiece.transform, new Vector2(currentParking[0], currentParking[1]));
            piecesOnParking.Add(enemyPiece.transform.name);
            currentParking[0] += delta;
            if (currentParking[0] > 8)
            {
                currentParking[0] = initialCorner + delta * parkingFloat;
                currentParking[1] += currentDelta;
            }
        }
    }
}

using System;
using UnityEngine;
using static Constants;
using static Field;
using static Cell;
using static PossibleMoves;
using static Game;


public static class Validator
{
    private static Transform transform;
    private static Cell startCell, targetCell;
    private static int dx, dy;
    public static bool logging = false;

    // Проверка валидности хода
    public static bool ValidMove(Move move)
    {
        transform = move.transform;
        startCell = move.startCell;
        targetCell = move.targetCell;
        dx = targetCell.i - startCell.i;
        dy = targetCell.j - startCell.j;
        if (ValidFinishCell() && ValidTurn() && ValidShift() && ValidPath())
            return true;
        return false;
    }

    // Финальная клетка занята фигурой своего цвета
    private static bool ValidFinishCell()
    {
        if (currentTurn == Turn.White && field[targetCell.i, targetCell.j].ToString().Contains("White"))
        {
            if (logging)
                Debug.Log($"[VALIDATOR] Finish cell is occupied, current turn = {currentTurn}, finish cell piece = {field[targetCell.i, targetCell.j]}");
            return false;
        }
        if (currentTurn == Turn.Black && field[targetCell.i, targetCell.j].ToString().Contains("Black"))
        {
            if (logging)
                Debug.Log($"[VALIDATOR] Finish cell is occupied, current turn = {currentTurn}, finish cell piece = {field[targetCell.i, targetCell.j]}");
            return false;
        }
        return true;
    }

    // Сейчас ходят фигуры другого цвета
    private static bool ValidTurn()
    {
        if (currentTurn == Turn.White && transform.name.Contains("Black"))
        {
            if (logging)
                Debug.Log($"[VALIDATOR] Invalid turn, current turn = {currentTurn}");
            return false;
        }
        if (currentTurn == Turn.Black && transform.name.Contains("White"))
        {
            if (logging)
                Debug.Log($"[VALIDATOR] Invalid turn, current turn = {currentTurn}");
            return false;
        }
        return true;
    }

    // Некорректное передвижение для фигуры
    private static bool ValidShift()
    {
        if (transform.name.Contains("Pawn") && !ValidPawnMove())
        {
            if (logging)
                Debug.Log($"[VALIDATOR] Invalid pawn move, name = {transform.name}, shift = ({dx}, {dy})");
            return false;
        }

        if (transform.name.Contains("King") && !ValidKingMove())
        {
            if (logging)
                Debug.Log($"[VALIDATOR] Invalid king move, name = {transform.name}, shift = ({dx}, {dy})");
            return false;
        }

        foreach (string name in possibleMoves.Keys)
        {
            if (transform.name.Contains(name) && !possibleMoves[name].Contains(new Tuple<int, int>(dx, dy)))
            {
                if (logging)
                    Debug.Log($"[VALIDATOR] Invalid move, name = {transform.name}, shift = ({dx}, {dy})");
                return false;
            }
        }

        return true;
    }

    // Недопустимое перешагивание через фигуры
    private static bool ValidPath()
    {
        if (transform.name.Contains("Knight") || transform.name.Contains("King"))
            return true;

        // Queen, Bishop, Rook, Pawn (x2 start)
        int di = dx == 0 ? 0 : dx / Math.Abs(dx);
        int dj = dy == 0 ? 0 : dy / Math.Abs(dy);
        int count = Math.Max(Math.Abs(dx), Math.Abs(dy)) - 1;

        for (int i = startCell.i + di, j = startCell.j + dj; count > 0; i += di, j += dj, count -= 1)
        {
            if (i >= 0 && i < 8 && j >= 0 && j < 8 && field[i, j] != Piece.none)
            {
                if (logging)
                    Debug.Log($"[VALIDATOR] Invalid path, cell = {GetCellName(i, j)}, piece = {field[i, j]}");
                return false;
            }
        }

        return true;
    }

    /// 
    /// Дополнительные функции
    /// 

    private static bool ValidPawnMove()
    {
        // Запрет есть вперед
        if (dx == 0 && field[targetCell.i, targetCell.j] != Piece.none)
            return false;

        // Запрет просто ходить по диагонали (не съедая фигуру)
        if (Math.Abs(dx) == 1 && field[targetCell.i, targetCell.j] == Piece.none)
            return false;

        // Запрет ходить на две клетки вперед не со старта 
        if (Math.Abs(dy) == 2 && startCell.j != 1 && startCell.j != 6)
            return false;

        return true;
    }

    // Проверка рокировки
    private static bool ValidKingMove()
    {
        // Запреты на рокировку
        if (dx == 2)
        {
            // Фигуры уже ходили, рокировка запрещена
            var currentCastling = transform.name.Contains("White") ? whiteCastling : blackCastling;
            if (!currentCastling)
                return false;

            // Заняты поля для рокировки
            int j = transform.name.Contains("White") ? 0 : 7;
            if (field[5, j] != Piece.none || field[6, j] != Piece.none)
                return false;

            // Попытка рокировки с другой фигурой
            if (transform.name.Contains("White") && (field[4, 0].ToString() != "WhiteKing" || field[7, 0].ToString() != "WhiteRook2"))
                return false;
            if (transform.name.Contains("Black") && (field[4, 7].ToString() != "BlackKing" || field[7, 7].ToString() != "BlackRook2"))
                return false;
        }
        return true;
    }

    public static string ValidDegubLog()
    {
        Validator.logging = false;
        string log = "";
        log += $"({!ValidFinishCell()}) Финальная клетка занята фигурой своего цвета\n";
        log += $"({!ValidTurn()}) Сейчас ходят фигуры другого цвета\n";
        log += $"({!ValidShift()}) Некорректное передвижение для фигуры\n";
        log += $"({!ValidPath()}) Недопустимое перешагивание через фигуры";
        Validator.logging = true;
        return log;
    }
}

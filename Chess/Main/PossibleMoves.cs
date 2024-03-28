using System;
using System.Collections.Generic;


public static class PossibleMoves
{
    public static List<Tuple<int, int>> whitePawnsMoves = new List<Tuple<int, int>>();
    public static List<Tuple<int, int>> blackPawnsMoves = new List<Tuple<int, int>>();
    public static List<Tuple<int, int>> rooksMoves = new List<Tuple<int, int>>();
    public static List<Tuple<int, int>> bishopsMoves = new List<Tuple<int, int>>();
    public static List<Tuple<int, int>> knightsMoves = new List<Tuple<int, int>>();
    public static List<Tuple<int, int>> queensMoves = new List<Tuple<int, int>>();
    public static List<Tuple<int, int>> kingsMoves = new List<Tuple<int, int>>();

    public static Dictionary<string, List<Tuple<int, int>>> possibleMoves = new Dictionary<string, List<Tuple<int, int>>>() {
                                                               { "WhitePawn", whitePawnsMoves }, { "BlackPawn", blackPawnsMoves },
                                                               { "Rook", rooksMoves }, { "Bishop", bishopsMoves }, { "Knight", knightsMoves },
                                                               { "Queen", queensMoves }, { "King", kingsMoves }};

    public static void FillInMoves()
    {
        whitePawnsMoves.Add(new Tuple<int, int>(0, 1));
        whitePawnsMoves.Add(new Tuple<int, int>(0, 2));
        whitePawnsMoves.Add(new Tuple<int, int>(1, 1));
        whitePawnsMoves.Add(new Tuple<int, int>(-1, 1));

        blackPawnsMoves.Add(new Tuple<int, int>(0, -1));
        blackPawnsMoves.Add(new Tuple<int, int>(0, -2));
        blackPawnsMoves.Add(new Tuple<int, int>(1, -1));
        blackPawnsMoves.Add(new Tuple<int, int>(-1, -1));

        for (int i = 1; i < 8; i++)
        {
            rooksMoves.Add(new Tuple<int, int>(i, 0));
            rooksMoves.Add(new Tuple<int, int>(0, i));
            rooksMoves.Add(new Tuple<int, int>(-i, 0));
            rooksMoves.Add(new Tuple<int, int>(0, -i));
            queensMoves.Add(new Tuple<int, int>(i, 0));
            queensMoves.Add(new Tuple<int, int>(0, i));
            queensMoves.Add(new Tuple<int, int>(-i, 0));
            queensMoves.Add(new Tuple<int, int>(0, -i));
        }

        for (int i = 1; i < 8; i++)
        {
            bishopsMoves.Add(new Tuple<int, int>(i, i));
            bishopsMoves.Add(new Tuple<int, int>(i, -i));
            bishopsMoves.Add(new Tuple<int, int>(-i, i));
            bishopsMoves.Add(new Tuple<int, int>(-i, -i));
            queensMoves.Add(new Tuple<int, int>(i, i));
            queensMoves.Add(new Tuple<int, int>(i, -i));
            queensMoves.Add(new Tuple<int, int>(-i, i));
            queensMoves.Add(new Tuple<int, int>(-i, -i));
        }

        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -2; j <= 2; j += 4)
            {
                knightsMoves.Add(new Tuple<int, int>(i, j));
                knightsMoves.Add(new Tuple<int, int>(j, i));
            }
        }

        queensMoves.Add(new Tuple<int, int>(1, 0));
        queensMoves.Add(new Tuple<int, int>(0, 1));
        queensMoves.Add(new Tuple<int, int>(-1, 0));
        queensMoves.Add(new Tuple<int, int>(0, -1));
        queensMoves.Add(new Tuple<int, int>(1, 1));
        queensMoves.Add(new Tuple<int, int>(1, -1));
        queensMoves.Add(new Tuple<int, int>(-1, 1));
        queensMoves.Add(new Tuple<int, int>(-1, -1));

        kingsMoves.Add(new Tuple<int, int>(1, 0));
        kingsMoves.Add(new Tuple<int, int>(0, 1));
        kingsMoves.Add(new Tuple<int, int>(-1, 0));
        kingsMoves.Add(new Tuple<int, int>(0, -1));
        kingsMoves.Add(new Tuple<int, int>(1, 1));
        kingsMoves.Add(new Tuple<int, int>(1, -1));
        kingsMoves.Add(new Tuple<int, int>(-1, 1));
        kingsMoves.Add(new Tuple<int, int>(-1, -1));
        kingsMoves.Add(new Tuple<int, int>(2, 0));
    }
}

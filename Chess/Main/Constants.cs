using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
    public static float delta = 1f;
    public static float initialCorner = -3.5f * delta;

    public static float parkingFloat = 8.25f;
    public static List<float> blackParking = new List<float>() { initialCorner + delta * parkingFloat, -initialCorner };
    public static List<float> whiteParking = new List<float>() { initialCorner + delta * parkingFloat, initialCorner };
    public static List<string> piecesOnParking = new List<string>();

    public static Color boardDefault = new Color(148f / 255f, 102f / 255f, 89f / 255f);
    public static Color darkDefault = new Color(33f / 255f, 33f / 255f, 35f / 255f);
    public static Color lightDefault = new Color(171f / 255f, 171f / 255f, 171f / 255f);

    public static Color greenDarkOk = new Color(16f / 255f, 93f / 255f, 69f / 255f);
    public static Color greenLightOk = new Color(56f / 255f, 159f / 255f, 125f / 255f);
    public static Color blueDarkOk = new Color(24f / 255f, 68f / 255f, 112f / 255f);
    public static Color blueLightOk = new Color(90f / 255f, 124f / 255f, 159f / 255f);

    public static bool isGreen = false;
    public static Color darkOk = isGreen ? greenDarkOk : blueDarkOk;
    public static Color lightOk = isGreen ? greenLightOk : blueLightOk;

    public static bool whiteCastling = true;
    public static bool blackCastling = true;

    public static AudioSource[] sounds = Resources.FindObjectsOfTypeAll<AudioSource>();

    public enum Piece
    {
        none,
        BlackPawn1, BlackPawn2, BlackPawn3, BlackPawn4, BlackPawn5, BlackPawn6, BlackPawn7, BlackPawn8,
        BlackKnight1, BlackKnight2, BlackRook1, BlackRook2, BlackBishop1, BlackBishop2, BlackQueen, BlackKing,
        WhitePawn1, WhitePawn2, WhitePawn3, WhitePawn4, WhitePawn5, WhitePawn6, WhitePawn7, WhitePawn8,
        WhiteKnight1, WhiteKnight2, WhiteRook1, WhiteRook2, WhiteBishop1, WhiteBishop2, WhiteQueen, WhiteKing
    };

    public static List<List<Piece>> initialRows = new List<List<Piece>>() 
    { 
        new List<Piece>() { Piece.BlackRook1, Piece.BlackKnight1, Piece.BlackBishop1, Piece.BlackQueen,
                            Piece.BlackKing, Piece.BlackBishop2, Piece.BlackKnight2, Piece.BlackRook2},
        new List<Piece>() { Piece.BlackPawn1, Piece.BlackPawn2, Piece.BlackPawn3, Piece.BlackPawn4,
                            Piece.BlackPawn5, Piece.BlackPawn6, Piece.BlackPawn7, Piece.BlackPawn8},
        new List<Piece>() { Piece.WhitePawn1, Piece.WhitePawn2, Piece.WhitePawn3, Piece.WhitePawn4,
                            Piece.WhitePawn5, Piece.WhitePawn6, Piece.WhitePawn7, Piece.WhitePawn8 },
        new List<Piece>() { Piece.WhiteRook1, Piece.WhiteKnight1, Piece.WhiteBishop1, Piece.WhiteQueen,
                            Piece.WhiteKing, Piece.WhiteBishop2, Piece.WhiteKnight2, Piece.WhiteRook2 }
    };
}

public static class Game
{
    public static bool gameIsOn = true;

    public enum Turn { White, Black };
    public static Turn currentTurn = Turn.White;

    public static void ChangeTurn()
    {
        if (currentTurn == Turn.White)
            currentTurn = Turn.Black;
        else
            currentTurn = Turn.White;
        TurnTextBoxScript.text = $"Turn {currentTurn}";
    }
}

public class Battleship : Ship
{
    public override string ShipType => "Battleship";

    public Battleship()
    {
        Length = 4;
        Hit = new bool[] { false, false, false, false };
    }
}
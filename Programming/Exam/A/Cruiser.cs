public class Cruiser : Ship
{
    public override string ShipType => "Cruiser";

    public Cruiser()
    {
        Length = 3;
        Hit = new bool[] { false, false, false };
    }
}
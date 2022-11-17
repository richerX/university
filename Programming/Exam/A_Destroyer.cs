public class Destroyer : Ship
{
    public override string ShipType => "Destroyer";

    public Destroyer()
    {
        Length = 2;
        Hit = new bool[] { false, false };
    }
}
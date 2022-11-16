public class Submarine : Ship
{
    public override string ShipType => "Submarine";
    
    public Submarine()
    {
        Length = 1;
        Hit = new bool[] { false };
    }
}
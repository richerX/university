using System;
using System.Linq;

public class Battlefield
{
    private readonly Ship[,] ships;

    public Battlefield(Ship[,] ships)
    {
        this.ships = ships;
    }

    public string this[int x, int y]
    {
        get
        {
            var ship = ships[x, y];
            if (ship is null)
                return "miss";

            int hits = GetHits(ship);
            if (hits == ship.Length)
                return $"this {ship.ShipType} has already sunk";

            int hitIndex = -1;
            if (ship.IsHorizontal)
                hitIndex = y - ship.BowColumn;
            else
                hitIndex = x - ship.BowRow;
            if (ship.Hit[hitIndex])
                return $"this {ship.ShipType} has already shot";

            ship.Hit[hitIndex] = true;
            hits = GetHits(ship);
            if (hits == ship.Hit.Length)
                return $"{ship.ShipType} sunk";
            return $"{ship.ShipType} shot";
        }
    }

    public int GetHits(Ship ship)
    {
        int hits = 0;
        foreach (var elem in ship.Hit)
            if (elem)
                hits += 1;
        return hits;
    }
}
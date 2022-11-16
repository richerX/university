using System;

public static class GiftCreator
{
    public static int phones = -1;
    public static int consoles = -1;
    public static Gift CreateGift(string giftName)
    {
        if (giftName == "Phone")
        {
            phones += 1;
            return new Phone(phones);
        }
        consoles += 1;
        return new PlayStation(consoles);
    }
}

public class Phone : Gift
{
    public Phone(int id) : base(id)
    {
        ID = id;
    }
}

public class PlayStation : Gift
{
    public PlayStation(int id) : base(id)
    {
        ID = id;
    }
}

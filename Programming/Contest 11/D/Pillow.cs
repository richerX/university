using System;
using System.Xml.Serialization;


public class Pillow
{
    public long id;
    public string isRuined;

    //[XmlElement]
    //public string IsRuinedStr => isRuined ? "Yes" : "No";

    public Pillow() { }

    public Pillow(long id, bool isRuined)
    {
        this.id = id;
        if (isRuined)
            this.isRuined = "Yes";
        else
            this.isRuined = "No";
    }
}
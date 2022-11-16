using System;
using System.Xml.Serialization;

public class Lamp : Furniture
{
    public double lifeTime;

    //[XmlElement]
    //public double LifeTimeSeconds => lifeTime.Seconds;

    public Lamp() { }

    public Lamp(long id, TimeSpan lifeTime) : base(id)
    {
        this.id = id;
        this.lifeTime = lifeTime.TotalSeconds;
    }
}
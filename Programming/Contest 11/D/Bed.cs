using System;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Bed : Furniture
{
    [XmlElement("pillow")]
    public List<Pillow> pillows;

    public Bed() : base() { }

    public Bed(long id, List<Pillow> pillows) : base(id)
    {
        this.id = id;
        this.pillows = pillows;
    }
}
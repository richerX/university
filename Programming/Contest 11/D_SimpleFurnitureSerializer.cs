using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


public class SimpleFurnitureSerializer
{
    public void Serialize(List<Furniture> furniture, string outputPath)
    {
        XmlSerializer formatter = new XmlSerializer(typeof(List<Furniture>));
        using (Stream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            formatter.Serialize(stream, furniture);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

partial class Program
{
    private static void PlaceAll(List<Element> elements)
    {
        int count = elements.FindAll(x => x.Align != "absolute").Count;
        while (count > 0)
        {
            foreach (var elem in elements)
            {
                if (elem.Align != "absolute")
                {
                    int index = int.Parse(elem.Align);
                    if (elements[index].Align == "absolute")
                    {
                        elem.PosX += elements[index].PosX;
                        elem.PosY += elements[index].PosY;
                        elem.Align = "absolute";
                    }
                }
            }

            int newCount = elements.FindAll(x => x.Align != "absolute").Count;
            if (count == newCount)
                throw new FormatException("Unable to place elements");
            count = newCount;
        }

    }

    private static void DumpData(List<Element> elements, string path)
    {
        elements.Sort((x, y) => x.PosY.CompareTo(y.PosY));
        elements.Sort((x, y) => x.PosX.CompareTo(y.PosX));

        //var fs = new FileStream(path, FileMode.OpenOrCreate);
        using (var stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Element>));
            xmlSerializer.Serialize(stream, elements);
        }
    }
}
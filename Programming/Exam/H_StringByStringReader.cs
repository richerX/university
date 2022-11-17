using System;
using System.Collections.Generic;
using System.IO;

class StringByStringReader 
{
    private string filename;
    public StringByStringReader(string filename)
    {
        this.filename = filename;
    }

    public IEnumerator<string> GetEnumerator()
    {
        using (StreamReader stream = new StreamReader(filename))
        {
            while (stream.Peek() >= 0)
            {
                yield return stream.ReadLine();
            }
        }
    }

    public IEnumerable<string> CutStrings(int length)
    {
        using (StreamReader stream = new StreamReader(filename))
        {
            while (stream.Peek() >= 0)
            {
                var elem = stream.ReadLine();
                yield return elem.Substring(0, Math.Min(length, elem.Length));
            }
        }
    }
}
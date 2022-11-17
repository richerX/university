using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Program
{
    public static int SecondInArray(int[] arr)
    {
        if (arr.Length < 2)
            throw new ArgumentException("Not enough elements");
        Array.Sort(arr);
        return arr[arr.Length - 2];
    }
}


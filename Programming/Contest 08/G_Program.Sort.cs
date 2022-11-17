using System;
using System.Collections;

partial class Program
{
    internal static int[] StrangeSort(int[] arr)
    {
        int[] myArr = new int[arr.Length];
        arr.CopyTo(myArr, 0);
        Array.Sort(myArr, (int x, int y) => x.ToString().Length - y.ToString().Length);
        return myArr;
    }
}
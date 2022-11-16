using System;
using System.Text;

public class MyList<T>
{
    internal T[] array;
    internal int count = 0;

    public MyList()
    {
        array = new T[0];
    }

    public MyList(int capacity)
    {
        array = new T[capacity];
    }

    public int Count => count;

    public int Capacity => array.Length;


    public void Add(T element)
    {
        if (count == array.Length)
        {
            if (array.Length == 0)
                Array.Resize(ref array, 4);
            else
                Array.Resize(ref array, array.Length * 2);
        }
        array[count] = element;
        count += 1;
    }

    public T this[int x]
    {
        get
        {
            if (count <= x)
                throw new IndexOutOfRangeException();
            return array[x];
        }
    }

    public void Clear()
    {
        count = 0;
    }

    public void RemoveLast()
    {
        if (count == 0)
            throw new IndexOutOfRangeException();
        count -= 1;
    }

    public void RemoveAt(int index)
    {
        if (count <= index)
            throw new IndexOutOfRangeException();
        for (int i = index; i < count - 1; i++)
            array[i] = array[i + 1];
        count -= 1;
    }

    public T Max()
    {
        if (count == 0)
            throw new IndexOutOfRangeException();

        if (!(array[0] is IComparable<T>))
            throw new NotSupportedException("This operation is not supported for this type");

        T maximum = array[0];
        for (int i = 0; i < count; i++)
        {
            if (((IComparable)array[i]).CompareTo((IComparable)maximum) == 1)
                maximum = array[i];
        }
        return maximum;
    }

    public override string ToString()
    {
        string answer = "";
        for (int i = 0; i < count; i++)
            answer += array[i].ToString() + " ";
        return answer;
    }
}
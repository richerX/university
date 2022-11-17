using System;
using System.Collections;
using System.Collections.Generic;

public class ConverterArray<TV, TU> : IEnumerable, IConverterArray<TV, TU>
{
    private readonly TV[] originArr;
    private readonly IConverter<TV, TU> converter;

    public ConverterArray(int length, IConverter<TV, TU> converter)
    {
        originArr = new TV[length];
        this.converter = converter;
    }

    public TU GetAt(int index)
    {
        return converter.Convert(originArr[index]);
    }

    public void SetAt(int index, TV element)
    {
        originArr[index] = element;
    }

    public IEnumerator GetEnumerator()
    {
        List<TU> answer = new List<TU>();
        foreach (var elem in originArr)
            answer.Add(converter.Convert(elem));
        return answer.GetEnumerator();
    }
}
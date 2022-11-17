using System;

class Singleton<T> where T : new()
{
    public static T data;

    public static T Instance
    {
        get
        {
            if (data == null)
                data = new T();
            return data;
        }

        set
        {
            throw new NotSupportedException("This operation is not supported");
        }
    }
}


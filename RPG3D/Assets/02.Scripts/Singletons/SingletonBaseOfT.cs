using System.Reflection;
using System;

public abstract class SingletonBase<T>
    where T : SingletonBase<T>
{
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = (T)typeof(T).GetConstructor(new Type[] { })
                //                        .Invoke(new object[] { });

                _instance = (T)Activator.CreateInstance(typeof(T));
                _instance.Init();
            }
            return _instance;
        }
    }
    private static T _instance;

    protected virtual void Init() { }
}

public abstract class SingletonBase<TData, TWrapper>
    where TWrapper : SingletonBase<TData, TWrapper>
{
    public static TWrapper instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (TWrapper)Activator.CreateInstance(typeof(TWrapper));
                _instance.Init();
            }
            return _instance;
        }
    }
    private static TWrapper _instance;

    protected virtual void Init() { }
}
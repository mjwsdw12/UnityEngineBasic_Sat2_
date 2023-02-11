using System;

public interface INotifyList<T>
{
    event Action<T> itemAdded;
    event Action<T> itemRemoved;
    event Action<int, T> itemChanged; // index, item
    event Action listChanged;
}
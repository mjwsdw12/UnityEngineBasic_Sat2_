using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ObservableList<T> : IList<T>, INotifyList<T>
    where T : IComparable<T>
{
    private const int DEFAULT_SIZE = 1;
    protected T[] _data;
    public T this[int index] 
    { 
        get => _data[index];
        set
        {
            _data[index] = value;
            itemChanged?.Invoke(index, value);
            listChanged?.Invoke();
        }
    }

    public int Count { get; private set; }
    public int Capacity => _data.Length;

    public bool IsReadOnly => false;

    public event Action<T> itemAdded;
    public event Action<T> itemRemoved;
    public event Action<int, T> itemChanged;
    public event Action listChanged;

    public ObservableList()
    {
        _data = new T[DEFAULT_SIZE];
    }

    public void Add(T item)
    {
        if (Count >= Capacity)
        {
            T[] tmp = new T[Capacity * 2];
            for (int i = 0; i < _data.Length; i++)
            {
                tmp[i] = _data[i];
            }
            _data = tmp;
        }

        _data[Count] = item;
        Count++;
        itemAdded?.Invoke(item);
        listChanged?.Invoke();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (_data[i].CompareTo(item) == 0)
                return true;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator<T>(_data, Count);
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (_data[i].CompareTo(item) == 0)
            {
                return i;
            }
        }
        return -1;
    }

    public int IndexOf(Predicate<T> match)
    {
        for (int i = 0; i < Count; i++)
        {
            if (match.Invoke(_data[i]))
            {
                return i;
            }
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index >= 0 &&
            index < Count - 1)
        {
            for (int i = Count - 1; i >= index; i--)
            {
                _data[i + 1] = _data[i];
                itemChanged?.Invoke(i + 1, _data[i + 1]);
            }
            Count++;
            _data[index] = item;
            itemChanged?.Invoke(index, _data[index]);
            listChanged?.Invoke();
        }
        else
        {
            throw new Exception();
        }
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        T expected = _data[index];
        if (index >= 0)
        {
            for (int i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i + 1];
                itemChanged?.Invoke(i, _data[i]);
            }
            Count--;
            itemRemoved?.Invoke(expected);
            listChanged?.Invoke();
            return true;
        }
        else
        {
            return false;
        }        
    }

    public void RemoveAt(int index)
    {
        T expected = _data[index];
        for (int i = index; i < Count - 1; i++)
        {
            _data[i] = _data[i + 1];
            itemChanged?.Invoke(i, _data[i]);
        }
        Count--;
        itemRemoved?.Invoke(expected);
        listChanged?.Invoke();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public struct Enumerator<K> : IEnumerator<K>
    {
        private readonly K[] _originalData;
        private int _endIndex;
        private int _currentIndex;
        public K Current => _originalData[_currentIndex];

        object IEnumerator.Current => Current;

        public Enumerator(K[] original, int validCount)
        {
            _originalData = new K[original.Length];
            original.CopyTo(_originalData, 0);
            _endIndex = validCount - 1;
            _currentIndex = -1;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_currentIndex < _endIndex)
            {
                _currentIndex++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
}
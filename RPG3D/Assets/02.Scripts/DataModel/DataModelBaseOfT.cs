using System.Collections.Generic;
using System.Collections;
using System;

public abstract class DataModelBase<TData, TWrapper> : SingletonBase<TData, TWrapper>, IList<TData>, INotifyList<TData>
    where TData : IComparable<TData>
    where TWrapper : SingletonBase<TData, TWrapper>
{
    [Serializable]
    protected class Data
    {
        public ItemPair[] items;

        public Data(ItemPair[] items) => this.items = items;
    }

    private const int DEFAULT_SIZE = 1;
    protected TData[] data
    {
        get => _data;
        set
        {
            _data = value;
            Count = value.Length;
        }
    }
    private TData[] _data = new TData[DEFAULT_SIZE];
    public TData this[int index]
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

    public event Action<TData> itemAdded;
    public event Action<TData> itemRemoved;
    public event Action<int, TData> itemChanged;
    public event Action listChanged;


    public virtual void Add(TData item)
    {
        if (Count >= Capacity)
        {
            TData[] tmp = new TData[Capacity * 2];
            for (int i = 0; i < _data.Length; i++)
            {
                tmp[i] = _data[i];
            }
            _data = tmp;
        }

        _data[Count] = item;
        Count++;
        Save();
        itemAdded?.Invoke(item);
        listChanged?.Invoke();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(TData item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (_data[i].CompareTo(item) == 0)
                return true;
        }

        return false;
    }

    public void CopyTo(TData[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator<TData> GetEnumerator()
    {
        return new Enumerator<TData>(_data, Count);
    }

    public int IndexOf(TData item)
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

    public int IndexOf(Predicate<TData> match)
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

    public void Insert(int index, TData item)
    {
        if (index >= 0 &&
            index < Count - 1)
        {
            for (int i = Count - 1; i >= index; i--)
            {
                _data[i + 1] = _data[i];
            }
            Count++;
            _data[index] = item;
            Save();

            for (int i = Count - 1; i >= index; i--)
            {
                itemChanged?.Invoke(i, _data[i]);
            }
            listChanged?.Invoke();
        }
        else
        {
            throw new Exception();
        }
    }

    public bool Remove(TData item)
    {
        int index = IndexOf(item);

        if (index >= 0)
        {
            TData expected = _data[index];
            for (int i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i + 1];
            }
            Count--;
            Save();

            for (int i = index; i < Count - 1; i++)
            {
                itemChanged?.Invoke(i, _data[i]);
            }

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
        TData expected = _data[index];
        for (int i = index; i < Count - 1; i++)
        {
            _data[i] = _data[i + 1];
        }
        Count--;
        Save();

        for (int i = index; i < Count - 1; i++)
        {
            itemChanged?.Invoke(i, _data[i]);
        }
        itemRemoved?.Invoke(expected);
        listChanged?.Invoke();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract void Save();
    public abstract void Load();

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

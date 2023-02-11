using System;

[Serializable]
public struct ItemPair : IComparable<ItemPair>
{
    public static ItemPair zero = new ItemPair(0, 0);
    public static ItemPair empty = new ItemPair(-1, -1);

    public int id;
    public int num;

    public ItemPair(int id, int num)
    {
        this.id = id;
        this.num = num;
    }

    public static ItemPair operator +(ItemPair op1, ItemPair op2)
    {
        if (op1.id != op2.id)
            throw new InvalidOperationException($"[ItemPair] : Failed to sum item. {op1.id} != {op2.id}");

        return new ItemPair(op1.id, op1.num + op2.num);
    }
    public static ItemPair operator -(ItemPair op1, ItemPair op2)
    {
        if (op1.id != op2.id)
            throw new InvalidOperationException($"[ItemPair] : Failed to sum item. {op1.id} != {op2.id}");

        return new ItemPair(op1.id, op1.num - op2.num);
    }
    public static bool operator ==(ItemPair op1, ItemPair op2)
    {
        return (op1.id == op2.id) && (op1.num == op2.num);
    }

    public static bool operator !=(ItemPair op1, ItemPair op2)
    {
        return !(op1 == op2);
    }

    public static bool operator >(ItemPair op1, ItemPair op2)
    {
        return op1.num > op2.num;
    }

    public static bool operator <(ItemPair op1, ItemPair op2)
    {
        return op1.num < op2.num;
    }

    public static bool operator >=(ItemPair op1, ItemPair op2)
    {
        return op1.num >= op2.num;
    }

    public static bool operator <=(ItemPair op1, ItemPair op2)
    {
        return op1.num <= op2.num;
    }

    public int CompareTo(ItemPair other)
    {
        if (num < other.num)
            return -1;
        else if (num > other.num)
            return 1;
        else
            return 0;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour, IComparable<TileInfo>
{
    public int Id;


    protected virtual void Awake()
    {
        Id = transform.GetSiblingIndex() + 1;
    }
    public virtual void OnTile()
    {
        Debug.Log ($"{Id} 칸에 도착했습니다.");
    }

    public int CompareTo(TileInfo other)
    {
        return this.Id - other.Id;
    }


    // 연산자 오버로딩
    public static bool operator > (TileInfo op1, TileInfo op2)
    {
        return op1.Id > op2.Id;
    }

    public static bool operator < (TileInfo op1, TileInfo op2)
    {
        return op1.Id < op2.Id;
    }
}

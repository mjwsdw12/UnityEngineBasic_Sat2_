using UnityEngine;
using System.Collections.Generic;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ItemAssets>("ItemAssets"));
            return _instance;
        }
    }
    private static ItemAssets _instance;
    public ItemInfo this[int id] => itemInfoPairs[id];

    public List<ItemInfo> itemInfos = new List<ItemInfo> ();
    public Dictionary<int, ItemInfo> itemInfoPairs = new Dictionary<int, ItemInfo>();

    private void Awake()
    {
        foreach (var item in itemInfos)
        {
            itemInfoPairs.Add(item.id, item);
        }
    }
}
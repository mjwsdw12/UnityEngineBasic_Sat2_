using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemInfo itemInfo;
    public int num;
    private bool _isPicking;

    public void PickUp()
    {
        if (_isPicking)
            return;

        _isPicking = true;
        InventoryDataModel.instance.Add(new ItemPair(itemInfo.id, num));
        Destroy(gameObject);
    }
}

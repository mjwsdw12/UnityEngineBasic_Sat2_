using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryViewSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _num;

    public void Set(Sprite icon, int num)
    {
        _icon.sprite = icon;
        _num.text = num.ToString();
    }
}
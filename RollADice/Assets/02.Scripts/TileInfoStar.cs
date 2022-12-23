using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TileInfoStar : TileInfo
{
    private int _starValue;
    public int StarValue
    {
        get
        {
            return _starValue;
        }
        set
        {
            _starValue = value;

            if (_starValueText != null)
                _starValueText.text = _starValue.ToString();
        }
    }
    private TMP_Text _starValueText;

    public override void OnTile()
    {
        base.OnTile();
        StarValue++;
    }

    protected override void Awake()
    {
        base.Awake();
        
        _starValueText = transform.GetComponentInChildren<TMP_Text>();
        StarValue = Constants.STAR_VALUE_INIT;
    }
}

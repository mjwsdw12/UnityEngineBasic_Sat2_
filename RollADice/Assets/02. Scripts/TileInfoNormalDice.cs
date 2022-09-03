using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfoNormalDice : TileInfo
{
    public override void OnTile()
    {
        base.OnTile();
        GamePlay.Instance.NomalDiceNum++;
    }
}

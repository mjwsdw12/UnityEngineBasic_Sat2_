using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTypes
{
    MachineGun,
    RocketLauncher,
    LaserBeamer
}

[CreateAssetMenu(fileName = "new TowerInfo",  menuName = "TowerDefence/TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public TowerTypes Type;
    public int UpgradeLevel;
    public int BuildPrice;
    public int SellPrice;
    public Sprite Icon;
}

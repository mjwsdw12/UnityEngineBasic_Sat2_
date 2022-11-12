using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "TowerDefence/LevelData")]
public class LevelData : ScriptableObject
{
    public int Level;
    public int LifeInit;
    public int MoneyInit;
    public List<StageData> StagesDataList;
}

[System.Serializable]
public class StageData
{
    public int Stage;
    public List<SpawnData> SpawnDataList;
}

[System.Serializable]
public class SpawnData
{
    public GameObject Prefab;
    public int Num;
    public int SpawnPointIndex;
    public int GoalPointIndex;
    public float Delay;
    public float Term;
}

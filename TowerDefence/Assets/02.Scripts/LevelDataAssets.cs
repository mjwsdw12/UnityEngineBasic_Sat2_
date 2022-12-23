using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataAssets : MonoBehaviour
{
    private static LevelDataAssets _instance;
    public static LevelDataAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<LevelDataAssets>("LevelDataAssets"));
            return _instance;
        }
    }

    [SerializeField] private List<LevelData> _levelDataList;
    public bool TryGetLevelData(int level, out LevelData data)
    {
        data = _levelDataList.Find(data => data.Level == level);
        return data != null;
    }
}

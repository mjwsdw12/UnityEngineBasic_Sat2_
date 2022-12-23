using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public void OnClick(int level)
    {
        GameManager.Instance.SelectLevel(level);
    }
}

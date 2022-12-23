using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.StartLevel();
    }
}

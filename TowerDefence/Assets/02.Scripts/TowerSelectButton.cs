using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectButton : MonoBehaviour
{
    [SerializeField] private TowerInfo _info;

    public void OnClick()
    {
        TowerHandler.Instance.Handle(_info);
    }
}

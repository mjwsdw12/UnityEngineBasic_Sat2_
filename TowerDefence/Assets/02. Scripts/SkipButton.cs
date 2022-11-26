using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class SkipButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void AddListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void OnClick()
    {

    }
}

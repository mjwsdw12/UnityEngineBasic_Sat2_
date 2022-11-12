using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartLevelAskingText : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        _text.text = $"Start level {GameManager.instance.Level} ??";
    }
}

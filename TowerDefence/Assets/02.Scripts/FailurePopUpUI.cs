using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailurePopUpUI : MonoBehaviour
{
    [SerializeField] private Button _lobby;
    [SerializeField] private Button _replay;

    private void OnEnable()
    {
        _lobby.onClick.RemoveAllListeners();
        _lobby.onClick.AddListener(() =>
        {
            GameManager.Instance.GoToLobby();
        });

        _replay.onClick.RemoveAllListeners();
        _replay.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartLevel();
        });

    }
}

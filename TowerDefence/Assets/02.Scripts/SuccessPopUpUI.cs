using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessPopUpUI : MonoBehaviour
{
    [SerializeField] private List<SuccessStar> _stars;
    [SerializeField] private float _starShowingTerm = 0.5f;

    [SerializeField] private Button _lobby;
    [SerializeField] private Button _replay;
    [SerializeField] private Button _next;

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

        _next.onClick.RemoveAllListeners();
        _next.onClick.AddListener(() =>
        {
            GameManager.Instance.StartNextLevel();
        });

        StartCoroutine(E_ShowStars());
    }

    private IEnumerator E_ShowStars()
    {
        int count = 0;
        float lifeRatio = (float)Player.Instance.Life / GameManager.Instance.Data.LifeInit;
        while (count < _stars.Count * lifeRatio)
        {
            _stars[count++].Show();
            yield return new WaitForSeconds(_starShowingTerm);
        }
    }
}

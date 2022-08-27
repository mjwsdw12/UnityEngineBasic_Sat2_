using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _PasuedUI;
    public bool IsGamePause
    {
        get => Time.timeScale <= 0.0f ? true : false;
    }
    public bool IsGameOver;
    public void GameOver()
    {
        pauseGame(true);
        PopUpGameOverUI();
        IsGameOver = true;
    }

    private void CountinueGame()
    {
        pauseGame(false);
        HideGameOverUI();
    }

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (IsGameOver == false &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePause)
            {
                pauseGame(false);
                HidePausedUI();
            }
            else
            {
                pauseGame(true);
                PopUpPausedUI();
            }
        }
    }
    private void pauseGame(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    private void PopUpGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }

    private void HideGameOverUI()
    {
        _gameOverUI.SetActive(false);
    }
    private void PopUpPausedUI()
    {
        _PasuedUI.SetActive(true);
    }
    private void HidePausedUI()
    {
        _PasuedUI.SetActive(false );
    }
}

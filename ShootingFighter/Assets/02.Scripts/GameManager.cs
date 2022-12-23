using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _pausedUI;
    public bool IsGamePaused
    {
        get => Time.timeScale <= 0.0f ? true : false;
    }

    public bool IsGameOver;
    public void GameOver()
    {
        PauseGame(true);
        PopUpGameOverUI();
        IsGameOver = true;
    }
    public void ContinueGame()
    {
        PauseGame(false);
        HideGameOverUI();
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (IsGameOver == false &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                PauseGame(false);
                HidePausedUI();
            }
            else
            {
                PauseGame(true);
                PopUpPausedUI();
            }
        }
    }

    private void PauseGame(bool pause)
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
        _pausedUI.SetActive(true);
    }

    private void HidePausedUI()
    {
        _pausedUI.SetActive(false);
    }
}

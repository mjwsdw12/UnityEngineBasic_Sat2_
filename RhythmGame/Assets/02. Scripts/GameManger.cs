using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    public enum GameStates
    {
        Idle,
        StartPlay,
        WaitForGameFinished,
        DisplayScore,
        WaitForUser
    }
    public GameStates State;

    public void StartGame()
    {
        if (State == GameStates.Idle)
            State = GameStates.StartPlay;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        switch (State)
        {
            case GameStates.Idle:
                break;
            case GameStates.StartPlay:
                {
                    if (NoteManger.Instance.IsReady)
                    {
                        NoteManger.Instance.StartSpawn();
                        State++;
                    }

                }
                break;
            case GameStates.WaitForGameFinished:
                break;
            case GameStates.DisplayScore:
                break;
            case GameStates.WaitForUser:
                break;
            default:
                break;
        }
    }
}

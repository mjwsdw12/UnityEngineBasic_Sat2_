using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameStates
    {
        Idle,
        LoadLevelData,
        WaitUntilLevelDataLoaded,
        StartGame,
        LoadLevel,
        WaitUntilLevelLoaded,
        WaitUntilLevelFinished,
        LevelCleared,
        LevelFailed,
        WaitForUser
    }
    public GameStates Current;
    public int Level;


    public void SelectLevel(int level)
    {
        Level = level;
    }

    public void StartLevel()
    {
        if (Current == GameStates.Idle)
            MoveNext();
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        Workflow();
    }

    private void Workflow()
    {
        switch (Current)
        {
            case GameStates.Idle:
                break;
            case GameStates.LoadLevelData:
                {
                    if (LevelDataAssets.Instance != null)
                        MoveNext();
                }
                break;
            case GameStates.WaitUntilLevelDataLoaded:
                {
                    if (LevelDataAssets.Instance.TryGetLevelData(Level, out LevelData data))
                        MoveNext();
                    else
                        throw new System.Exception("Failed to load level data");
                }
                break;
            case GameStates.StartGame:
                {
                    SceneManager.LoadScene($"Level{Level}");
                    MoveNext();
                }
                break;
            case GameStates.LoadLevel:
                {
                    Pathfinder.SetUpMap();
                    MoveNext();
                }
                break;
            case GameStates.WaitUntilLevelLoaded:
                {
                    MoveNext();
                }
                break;
            case GameStates.WaitUntilLevelFinished:
                break;
            case GameStates.LevelCleared:
                break;
            case GameStates.LevelFailed:
                break;
            case GameStates.WaitForUser:
                break;
            default:
                break;
        }
    }

    private void MoveNext() => Current++;
}

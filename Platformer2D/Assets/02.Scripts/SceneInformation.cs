using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInformation
{
    private static SceneInformation _instance;
    public static SceneInformation Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneInformation();                
            }
            return _instance;
        }
    }
    private bool _isSceneLoaded;
    public static bool IsSceneLoaded => Instance._isSceneLoaded;
    public static string OldSceneName;
    public static string NewSceneName;

    public SceneInformation()
    {
        OldSceneName = NewSceneName = SceneManager.GetActiveScene().name;
        _isSceneLoaded = true;
        SceneManager.sceneUnloaded += (scene) =>
        {
            _isSceneLoaded = false;
        };
        SceneManager.sceneLoaded += (scene, loadMode) =>
        {
            OldSceneName = NewSceneName;
            NewSceneName = SceneManager.GetActiveScene().name;
            Debug.Log($"Scene loaded : {OldSceneName} -> {NewSceneName}");
            _isSceneLoaded = true;
        };
    }
}

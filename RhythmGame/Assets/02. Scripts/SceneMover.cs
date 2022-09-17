using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬 이동 관련 클래스
/// </summary>
public class SceneMover
{
    public static void MoveTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void MoveTo(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
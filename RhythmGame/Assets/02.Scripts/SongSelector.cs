using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 노래 선택자
/// </summary>
public class SongSelector : MonoBehaviour
{
    public static SongSelector Instance;
    public string SelectedSongName;
    public VideoClip Clip;
    public SongData Data;
    public bool IsDataLoaded => Data != null;

    public void Select(string songName)
    {
        SelectedSongName = songName;
    }

    /// <summary>
    /// Json 노래 데이터와 비디오클립을 로드
    /// </summary>
    /// <returns></returns>
    public bool TryLoadSelectedSongData()
    {
        bool isLoaded = false;

        // 선택된 노래가 있는지 체크
        if (string.IsNullOrEmpty(SelectedSongName))
            return false;

        // 노래 데이터 & 비디오클립 로드시 예외 잡기 시도
        try
        {
            Clip = Resources.Load<VideoClip>($"VideoClips/{SelectedSongName}");
            TextAsset dataText = Resources.Load<TextAsset>($"SongData/{SelectedSongName}");
            Data = JsonUtility.FromJson<SongData>(dataText.ToString());
            isLoaded = true;
        }
        catch (System.Exception e)
        {
            isLoaded = false;
            Debug.LogError($"SongSelector : Failed to load song... {e.Message}");
        }

        return isLoaded;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

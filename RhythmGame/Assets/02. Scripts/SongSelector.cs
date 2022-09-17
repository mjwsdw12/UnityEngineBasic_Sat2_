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
    public void Select(string songName)
    {
        SelectedSongName = songName;
    }
    
    public bool TryLoadSelectedSongData()
    {
        bool isLoaded = false;

        if (string.IsNullOrEmpty(SelectedSongName))
            return false;

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
            Debug.LogError($"SongSelector : failed to load song ... {e.Message}");
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

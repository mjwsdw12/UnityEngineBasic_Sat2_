using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NotesMaker : MonoBehaviour
{
    private SongData _songData;
    private KeyCode[] _keys = {KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L};
    [SerializeField] private VideoPlayer _vp;
    [SerializeField] private int _level = 1;
    public bool DoRecord;


    //==========================================================
    //***********************public Methods*********************
    //==========================================================

    /// <summary>
    /// 노래 데이터 생성, 녹화시작
    /// </summary>
    public void StartRecording()
    {
        if (DoRecord)
            return;

        _songData = new SongData(_vp.clip.name, _level);
        _songData.Name = _vp.clip.name;
        _songData.Level = _level;
        _vp.Play();
        DoRecord = true;
    }

    /// <summary>
    /// 키 입력 체크하면서 해당 키에 대한 노트 데이터 생성
    /// Update() 에서 호출해야함
    /// </summary>
    public void Recording()
    {
        foreach (KeyCode Key in _keys)
        {
            if (Input.GetKeyDown(Key))
                CreateNoteData(Key);
        }
    }

    /// <summary>
    /// 녹화중 정지버튼 누를시 노래데이터 저장 후 비디오 멈춤
    /// </summary>
    public void StopRecording()
    {
        if (DoRecord == false)
            return;
    
        SaveSongData();
        _songData = null;
        _vp.Stop();
    
    }


    //==========================================================
    //**********************private Methods*********************
    //==========================================================

    private void Update()
    {
        if (DoRecord)
            Recording();
    }

    private void CreateNoteData(KeyCode keyCode)
    {
        NoteData noteData = new NoteData();

        float time = (float)System.Math.Round(_vp.time, 2);
        noteData.Time = time;
        noteData.Key = keyCode;

        _songData.Notes.Add(noteData);
        Debug.Log($"NotesMaker : Note created, {keyCode}, {time}");
    }

    private void SaveSongData()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanel("저장할 곳을 지정하세요", "", $"{_songData.Name}", "json");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }
}

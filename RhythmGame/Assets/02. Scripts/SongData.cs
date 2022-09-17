using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 노래 데이터
/// 노래 이름, 난이도, 노트 목록 정보
/// </summary>
[System.Serializable]
public class SongData
{
    public string Name;
    public int Level;
    public List<NoteData> Notes;

    public SongData(string name, int level)
    {
        Name = name;
        Level = level;
        Notes = new List<NoteData>();   
    }

    public SongData()
    {
        Notes = new List<NoteData>();
    }
}

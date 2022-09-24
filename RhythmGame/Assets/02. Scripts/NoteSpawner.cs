using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 노트 소환 클래스
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    public KeyCode Key;
    [SerializeField] private GameObject _notePrefab;

    /// <summary>
    /// 노트 소환후 스케일을 맞춤
    /// </summary>
    public void spawn()
    {
        GameObject note = Instantiate(_notePrefab, transform.position, Quaternion.identity);
        note.transform.localScale = transform.lossyScale;
    }
}

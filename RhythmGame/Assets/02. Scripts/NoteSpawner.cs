using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public KeyCode Key;
    [SerializeField] private GameObject _notePrefab;


    public void spawn()
    {
        GameObject note = Instantiate(_notePrefab, transform.position, Quaternion.identity);
        note.transform.localScale = transform.lossyScale;
    }
}

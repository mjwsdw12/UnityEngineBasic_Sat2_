using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;
    public static float NoteSpeedScale = 2.0f;

    public bool IsReady = false;
    private Queue<NoteData> _noteDataQueue = new Queue<NoteData>();
    private Dictionary<KeyCode, NoteSpawner> _spawners = new Dictionary<KeyCode, NoteSpawner>();

    [SerializeField] private Transform _spawnersPoint;
    [SerializeField] private Transform _noteHitterPoint;
    [SerializeField] private VideoPlayer _videoPlayer;

    public float NoteFallingDistance => _spawnersPoint.transform.position.y - _noteHitterPoint.transform.position.y;
    public float NoteFallingTime => NoteFallingDistance / NoteSpeedScale;

    public void StartSpawn()
    {
        if (_noteDataQueue.Count > 0)
        {
            StartCoroutine(E_Spawning());
            Invoke("PlayVideo", NoteFallingTime);
        }
    }

    IEnumerator E_Spawning()
    {
        float timeMark = Time.time;
        NoteData noteData;

        // note data가 남아있으면 계속 진행
        while (_noteDataQueue.Count > 0)
        {
            // 소환시간이 된 노트 전부 큐에서 빼서 소환함
            while (_noteDataQueue.Count > 0)
            {
                if (_noteDataQueue.Peek().Time < (Time.time - timeMark))
                {
                    noteData = _noteDataQueue.Dequeue();
                    _spawners[noteData.Key].Spawn();
                }
                else
                {
                    break;
                }
            }
            yield return null;
        }
    }


    private void Awake()
    {
        Instance = this;

        StartCoroutine(E_Init());
    }

    IEnumerator E_Init()
    {
        NoteSpawner[] noteSpawners = _spawnersPoint.GetComponentsInChildren<NoteSpawner>();
        for (int i = 0; i < noteSpawners.Length; i++)
            _spawners.Add(noteSpawners[i].Key, noteSpawners[i]);

        // SongSelector 에 노래데이터 로드 될때까지 기다림
        yield return new WaitUntil(() => SongSelector.Instance &&
                                         SongSelector.Instance.IsDataLoaded);

        // 노트 데이터 시간순 정렬 후 큐에 등록
        IOrderedEnumerable<NoteData> noteDataFiltered = SongSelector.Instance.Data.Notes.OrderBy(note => note.Time);
        foreach (NoteData noteData in noteDataFiltered)
            _noteDataQueue.Enqueue(noteData);

        IsReady = true;
    }

    private void PlayVideo()
    {
        _videoPlayer.clip = SongSelector.Instance.Clip;
        _videoPlayer.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelData Data;
    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 0.25f, 0.0f);
    private List<StageData> _stageDataList = new List<StageData>();
    private List<int> _stageSpawnedList = new List<int>();
    private List<float[]> _delayTimersList = new List<float[]>();
    private List<float[]> _termTimerList = new List<float[]>();
    private List<int[]> _counterList = new List<int[]>();

    [SerializeField] private GameObject _skipButtonPrefab;
    private List<GameObject> _skipButtons = new List<GameObject>();
    private int _currentStage;
    public bool IsFinished => (_currentStage >= Data.StageDataList.Count) && (_stageDataList.Count <= 0);
    private int _totalSpawnedEnemy;
    public int TotalSpawnedEnemy
    {
        get
        {
            return _totalSpawnedEnemy;
        }
        set
        {
            _totalSpawnedEnemy = value;

            if (IsFinished && value <= 0)
            {
                GameManager.Instance.ClearLevel();
            }
        }
    }

    public void SpawnNext()
    {
        _currentStage++;
        StartSpawn(_currentStage);
    }

    public void StartSpawn(int stage)
    {
        // 소환하려는 스테이지가 유효한지 / 이미 소환중인지 체크
        if ((stage < 1 || stage > Data.StageDataList.Count) ||
            _stageSpawnedList.Contains(stage))
            return;

        _stageSpawnedList.Add(stage);
        StageData stageData = Data.StageDataList[stage - 1]; // 레벨 데이터에서 해당 스테이지 데이터 참조
        int length = Data.StageDataList[stage - 1].SpawnDataList.Count;

        _stageDataList.Add(stageData); // 현재 소환중인 스테이지 데이터 리스트에 등록
        float[] tmpDelayTimers = new float[length];
        float[] tmpTermTimers = new float[length];
        int[] tmpCounters = new int[length];

        for (int i = 0; i < length; i++)
        {
            tmpDelayTimers[i] = stageData.SpawnDataList[i].Delay;
            tmpTermTimers[i] = stageData.SpawnDataList[i].Term;
            tmpCounters[i] = stageData.SpawnDataList[i].Num;
        }

        _delayTimersList.Add(tmpDelayTimers);
        _termTimerList.Add(tmpTermTimers);
        _counterList.Add(tmpCounters);
    }

    
    private void Start()
    {
        CreateSkipButtons();
        Pathfinder.SetUpMap();
        RegisterPoolElements();
    }

    private void Update()
    {
        // 역순으로  for문을 진행하는 이유는 특정 스테이지 소환 종료시 리스트에서 해당 스테이지 데이터를 제거해야하기 때문

        // 소환중인 스테이지 데이터들을 순회
        for (int i = _stageDataList.Count -1; i >= 0; i--)
        {
            bool finished = true;
            // 스테이지에서 소환해야하는 각 적들에 대한 데이터들을 순회
            for (int j = 0; j < _stageDataList[i].SpawnDataList.Count; j++)
            {
                // 소환할 것이 있다면 
                if (_counterList[i][j] > 0)
                {
                    finished = false;

                    // 소환 시작 딜레이 타이머 종료되었으면 
                    if (_delayTimersList[i][j] <= 0)
                    {
                        // 소환 주기 타이머 종료되었으면
                        if (_termTimerList[i][j] <= 0)
                        {
                            GameObject go = 
                                ObjectPool.Instance.Spawn(_stageDataList[i].SpawnDataList[j].Prefab.name,
                                                          Paths.Instance.StartPointList[_stageDataList[i].SpawnDataList[j].SpawnPointIndex].position + _stageDataList[i].SpawnDataList[j].Prefab.transform.position + _offset);

                            TotalSpawnedEnemy++;
                            go.GetComponent<Enemy>().OnDie += () =>
                            {
                                ObjectPool.Instance.Return(go);
                                TotalSpawnedEnemy--;
                            };

                            go.GetComponent<EnemyMove>().SetUp(Paths.Instance.StartPointList[_stageDataList[i].SpawnDataList[j].SpawnPointIndex],
                                                               Paths.Instance.EndPointList[_stageDataList[i].SpawnDataList[j].GoalPointIndex]);

                            _termTimerList[i][j] = _stageDataList[i].SpawnDataList[j].Term;
                            _counterList[i][j]--;
                        }
                        else
                        {
                            _termTimerList[i][j] -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        _delayTimersList[i][j] -= Time.deltaTime;
                    }

                }
            }

            if (finished)
            {
                _stageDataList.RemoveAt(i);
                _delayTimersList.RemoveAt(i);
                _termTimerList.RemoveAt(i);
                _counterList.RemoveAt(i);

                if (_currentStage < Data.StageDataList.Count)
                {
                    ActiveSkipButtons();
                }
            }
        }
    }


    private void RegisterPoolElements()
    {
        for (int i = 0; i < Data.StageDataList.Count; i++)
        {
            for (int j = 0; j < Data.StageDataList[i].SpawnDataList.Count; j++)
            {
                ObjectPool.Instance.AddElement(new ObjectPoolElement()
                {
                    Name = Data.StageDataList[i].SpawnDataList[j].Prefab.name,
                    Prefab = Data.StageDataList[i].SpawnDataList[j].Prefab,
                    Num = Data.StageDataList[i].SpawnDataList[j].Num
                });
            }
        }
        ObjectPool.Instance.InstantiateAllElements();
    }

    private void CreateSkipButtons()
    {
        for (int i = 0; i < Paths.Instance.StartPointList.Count; i++)
        {
            GameObject go = Instantiate(_skipButtonPrefab);
            go.transform.position = Paths.Instance.StartPointList[i].transform.position + Vector3.up;
            _skipButtons.Add(go);
            go.GetComponentInChildren<SkipButton>().AddListener(() =>
            {
                SpawnNext();
                DeactiveSkipButtons();
            });
        }
    }

    private void ActiveSkipButtons()
    {
        foreach (var button in _skipButtons)
        {
            button.SetActive(true);
        }
    }

    private void DeactiveSkipButtons()
    {
        foreach (var button in _skipButtons)
        {
            button.SetActive(false);
        }
    }
}

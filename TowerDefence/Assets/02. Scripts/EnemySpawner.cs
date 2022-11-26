using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
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
    
    public void SpawnNext()
    {
        _currentStage++;
        StartSpawn(_currentStage);
    }

    public void StartSpawn(int stage)
    {
        // ��ȯ�Ϸ��� ���������� ��ȿ���� / �̹� ��ȯ������ üũ
        if ((stage < 1 || stage > Data.StagesDataList.Count) ||
            _stageSpawnedList.Contains(stage))
            return;

        _stageSpawnedList.Add(stage);
        StageData stageDate = Data.StagesDataList[stage - 1]; // ���� �����Ϳ��� �ش� �������� ������ ����
        int length = Data.StagesDataList[stage - 1].SpawnDataList.Count;

        _stageDataList.Add(stageDate); // ���� ��ȯ���� �������� ������ ����Ʈ ���
        float[] tmpDelayTimers = new float[length];
        float[] tmpTermTimers = new float[length];
        int[] tmpCounters = new int[length];

        for (int i = 0; i < length; i++)
        {
            tmpDelayTimers[i] = stageDate.SpawnDataList[i].Delay;
            tmpTermTimers[i] = stageDate.SpawnDataList[i].Term;
            tmpCounters[i] = stageDate.SpawnDataList[i].Num;
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
        // �������� for���� �����ϴ� ������ Ư�� �������� ��ȯ ����� ����Ʈ���� �ش� �������� �����͸� �����ؾ߱� ����

        // ��ȯ���� �������� �����͸� ��ȸ
        for (int i = _stageDataList.Count -1; i >= 0; i--)
        {
            bool finished = true;
            // ������������ ��ȯ�ؾ��ϴ� �� ���鿡 ���� �����͵��� ��ȸ
            for(int j = 0; j < _stageDataList[i].SpawnDataList.Count; j++)
            {
                // ��ȯ�� ���� �ִٸ�
                if (_counterList[i][j] > 0)
                {
                    finished = false;

                    // ��ȯ ���� ������ Ÿ�̸� ����Ǿ�����
                    if(_delayTimersList[i][j] <= 0)
                    {
                        // ��ȯ �ֱ� Ÿ�̸� ����Ǿ�����
                        if(_termTimerList[i][j] <= 0)
                        {
                            GameObject go = 
                                ObjectPool.Instance.Spawn(_stageDataList[i].SpawnDataList[j].Prefab.name,
                                                          Paths.Instance.StartPointList[_stageDataList[i].SpawnDataList[j].SpawnPointIndex].position + _stageDataList[i].SpawnDataList[j].Prefab.transform.position + _offset);

                            go.GetComponent<Enemy>().OnDie += () => ObjectPool.Instance.Return(go);

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

                if(_currentStage < Data.StagesDataList.Count)
                {
                    ActiveSkipButtons();
                }
            }

        }
    }

    private void RegisterPoolElements()
    {
        for (int i = 0; i < Data.StagesDataList.Count; i++)
        {
            for (int j = 0; j < Data.StagesDataList[i].SpawnDataList.Count; j++)
            {
                ObjectPool.Instance.AddElement(new ObjectPoolElement()
                {
                    Name = Data.StagesDataList[i].SpawnDataList[j].Prefab.name,
                    Prefab = Data.StagesDataList[i].SpawnDataList[j].Prefab,
                    Num = Data.StagesDataList[i].SpawnDataList[j].Num
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

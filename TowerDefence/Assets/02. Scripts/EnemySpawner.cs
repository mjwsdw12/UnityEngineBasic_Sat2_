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
    public void StartSpawn(int stage)
    {
        if ((stage < 1 || stage > Data.StagesDataList.Count) ||
            _stageSpawnedList.Contains(stage))
            return;

        _stageSpawnedList.Add(stage);
        StageData stageDate = Data.StagesDataList[stage - 1];
        int length = Data.StagesDataList[stage - 1].SpawnDataList.Count;

        _stageDataList.Add(stageDate);
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

    private void Update()
    {
        for (int i = _stageDataList.Count -1; i >= 0; i--)
        {
            bool finished = true;
            for(int j = 0; j < _stageDataList[i].SpawnDataList.Count; j++)
            {
                // 소환할 것이 있다면
                if (_counterList[i][j] > 0)
                {
                    finished = false;

                    // 소환 시작 딜레이 타이머 종료되었으면
                    if(_delayTimersList[i][j] <= 0)
                    {
                        // 소환 주기 타이머 종료되었으면
                        if(_termTimerList[i][j] <= 0)
                        {
                            GameObject go = Instantiate(_stageDataList[i].SpawnDataList[j].Prefab,
                                                        Paths.Instance.StartPointList[_stageDataList[i].SpawnDataList[j].SpawnPointIndex].position + _stageDataList[i].SpawnDataList[j].Prefab.transform.position + _offset,
                                                        Quaternion.identity);

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
            }

        }
    }
}

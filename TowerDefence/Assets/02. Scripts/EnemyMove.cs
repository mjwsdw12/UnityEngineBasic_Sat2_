using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 의존성을 명시하기위한 Attribute. 
// 스크립트 인스턴스를 추가했을 때 해당 Component 가 없으면 에디터가 알아서 추가해줌. 
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Pathfinder))]
public class EnemyMove : MonoBehaviour
{
    private Enemy _enemy;
    private Pathfinder _pathfinder;
    private List<Transform> _path;
    private int _currentPathPointIndex;
    private Transform _nextPoint;
    private float _posTolerance = 0.05f;


    public void SetUp(Transform start, Transform end)
    {
        _pathfinder.TryFindOptimizedPath(start, end, out _path);
        _nextPoint = _path[1];
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _pathfinder = GetComponent<Pathfinder>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_nextPoint.position.x,
                                        transform.position.y,
                                        _nextPoint.position.z);
        Vector3 dir = (targetPos - transform.position).normalized;

        if (Vector3.Distance(targetPos, transform.position) < _posTolerance)
        {
            // 다음 목표 길 지점이 있으면
            if (TryGetNextPoint(_currentPathPointIndex, out _nextPoint))
            {
                _currentPathPointIndex++;
            }
            // 도착 지점에 도착했으면
            else
            {
                OnReachedToEnd();
            }
        }

        transform.LookAt(targetPos);
        transform.Translate(dir * _enemy.Speed * Time.fixedDeltaTime, Space.World);
    }

    private bool TryGetNextPoint(int pointIndex, out Transform nextPoint)
    {
        nextPoint = null;

        if (pointIndex < _path.Count - 1)
        {
            nextPoint = _path[pointIndex + 1];
        }

        return nextPoint != null;
    }

    private void OnReachedToEnd()
    {
        // todo -> decrease play life 1
        _enemy.Die();
    }
}
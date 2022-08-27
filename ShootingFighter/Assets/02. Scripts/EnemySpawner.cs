using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private BoxCollider _spawnBound;
    private float _timer;
    private Vector3 _spawnPos;

    private void Awake()
    {
        _timer = _spawnDelay;
    }
    private void Update()
    {
        if (_timer <0)
        {
            _spawnPos = new Vector3 (transform.position.x + _spawnBound.center.x + Random.Range(-_spawnBound.size.x / 2.0f, _spawnBound.size.x / 2.0f),
                                 transform.position.y + _spawnBound.center.y + Random.Range(-_spawnBound.size.y / 2.0f, _spawnBound.size.y / 2.0f),
                                 transform.position.z + _spawnBound.center.z);

            Instantiate(_enemyPrefab, _spawnPos, Quaternion.identity);
            _timer = _spawnDelay;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMachineGun : Tower
{
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private float _firePointsBlinkTime;
    [SerializeField] private int _damage;    
    [SerializeField] private float _reloadTime;    
    private float _reloadTimer;

    private bool _isBlinking;
    private float _blinkTimer;

    private void Update()
    {
        Reload();
    }

    private void Reload()
    {
        if (_reloadTimer < 0)
        {
            if (Target != null)
            {
                Attack();
                _reloadTimer = _reloadTime;
            }
        }
        else
        {
            _reloadTimer -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        BlinkFirePoints();
        for (int i = 0; i < _firePoints.Length; i++)
        {
            GameObject bullet = ObjectPool.Instance.Spawn("MachineGunBullet", _firePoints[i].position);
            bullet.GetComponent<ProjectileMachineGunBullet>().SetUp(true, 5.0f, _damage, TargetLayer, Target);
        }
    }

    private void BlinkFirePoints()
    {
        if (_isBlinking)
            _blinkTimer = _firePointsBlinkTime;
        else
            StartCoroutine(E_BlinkFirePoints());
    }

    private IEnumerator E_BlinkFirePoints()
    {
        _isBlinking = true;
        _blinkTimer = _firePointsBlinkTime;
        ActiveFirePoints();
        while (_blinkTimer > 0)
        {
            _blinkTimer -= Time.deltaTime;
            yield return null;
        }
        DeactiveFirePoints();
        _isBlinking = false;
    }

    private void ActiveFirePoints()
    {
        foreach (Transform firePoint in _firePoints)
            firePoint.gameObject.SetActive(true);
    }

    private void DeactiveFirePoints()
    {
        foreach (Transform firePoint in _firePoints)
            firePoint.gameObject.SetActive(false);
    }
}

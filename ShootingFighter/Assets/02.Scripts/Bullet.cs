using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private LayerMask _targetLayer;
    
    private void FixedUpdate()
    {
        Vector3 deltaMove = Vector3.forward * _moveSpeed * Time.fixedDeltaTime;
        transform.Translate(deltaMove, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1<<other.gameObject.layer == _targetLayer)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Hp -= _damage;
            }
        }
        
        Destroy(gameObject);
    }

    //private bool TryGetComponent(out Enemy enemy)
    //{
    //    enemy = gameObject.GetComponent<Enemy>();
    //    return enemy != null ? true : false;
    //    // 삼항 연산자
    //    // 조건 ? 참일떄반환값 : 거짓일때반환값
    //}
    //
    //private bool TryGetComponentRef(ref Enemy enemy)
    //{
    //    enemy = gameObject.GetComponent<Enemy>();
    //    return enemy != null ? true : false;
    //}
}

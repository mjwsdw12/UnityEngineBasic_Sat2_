using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(1.0f, 1.0f);
    public void KnockBack(int knockBackDirection)
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(_knockBackForce.x * knockBackDirection,
                                 _knockBackForce.y),
                     ForceMode2D.Impulse);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
}

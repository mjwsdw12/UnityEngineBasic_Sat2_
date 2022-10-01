using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundDetector : MonoBehaviour
{
    public bool IsDetected => _currentGround;
    private Collider2D _currentGround;
    private Vector2 _size;
    private Vector2 _offset;
    [SerializeField] private LayerMask _groundLayer;

    private CapsuleCollider2D _col;

    private void Awake()
    {
        _col = GetComponent<CapsuleCollider2D>();
        _size = new Vector2(_col.size.x / 2, 0.01f);
        _offset = new Vector2(0.0f, -0.011f);
    }
    private void FixedUpdate()
    {
        _currentGround = Physics2D.OverlapBox((Vector2)transform.position + _offset, _size, 0, _groundLayer);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset, _size);
    }
}

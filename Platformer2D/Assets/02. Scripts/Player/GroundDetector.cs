using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundDetector : MonoBehaviour
{
    public bool IsDetected => _currentGround &&
                              _currentGround != _ignoringGround;

    private Collider2D _currentGround;
    private Collider2D _ignoringGround;
    private Coroutine _ignoreCoroutin;

    private Vector2 _size;
    private Vector2 _offset;
    [SerializeField] private LayerMask _groundLayer;

    private CapsuleCollider2D _col;

    public bool IsUnderGroundExist()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, transform.position + Vector3.down * 1.0f, _groundLayer);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != _currentGround)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IgnoreCurrentGround()
    {
        if (_currentGround == null)
            return false;

        _ignoreCoroutin = StartCoroutine(E_IgnoreGround(_currentGround));
        return true;
    }

    public void StopIgnoringGround()
    {
        if(_ignoringGround != null)
        {
            StopCoroutine(_ignoreCoroutin);
            Physics2D.IgnoreCollision(_col, _ignoringGround, false);
            _ignoringGround = null;
        }
    }

    IEnumerator E_IgnoreGround(Collider2D ground)
    {
        _ignoringGround = ground;
        Vector2 ignoreStartPos = _col.transform.position;
        Physics2D.IgnoreCollision(_col, _ignoringGround, true);

        yield return new WaitUntil(() =>
        {
            return _col.transform.position.y < ignoreStartPos.y - _col.size.y;
        });

        Physics2D.IgnoreCollision(_col, _ignoringGround, false);
        _ignoringGround = null;
    }

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

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    public bool CanGoUP, CanGoDown;

    public Vector2 GoUpStartPos => new Vector2(BottomPos.x, _rb.position.y + _col.offset.y);
    public Vector2 GoDownStartPos => new Vector2(TopPos.x, TopPos.y - _detectOffset - _col.size.y / 4.0f);
    public Vector2 GoUpEndPos => new Vector2(TopPos.x, TopPos.y - _detectOffset);
    public Vector2 GoDownEndPos => new Vector2(BottomPos.x, BottomPos.y - _col.offset.y);


    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _ladderLayer;
    public Vector2 TopPos, BottomPos;
    private float _detectOffset = 0.05f;
    private Collider2D _ladderUp;
    private Collider2D _ladderDown;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        _ladderUp = Physics2D.OverlapCircle(new Vector2(_rb.position.x, _rb.position.y + _col.offset.y), 0.01f, _ladderLayer);

        if (_ladderUp)
        {
            BoxCollider2D ladderBox = (BoxCollider2D)_ladderUp;
            TopPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.up * ladderBox.size.y / 2.0f;
            BottomPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.down * ladderBox.size.y / 2.0f;
            CanGoUP = true;
        }
        else
        {
            CanGoUP = false;
        }

        _ladderDown = Physics2D.OverlapCircle(new Vector2(_rb.position.x, _rb.position.y - _detectOffset), 0.01f, _ladderLayer);
        if (_ladderDown)
        {
            BoxCollider2D ladderBox = (BoxCollider2D)_ladderUp;
            TopPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.up * ladderBox.size.y / 2.0f;
            BottomPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.down * ladderBox.size.y / 2.0f;
            CanGoDown = true;
        }
        else
        {
            CanGoDown = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_rb == null)
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(new Vector2(_rb.position.x, _rb.position.y + _col.offset.y), 0.01f);
        Gizmos.DrawSphere(new Vector2(_rb.position.x, _rb.position.y - _detectOffset), 0.01f);
    }
}

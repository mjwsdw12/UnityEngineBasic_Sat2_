using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    public bool CanGoUP, CanGoDown;

    public Vector2 UpBottomStartPos => new Vector2(UpBottomPos.x, UpBottomPos.y + _col.offset.y);
    public Vector2 UpTopStartPos => new Vector2(UpTopPos.x, UpTopPos.y - _detectOffset);
    public Vector2 DownBottomStartPos => new Vector2(DownBottomPos.x, DownBottomPos.y + _col.offset.y);
    public Vector2 DownTopStartPos => new Vector2(DownTopPos.x, DownTopPos.y - _detectOffset);
    public Vector2 UpTopEndPos => new Vector2(UpTopPos.x, UpTopPos.y - _detectOffset);
    public Vector2 UpBottomEndPos => new Vector2(UpBottomPos.x, UpBottomPos.y - _col.offset.y);
    public Vector2 DownTopEndPos => new Vector2(DownTopPos.x, DownTopPos.y - _detectOffset);
    public Vector2 DownBottomEndPos => new Vector2(DownBottomPos.x, DownBottomPos.y - _col.offset.y);


    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _ladderLayer;
    public Vector2 UpTopPos, UpBottomPos, DownTopPos, DownBottomPos;
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
            UpTopPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.up * ladderBox.size.y / 2.0f;
            UpBottomPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.down * ladderBox.size.y / 2.0f;
            CanGoUP = true;
        }
        else
        {
            CanGoUP = false;
        }

        _ladderDown = Physics2D.OverlapCircle(new Vector2(_rb.position.x, _rb.position.y - _detectOffset), 0.01f, _ladderLayer);
        if (_ladderDown)
        {
            BoxCollider2D ladderBox = (BoxCollider2D)_ladderDown;
            DownTopPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.up * ladderBox.size.y / 2.0f;
            DownBottomPos = (Vector2)ladderBox.transform.position + ladderBox.offset + Vector2.down * ladderBox.size.y / 2.0f;
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

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(UpTopPos, 0.015f);
        Gizmos.DrawSphere(UpBottomPos, 0.015f);
        Gizmos.DrawSphere(DownTopPos, 0.015f);
        Gizmos.DrawSphere(DownBottomPos, 0.015f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(UpTopEndPos,0.015f);
        Gizmos.DrawSphere(UpBottomEndPos, 0.015f);
        Gizmos.DrawSphere(DownTopEndPos, 0.015f);
        Gizmos.DrawSphere(DownBottomEndPos, 0.015f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(UpBottomStartPos, 0.015f);
        Gizmos.DrawSphere(UpTopStartPos, 0.015f);
        Gizmos.DrawSphere(DownBottomStartPos, 0.015f);
        Gizmos.DrawSphere(DownTopStartPos, 0.015f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    public bool IsDetected => (BottomOn == true) && (TopOn == false);
    public float TopX => _rb.position.x + (_col.size.x / 2.0f * _col.gameObject.transform.lossyScale.x + 0.02f) * _machine.Direction;
    public float BottomX => _rb.position.x + (_col.size.x / 2.0f * _col.gameObject.transform.lossyScale.x + 0.02f) * _machine.Direction;
    public float TopY => _rb.position.y + _col.size.y * _col.gameObject.transform.lossyScale.y + 0.025f;
    public float BottomY => _rb.position.y + _col.size.y * _col.gameObject.transform.lossyScale.y - 0.025f;

    public bool TopOn, BottomOn;
    [SerializeField] private LayerMask _groundLayer;

    private StateMachine _machine;
    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _machine =GetComponent<StateMachine>();
        _col = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        TopOn = Physics2D.OverlapCircle(new Vector2(TopX, TopY),
                                        0.015f,
                                        _groundLayer);
        BottomOn = Physics2D.OverlapCircle(new Vector2(BottomX, BottomY),
                                        0.015f,
                                        _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if(_rb == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(TopX, TopY, 0.0f), 0.015f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(BottomX, BottomY, 0.0f), 0.015f);
    }
}

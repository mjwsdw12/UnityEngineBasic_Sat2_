using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public TowerInfo Info;
    public Node Node;
    [SerializeField] private Transform _rotatePoint;
    [SerializeField] private float _detectRange;
    [SerializeField] protected LayerMask TargetLayer;
    protected Transform Target;

    protected virtual void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        if (cols.Length > 0)
        {
            Target = cols[0].transform;
            _rotatePoint.LookAt(Target);
        }
        else
        {
            Target = null;
        }
    }

    private void OnMouseDown()
    {
        if (TowerHandler.Instance.gameObject.activeSelf == false)
            TowerUI.Instance.SetUp(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}

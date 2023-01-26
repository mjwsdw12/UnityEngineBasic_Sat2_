using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCaster : MonoBehaviour
{
    private bool _on;
    public bool on
    {
        get
        {
            return _on;
        }
        set
        {
            if (_on != value)
            {
                _targetsCasted.Clear();
            }
            _on = value;
        }
    }

    [SerializeField] private LayerMask _targetLayer;
    private Dictionary<int, GameObject> _targetsCasted = new Dictionary<int, GameObject>();

    public IEnumerable<GameObject> GetTargets() => _targetsCasted.Values;

    public void Clear() => _targetsCasted.Clear();

    private void OnTriggerStay(Collider other)
    {
        if (_on == false)
            return;

        if (((1<<other.gameObject.layer) & _targetLayer) > 0)
        {
            if (_targetsCasted.ContainsKey(other.gameObject.GetInstanceID()) == false)
            {
                _targetsCasted.Add(other.gameObject.GetInstanceID(), other.gameObject);
                Debug.Log($"target casted : {other.gameObject.name}");
            }
        }
    }
}

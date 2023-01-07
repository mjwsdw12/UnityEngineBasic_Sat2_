using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    public enum Mode
    {
        Auto,
        Manual
    }
    private Mode _mode;
    public Mode mode
    {
        get
        {
            return _mode;
        }
        set
        {
            if (_mode == value)
                return;

            _intertia = _rb.velocity;
            _mode = value;
        }
    }
    private Vector3 _intertia;
    [Range(0.0f, 1.0f)]
    private float _drag = 0.1f;

    private float _gain => Input.GetKey(KeyCode.LeftShift) ? 2.0f : 1.0f;
    private float _h => Input.GetAxis("Horizontal");
    private float _v => Input.GetAxis("Vertical");
    private float _multiplier = 0.5f;
    private Animator _animator;
    private Rigidbody _rb;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _animator.SetFloat("h", _h * _multiplier * _gain);
        _animator.SetFloat("v", _v * _multiplier * _gain);
    }

    private void FixedUpdate()
    {
        if (_mode == Mode.Manual)
        {
            transform.position += new Vector3(_intertia.x, 0.0f, _intertia.y) * Time.fixedDeltaTime;
            _intertia = Vector3.Lerp(_intertia, Vector3.zero, _drag * Time.fixedDeltaTime);
        }
    }

}

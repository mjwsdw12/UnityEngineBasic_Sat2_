using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : CharacterStateBase
{
    private Rigidbody _rb;
    private float _jumpForce = 3.0f;
    private float _startTimeMark;

    public PlayerStateJump(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions) 
        : base(id, owner, executeCondition, transitions)
    {
        _rb = owner.GetComponent<Rigidbody>();
    }

    public override void Execute()
    {
        base.Execute();
        movement.mode = Movement.Mode.Manual;
        animator.SetBool("doJump", true);        
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _startTimeMark = Time.time;
    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doJump", false);
    }

    public override int Update()
    {
        return (Time.time - _startTimeMark > 0.2f) ? base.Update() : id;
    }
}

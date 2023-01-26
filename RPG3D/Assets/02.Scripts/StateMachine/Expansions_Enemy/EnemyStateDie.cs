using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateDie : CharacterStateBase
{
    private EnemyMovement _movement;
    public EnemyStateDie(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions) : base(id, owner, executeCondition, transitions)
    {
        _movement = owner.GetComponent<EnemyMovement>();
    }

    public override void Execute()
    {
        base.Execute();
        animator.SetBool("doDie", true);
        _movement.StopMove();
    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doDie", false);
    }
}

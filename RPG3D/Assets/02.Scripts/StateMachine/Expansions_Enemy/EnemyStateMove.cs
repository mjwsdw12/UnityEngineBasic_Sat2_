using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMove : CharacterStateBase
{
    private EnemyMovement _movement;
    public EnemyStateMove(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions) : base(id, owner, executeCondition, transitions)
    {
        _movement = owner.GetComponent<EnemyMovement>();
    }

    public override void Execute()
    {
        base.Execute();
        animator.SetBool("doMove", true);
    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doMove", false);
    }

    public override int Update()
    {
        animator.SetFloat("MoveBlend", _movement.speed);

        return base.Update();
    }
}

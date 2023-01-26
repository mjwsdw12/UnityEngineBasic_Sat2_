using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttack : CharacterStateBase
{
    public EnemyStateAttack(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions) : base(id, owner, executeCondition, transitions)
    {
    }

    public override void Execute()
    {
        base.Execute();
        animator.SetBool("doAttack", true);
        Debug.Log("DoAttack");

    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doAttack", false);
        Debug.Log("StopAttack");
    }

    public override int Update()
    {
        if (animator.GetNormalizedTime() > 0.95f &&
            animator.isPreviousStateFinished)
            return base.Update();

        return id;
    }
}

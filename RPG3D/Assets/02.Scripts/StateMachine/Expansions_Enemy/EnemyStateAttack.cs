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
    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doAttack", false);
    }

    public override int Update()
    {
        if (animator.GetNormalizedTime() > 0.95f)
            return base.Update();

        return id;
    }
}

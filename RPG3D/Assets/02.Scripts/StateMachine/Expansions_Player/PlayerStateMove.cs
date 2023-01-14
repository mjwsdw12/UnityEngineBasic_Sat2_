using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : CharacterStateBase
{
    public PlayerStateMove(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions) 
        : base(id, owner, executeCondition, transitions)
    {
    }

    public override void Execute()
    {        
        base.Execute();
        movement.mode = Movement.Mode.Auto;
        animator.SetBool("doMove", true);
    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doMove", false);
    }
}

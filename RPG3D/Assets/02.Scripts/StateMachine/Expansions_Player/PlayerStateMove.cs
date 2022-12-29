using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : CharacterStateBase
{
    public PlayerStateMove(int id, GameObject owner, Func<bool> executeCondition) 
        : base(id, owner, executeCondition)
    {
    }
}

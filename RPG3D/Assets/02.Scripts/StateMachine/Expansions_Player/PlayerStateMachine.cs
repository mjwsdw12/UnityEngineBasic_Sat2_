using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : CharacterStateMachine
{
    public PlayerStateMachine(GameObject owner) : base(owner)
    {
    }

    public override void InitStates()
    {
        IState move = new PlayerStateMove(id: (int)StateType.Move,
                                          owner: owner,
                                          executeCondition: () => true);
        states.Add(StateType.Move, move);
    }
}

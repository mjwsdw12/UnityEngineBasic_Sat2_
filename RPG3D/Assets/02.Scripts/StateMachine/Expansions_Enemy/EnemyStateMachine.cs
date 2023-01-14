using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : CharacterStateMachine
{
    

    public EnemyStateMachine(GameObject owner) : base(owner)
    {
    }



    public override void InitStates()
    {
        GroundDetector groundDetector = owner.GetComponent<GroundDetector>();
        Rigidbody rb = owner.GetComponent<Rigidbody>();

        IState move = new EnemyStateMove(id: (int)StateType.Move,
                                          owner: owner,
                                          executeCondition: () => true,
                                          transitions: new List<KeyValuePair<Func<bool>, int>>()
                                          {
                                              new KeyValuePair<Func<bool>, int>
                                              (
                                                  () => false,
                                                  0
                                              )
                                          });
        states.Add(StateType.Move, move);

        IState attack = new EnemyStateAttack(id: (int)StateType.Attack,
                                          owner: owner,
                                          executeCondition: () => currentType == StateType.Move,
                                          transitions: new List<KeyValuePair<Func<bool>, int>>()
                                          {
                                              new KeyValuePair<Func<bool>, int>
                                              (
                                                  () => true,
                                                  (int)StateType.Move
                                              )
                                          });
        states.Add(StateType.Attack, attack);
    }
}

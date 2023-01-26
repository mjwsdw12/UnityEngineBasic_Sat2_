using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerStateMachine : CharacterStateMachine
{
    public PlayerStateMachine(GameObject owner) : base(owner)
    {
    }

    public override void InitStates()
    {
        GroundDetector groundDetector = owner.GetComponent<GroundDetector>();
        Rigidbody rb = owner.GetComponent<Rigidbody>();

        IState move = new PlayerStateMove(id: (int)StateType.Move,
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

        IState jump = new PlayerStateJump(id: (int)StateType.Jump,
                                          owner: owner,
                                          executeCondition: () => groundDetector.isDetected,
                                          transitions: new List<KeyValuePair<Func<bool>, int>>()
                                          {
                                              new KeyValuePair<Func<bool>, int>
                                              (
                                                  () => rb.velocity.y <= 0 && groundDetector.isDetected,
                                                  (int)StateType.Land
                                              ),
                                              new KeyValuePair<Func<bool>, int>
                                              (
                                                  () => rb.velocity.y <= 0,
                                                  (int)StateType.Fall
                                              )
                                          });
        states.Add(StateType.Jump, jump);

        IState fall = new PlayerStateFall(id: (int)StateType.Fall,
                                          owner: owner,
                                          executeCondition: () => true,
                                          transitions: new List<KeyValuePair<Func<bool>, int>>()
                                          {
                                              new KeyValuePair<Func<bool>, int>
                                              (
                                                  () => groundDetector.isDetected,
                                                  (int)StateType.Land
                                              )
                                          });
        states.Add(StateType.Fall, fall);

        IState land = new PlayerStateLand(id: (int)StateType.Land,
                                          owner: owner,
                                          executeCondition: () => true,
                                          transitions: new List<KeyValuePair<Func<bool>, int>>()
                                          {
                                              new KeyValuePair<Func<bool>, int>
                                              (
                                                  () => true,
                                                  (int)StateType.Move
                                              )
                                          });
        states.Add(StateType.Land, land);

        IState attack = new PlayerStateAttack(id: (int)StateType.Attack,
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

        InputHandler.main.RegisterKeyPressAction(KeyCode.Space, () => ChangeState(StateType.Jump));
        InputHandler.main.OnMouse0TriggerActive += () => ChangeState(StateType.Attack);
    }
}

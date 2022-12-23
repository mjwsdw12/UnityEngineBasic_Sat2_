using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDie : StateBase
{
    private Player _player;
    public StateDie(StateMachine.StateType machineType, StateMachine machine)
        : base(machineType, machine)
    {
        _player = Machine.GetComponent<Player>();   
    }

    public override bool IsExecuteOK => Machine.Current != StateMachine.StateType.Attack;

    public override void Execute()
    {
        Machine.IsMovable = false;
        Machine.IsDirectionChangable = false;
        _player.Invinciable = true;
        Current = IState.Commands.Prepare;
    }

    public override void FixedUpdate()
    {
    }

    public override void ForceStop()
    {
        _player.Invinciable = false;
        Current = IState.Commands.Idle;
    }

    public override StateMachine.StateType Update()
    {
        StateMachine.StateType next = MachineType;

        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("Die");
                    Machine.StopMove();
                    MoveNext();
                }
                break;
            case IState.Commands.Casting:
                {
                    MoveNext();
                }
                break;
            case IState.Commands.OnAction:
                {
                    if (AnimationManager.GetNormalizedTime() > 1.0f)
                        MoveNext();
                }
                break;
            case IState.Commands.Finish:
                {
                }
                break;
            default:
                break;
        }

        return next;
    }
}

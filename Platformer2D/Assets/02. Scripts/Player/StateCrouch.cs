using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCrouch : StateBase
{
    private CapsuleCollider2D _col;
    private Vector2 _colOffsetOrigin;
    private Vector2 _colSizeOrigin;
    private Vector2 _colOffsetCrouch = new Vector2(0.0f, 0.07f);
    private Vector2 _colSizeCrouch = new Vector2(0.15f, 0.15f);
    public StateCrouch(StateMachine.StateType machinType, StateMachine machine)
        : base(machinType, machine)
    {
        _col = machine.GetComponent<CapsuleCollider2D>();
        _colOffsetOrigin = _col.offset;
        _colSizeOrigin = _col.size;
    }
    public override bool IsExecuteOK => Machine.Current == StateMachine.StateType.Idle ||
                                        Machine.Current == StateMachine.StateType.Move; 

    public override void Execute()
    {
        Current = IState.Commands.Prepare;
        Machine.IsDirectionChangable = true;
        Machine.IsMovable = false;
        Machine.StopMove();
        _col.offset = _colOffsetCrouch;
        _col.size = _colSizeCrouch;
    }

    public override void FixedUpdate()
    {
    }

    public override void ForceStop()
    {
        Current = IState.Commands.Idle;
        _col.offset = _colOffsetOrigin;
        _col.size = _colSizeOrigin;
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
                    AnimationManager.Play("Crouch");
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
                    if(Input.GetKey(KeyCode.DownArrow) == false)
                    {
                        MoveNext();
                    }
                }
                break;
            case IState.Commands.Finish:
                {
                    next = StateMachine.StateType.Idle;
                }
                break;
            default:
                break;
        }

        return next;
    }
}

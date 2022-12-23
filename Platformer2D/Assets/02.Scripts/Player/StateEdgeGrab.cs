using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEdgeGrab : StateBase
{
    private enum EdgeTypes
    {
        Grab,
        Idle,
        Climb
    }
    private EdgeTypes _edgeType;
    private EdgeDetector _edgeDetector;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Vector2 _grabPos;
    private Vector2 _climbPos;
    public StateEdgeGrab(StateMachine.StateType machineType, StateMachine machine) 
        : base(machineType, machine)
    {
        _edgeDetector = machine.GetComponent<EdgeDetector>();
        _col = machine.GetComponent<CapsuleCollider2D>();
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool IsExecuteOK => _edgeDetector.IsDetected &&
                                        (Machine.Current == StateMachine.StateType.Idle ||
                                         Machine.Current == StateMachine.StateType.Move ||
                                         Machine.Current == StateMachine.StateType.Jump ||
                                         Machine.Current == StateMachine.StateType.Fall);



    public override void Execute()
    {
        Machine.IsMovable = false;
        Machine.IsDirectionChangable = false;
        Machine.StopMove();
        _edgeType = EdgeTypes.Grab;
        _rb.velocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        Current = IState.Commands.Prepare;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void ForceStop()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        Current = IState.Commands.Idle;
    }

    public override StateMachine.StateType Update()
    {
        switch (_edgeType)
        {
            case EdgeTypes.Grab:
                return EdgeGrabWorkflow();
            case EdgeTypes.Idle:
                return EdgeIdleWorkflow();
            case EdgeTypes.Climb:
                return EdgeClimbWorkflow();
            default:
                throw new System.Exception("[StateEgdeGrab] : Invalid grab type");
        }
    }

    private StateMachine.StateType EdgeGrabWorkflow()
    {
        StateMachine.StateType next = MachineType;
        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("EdgeGrab");
                    _grabPos = _rb.position;
                    _climbPos = _rb.position + new Vector2(_col.size.x * Machine.Direction, _col.size.y);
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
                    if (AnimationManager.GetNormalizedTime() >= 1.0f)
                        MoveNext();
                }
                break;
            case IState.Commands.Finish:
                {
                    _edgeType = EdgeTypes.Idle;
                    Current = IState.Commands.Prepare;
                }
                break;
            default:
                break;
        }
        return next;
    }
    private StateMachine.StateType EdgeIdleWorkflow()
    {
        StateMachine.StateType next = MachineType;
        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("EdgeIdle");
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
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        _edgeType = EdgeTypes.Climb;
                        Current = IState.Commands.Prepare;
                    }
                }
                break;
            case IState.Commands.Finish:
                break;
            default:
                break;
        }
        return next;
    }
    private StateMachine.StateType EdgeClimbWorkflow()
    {
        StateMachine.StateType next = MachineType;
        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("EdgeClimb");
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
                    _rb.position = Vector2.Lerp(_grabPos, _climbPos, AnimationManager.GetNormalizedTime() * 2.0f);
                    if (AnimationManager.GetNormalizedTime() >= 1.0f)
                        MoveNext();
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

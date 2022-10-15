using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateLadderUp : StateBase
{
    private LadderDetector _ladderDetector;
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    private float _h => Input.GetAxis("Horizontal");
    private float _v => Input.GetAxisRaw("Vertical");
    public StateLadderUp(StateMachine.StateType machineType, StateMachine machine) 
        : base(machineType, machine)
    {
        _ladderDetector = machine.GetComponent<LadderDetector>();
        _groundDetector = machine.GetComponent<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool IsExecuteOK => _ladderDetector.CanGoUP &&
                                        (Machine.Current == StateMachine.StateType.Idle ||
                                         Machine.Current == StateMachine.StateType.Move ||
                                         Machine.Current == StateMachine.StateType.Jump ||
                                         Machine.Current == StateMachine.StateType.Fall);

    public override void Execute()
    {
        Machine.IsMovable = false;
        Machine.IsDirectionChangable = false;
        AnimationManager.Speed = 0.0f;
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
        StateMachine.StateType next = MachineType;

        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("Ladder");
                    Machine.StopMove();
                    _rb.velocity = Vector2.zero;
                    _rb.position = _ladderDetector.GoUpStartPos;
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
                    AnimationManager.Speed = Mathf.Abs(_v);
                    _rb.MovePosition(_rb.position + Vector2.up * _v * Time.deltaTime);

                    // 사다리 위로 올라가는 조건
                    if (_rb.position.y > _ladderDetector.GoUpEndPos.y)
                    {
                        _rb.position = _ladderDetector.TopPos;
                        MoveNext();
                    }
                    // 사다리 밑으로 떨어지는 조건
                    else if (_rb.position.y < _ladderDetector.GoDownEndPos.y ||
                            _groundDetector.IsDetected)
                    {
                        MoveNext();
                    }

                    // 점프
                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        if( Mathf.Abs(_h) > 0.0f)
                        {
                            if (_h < 0.0f)
                                Machine.Direction = Constants.DIRECTION_LEFT;
                            else if (_h > 0.0f)
                                Machine.Direction = Constants.DIRECTION_RIGHT;

                            Machine.SetMove(Vector2.right * _h);
                            Machine.ForceChangeState(StateMachine.StateType.Jump);
                            next = StateMachine.StateType.Jump;
                        }
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
}

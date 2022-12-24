using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLadderUp : StateBase
{
    private LadderDetector _ladderDetector;
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    private CharacterBase _character;
    private float _h => Input.GetAxis("Horizontal");
    private float _v => Input.GetAxisRaw("Vertical");
    public StateLadderUp(StateMachine.StateType machineType, StateMachine machine) 
        : base(machineType, machine)
    {
        _ladderDetector = machine.GetComponent<LadderDetector>();
        _groundDetector = machine.GetComponent<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
        _character = machine.GetComponent<CharacterBase>();
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
        AnimationManager.Speed = 1.0f;
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
                    _rb.position = _ladderDetector.UpBottomStartPos;
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
                    _rb.MovePosition(_rb.position + Vector2.up * _v * _character.MoveSpeed * Time.deltaTime);

                    // ��ٸ� ���� �ö󰡴� ����
                    if (_rb.position.y > _ladderDetector.UpTopEndPos.y)
                    {
                        _rb.position = _ladderDetector.UpTopPos;
                        next = StateMachine.StateType.Idle;
                        MoveNext();
                    }
                    // ��ٸ� ������ �������� ����
                    else if (_rb.position.y < _ladderDetector.UpBottomEndPos.y)
                    {
                        next = StateMachine.StateType.Idle;
                        MoveNext();
                    }
                    // ���� ��� ����
                    else if (_rb.position.y < _ladderDetector.UpBottomStartPos.y && 
                             _groundDetector.IsDetected)
                    {
                        next = StateMachine.StateType.Idle;
                        MoveNext();
                    }

                    // ����
                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        if (Mathf.Abs(_h) > 0.0f)
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

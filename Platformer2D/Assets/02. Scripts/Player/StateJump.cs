using UnityEngine;
public class StateJump : StateBase
{
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    private bool _isDownJump;
    public StateJump(StateMachine.StateType machineType, StateMachine machine) 
        : base(machineType, machine)
    {
        _groundDetector = machine.GetComponentInChildren<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool IsExecuteOK => _groundDetector.IsDetected &&
                                        (Machine.Current == StateMachine.StateType.Idle ||
                                         Machine.Current == StateMachine.StateType.Move ||
                                         Machine.Current == StateMachine.StateType.Crouch);

    public override void Execute()
    {

        Current = IState.Commands.Prepare;
        Machine.IsDirectionChangable = true;
        Machine.IsMovable = false;

        _groundDetector.StopIgnoringGround();

        if (Machine.Current == StateMachine.StateType.Crouch &&
            _groundDetector.IsUnderGroundExist())
            _isDownJump = true;
        else
            _isDownJump = false;
    }

    public override void FixedUpdate()
    {
       
    }

    public override void ForceStop()
    {
        Current = IState.Commands.Idle;
        _isDownJump = false;
    }

    public override void MoveNext()
    {
        Current++;
    }

    public override StateMachine.StateType Update()
    {
        if(_isDownJump)
            return DownJumpWorkflow();
        else
            return JumpWorkflow();
    }

    private StateMachine.StateType JumpWorkflow()
    {
        StateMachine.StateType next = MachineType;

        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("Jump");
                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * Character.JumpForce, ForceMode2D.Impulse);
                    MoveNext();
                }
                break;
            case IState.Commands.Casting:
                {
                    if (_groundDetector.IsDetected == false)
                        MoveNext();
                }
                break;
            case IState.Commands.OnAction:
                {
                    if (_rb.velocity.y < 0.0f)
                        MoveNext();
                }
                break;
            case IState.Commands.Finish:
                {
                    if (_groundDetector.IsDetected)
                        next = StateMachine.StateType.Idle;
                    else
                        next = StateMachine.StateType.Fall;
                }
                break;
            default:
                break;
        }
        return next;
    }

    private StateMachine.StateType DownJumpWorkflow()
    {
        StateMachine.StateType next = MachineType;

        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("Jump");
                    _groundDetector.IgnoreCurrentGround();
                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * Character.DownJumpForce, ForceMode2D.Impulse);
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
                    if (_rb.velocity.y < 0.0f)
                        MoveNext();
                }
                break;
            case IState.Commands.Finish:
                {
                    next = StateMachine.StateType.Fall;
                }
                break;
            default:
                break;
        }
        return next;
    }
}
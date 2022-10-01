public class StateFall : StateBase
{
    private GroundDetector _groundDetector;
    public StateFall(StateMachine.StateType machineType, StateMachine machine)
        : base(machineType, machine)
    {
        _groundDetector = machine.GetComponent<GroundDetector>();
    }

    public override bool IsExecuteOK => _groundDetector.IsDetected == false &&
                                        Machine.Current != StateMachine.StateType.Attack;

    public override void Execute()
    {
        Current = IState.Commands.Prepare;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void ForceStop()
    {
        Current = IState.Commands.Idle;
    }

    public override void MoveNext()
    {
        Current++;
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
                    Animator.Play("Fall");
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
                    if (_groundDetector.IsDetected)
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
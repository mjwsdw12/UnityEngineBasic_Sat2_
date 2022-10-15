public class StateIdle : StateBase
{
    public StateIdle(StateMachine.StateType machineType, StateMachine machine)
        : base(machineType, machine)
    {
    }

    public override bool IsExecuteOK => true;

    public override void Execute()
    {
        Current = IState.Commands.Prepare;
        Machine.IsDirectionChangable = true;
        Machine.IsMovable = true;
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
                    AnimationManager.Play("Idle");
                    MoveNext();
                }
                break;
            case IState.Commands.Casting:
                {
                    MoveNext();
                }
                break;
            case IState.Commands.OnAction:
                // nothing to do
                break;
            case IState.Commands.Finish:
                break;
            default:
                break;
        }

        return next;
    }
}
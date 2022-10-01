public class StateMove : StateBase
{
    public StateMove(StateMachine.StateType machineType, StateMachine machine)
        : base(machineType, machine)
    {
    }

    public override bool IsExecuteOK => true;

    public override void Execute()
    {
       
    }

    public override void FixedUpdate()
    {
        
    }

    public override void ForceStop()
    {
        
    }

    public override void MoveNext()
    {
        
    }

    public override StateMachine.StateType Update()
    {
        return MachineType;
    }
}
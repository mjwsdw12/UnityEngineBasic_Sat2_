using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : IState
{
    protected StateMachine Machine;
    protected StateMachine.StateType MachineType;
    protected Animator Animator;
    public StateBase(StateMachine.StateType machineType, StateMachine machine)
    {
        MachineType = machineType;
        Machine = machine;
        Animator = Machine.GetComponent<Animator>();
    }

    public IState.Commands Current { get; protected set; }

    public abstract bool IsExecuteOK { get; }

    public abstract void Execute();

    public abstract void FixedUpdate();

    public abstract void ForceStop();

    public abstract void MoveNext();

    public abstract StateMachine.StateType Update();
    
}

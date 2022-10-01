using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : IState
{
    protected StateMachine Machine;
    protected StateMachine.StateType MachineType;
    protected AnimationManager AnimationManager;
    protected CharacterBase Character;
    public StateBase(StateMachine.StateType machineType, StateMachine machine)
    {
        MachineType = machineType;
        Machine = machine;
        AnimationManager = Machine.GetComponent<AnimationManager>();
        Character = Machine.GetComponent<CharacterBase>();
    }

    public IState.Commands Current { get; protected set; }

    public abstract bool IsExecuteOK { get; }

    public abstract void Execute();

    public abstract void FixedUpdate();

    public abstract void ForceStop();

    public virtual void MoveNext() => Current++;

    public abstract StateMachine.StateType Update();
    
}

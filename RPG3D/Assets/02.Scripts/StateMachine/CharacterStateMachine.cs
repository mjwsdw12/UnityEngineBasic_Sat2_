using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStateMachine
{
    public enum StateType
    {
        Move,
        Jump,
        Fall,
        Land,
        Attack,
        Hurt,
        Die
    }

    public StateType currentType;
    public IState current;
    protected Dictionary<StateType, IState> states;
    protected GameObject owner;

    public CharacterStateMachine(GameObject owner)
    {
        this.owner = owner;
        states = new Dictionary<StateType, IState>();

        InitStates();

        currentType = default(StateType);
        current = states[currentType];
    }

    public abstract void InitStates();

    public void Update()
    {
        current.Update();
    }

    public bool ChangeState(StateType nextType)
    {
        if (currentType == nextType)
            return false;

        if (states[nextType].canExecute)
        {
            current.Stop();
            current = states[nextType];
            currentType = nextType;
            return true;
        }

        return false;
    }
}

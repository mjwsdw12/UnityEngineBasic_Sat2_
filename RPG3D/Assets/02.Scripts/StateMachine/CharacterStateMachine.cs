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
        current.Execute();
    }

    public abstract void InitStates();

    public void Update()
    {
        if (ChangeState((StateType)current.Update()))
        {
            Debug.Log($"[StateMachine] : {owner}'s state has changed as {currentType}");
        }
    }

    public bool ChangeState(StateType nextType)
    {
        if (currentType == nextType)
            return false;

        if (states[nextType].canExecute)
        {
            current.Stop();
            current = states[nextType];
            current.Execute();
            currentType = nextType;
            return true;
        }

        return false;
    }
}

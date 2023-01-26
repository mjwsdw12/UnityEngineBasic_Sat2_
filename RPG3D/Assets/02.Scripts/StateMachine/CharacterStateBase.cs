using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStateBase : IState
{
    protected GameObject owner;
    protected Func<bool> condition;
    protected AnimatorWrapper animator;
    protected Movement movement;
    public int id { get; set; }

    public int current { get; set; }

    public bool canExecute => condition.Invoke() &&
                              animator.isPreviousMachineFinished &&
                              animator.isPreviousStateFinished;

    public List<KeyValuePair<Func<bool>, int>> transitions { get; set; }

    public CharacterStateBase(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions)
    {
        this.id = id;
        this.owner = owner;
        this.condition = executeCondition;
        this.transitions = transitions;
        animator = owner.GetComponent<AnimatorWrapper>();
        movement = owner.GetComponent<Movement>();
    }


    public virtual void Execute()
    {
        current = 0;
    }

    public virtual void Stop()
    {
    }
    public void MoveNext()
    {
        current++;
    }

    public virtual int Update()
    {
        int nextId = id;

        foreach (var transition in transitions)
        {
            if (transition.Key.Invoke())
            {
                nextId = transition.Value;
                break;
            }
        }

        return nextId;
    }

}
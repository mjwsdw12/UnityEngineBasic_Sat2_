using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum States
    {
        Idel,
        Move,
        Attack,
        Jump,
        Fall,
    }

    public enum JumpStates
    {
        Idel,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    public enum FallStates
    {
        Idel,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    public enum AttackStates
    {
        Idel,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    States _state;
    JumpStates _jumpStates;
    FallStates _fallStates;
    AttackStates _attackStates;

    private void Update()
    {
        switch (_state)
        {
            case States.Idel:
                break;
            case States.Move:
                break;
            case States.Attack:
                break;
            case States.Jump:
                break;
            case States.Fall:
                break;
            default:
                break;
        }
        switch (_jumpStates)
        {
            case JumpStates.Idel:
                break;
            case JumpStates.Prepare:
                break;
            case JumpStates.Casting:
                break;
            case JumpStates.OnAction:
                break;
            case JumpStates.Finish:
                break;
            default:
                break;
        }
    }
}


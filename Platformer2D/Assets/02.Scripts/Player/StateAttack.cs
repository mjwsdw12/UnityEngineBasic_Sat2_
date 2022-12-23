using System.Diagnostics;
using UnityEngine;

public class StateAttack : StateBase
{
    private LayerMask _enemyLayer;
    private Vector2 _boxCastCenter = new Vector2(0.16f, 0.16f);
    private Vector2 _boxCastSize = new Vector2(0.7f, 0.5f);
    public StateAttack(StateMachine.StateType machineType, StateMachine machine)
        : base(machineType, machine)
    {
        _enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    public override bool IsExecuteOK => Machine.Current == StateMachine.StateType.Idle ||
                                        Machine.Current == StateMachine.StateType.Move ||
                                        Machine.Current == StateMachine.StateType.Jump ||
                                        Machine.Current == StateMachine.StateType.Fall;

    public override void Execute()
    {
        Current = IState.Commands.Prepare;
        Machine.IsDirectionChangable = false;
        Machine.IsMovable = false;
    }

    public override void FixedUpdate()
    {
    }

    public override void ForceStop()
    {
        Current = IState.Commands.Idle;
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
                    AnimationManager.Play("Attack");
                    MoveNext();
                }
                break;
            case IState.Commands.Casting:
                {
                    if (AnimationManager.IsCastingFinished)
                    {
                        RaycastHit2D hit = Physics2D.BoxCast(Machine.transform.position + Vector3.right * Machine.Direction * _boxCastCenter.x,
                                                             _boxCastSize,
                                                             0,
                                                             Vector2.zero,
                                                             0,
                                                             _enemyLayer);

                        if (hit.collider != null)
                        {
                            hit.collider.GetComponent<Enemy>().Hurt(Character.ATK);
                            hit.collider.GetComponent<EnemyController>().KnockBack(Machine.Direction);
                        }

                        MoveNext();
                    }
                }
                break;
            case IState.Commands.OnAction:
                {
                    if (AnimationManager.GetNormalizedTime() >= 1.0f)
                    {
                        MoveNext();
                    }
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
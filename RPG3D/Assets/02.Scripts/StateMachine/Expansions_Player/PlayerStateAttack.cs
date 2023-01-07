using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack : CharacterStateBase
{
    public PlayerStateAttack(int id, GameObject owner, Func<bool> executeCondition, List<KeyValuePair<Func<bool>, int>> transitions) 
        : base(id, owner, executeCondition, transitions)
    {
    }

    public override void Execute()
    {
        base.Execute();
        movement.mode = Movement.Mode.Auto;
        animator.SetBool("doAttack", true);
    }

    public override void Stop()
    {
        base.Stop();
        animator.SetBool("doAttack", false);
    }

    public override int Update()
    {
        switch (current)
        {
            // Start
            //---------------------------------
            case 0:
                {
                    InputHandler.main.Mouse0Trigger = false;
                    current = 1;
                }
                break;
            // 
            //---------------------------------
            case 1:
                {
                    if (InputHandler.main.Mouse0Trigger &&
                        animator.GetBool("finishCombo") == false)
                    {
                        animator.SetBool("doCombo", true);
                        current = 2;
                    }

                    if (animator.GetNormalizedTime() >= 0.95f)
                    {
                        return base.Update();
                    }
                }
                break;
            // Combo 정상 실행 되었는지 체크
            //---------------------------------
            case 2:
                {
                    if (animator.isPreviousStateFinished)
                    {
                        animator.SetBool("doCombo", false);
                        current = 0;
                    }
                }
                break;
            default:
                break;
        }

        return id;
    }
}

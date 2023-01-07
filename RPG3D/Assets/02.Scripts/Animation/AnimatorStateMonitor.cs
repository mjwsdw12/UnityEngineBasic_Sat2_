using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimatorStateMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;
    public string boolParameterNameToToggle;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (string.IsNullOrEmpty(boolParameterNameToToggle) == false)
            animator.SetBool(boolParameterNameToToggle, true);

        OnEnter?.Invoke(stateInfo.fullPathHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (string.IsNullOrEmpty(boolParameterNameToToggle) == false)
            animator.SetBool(boolParameterNameToToggle, false);

        OnExit?.Invoke(stateInfo.fullPathHash);
    }
}

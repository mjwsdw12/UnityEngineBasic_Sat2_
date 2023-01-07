using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDummyStateMonitor : StateMachineBehaviour
{
    [SerializeField] private string boolParameterToOn;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (string.IsNullOrEmpty(boolParameterToOn) == false)
            animator.SetBool(boolParameterToOn, true);
    }
}

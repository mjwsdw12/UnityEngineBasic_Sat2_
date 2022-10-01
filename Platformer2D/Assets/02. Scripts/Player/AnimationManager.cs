using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    public bool IsCastingFinished {  get; private set; }
    public void OnCastingFinished()
    {
        IsCastingFinished = true;
    }

    public void Play(string clipname)
    {
        IsCastingFinished = false;
        _animator.Play(clipname);
    }

    public float GetNormallizedTime()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}

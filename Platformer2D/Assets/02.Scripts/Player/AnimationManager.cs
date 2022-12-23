using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    public bool IsCastingFinished { get; private set; }
    public float Speed { get => _animator.speed;
                         set => _animator.speed = value; }
    public void OnCastingFinished()
    {
        IsCastingFinished = true;
    }

    public void Play(string clipName)
    {
        IsCastingFinished = false;
        _animator.Play(clipName);
    }

    public float GetNormalizedTime()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}

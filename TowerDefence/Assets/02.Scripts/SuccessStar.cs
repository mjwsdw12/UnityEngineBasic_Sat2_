using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessStar : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void Show()
    {
        gameObject.SetActive(true);
        _animator.Play("Star");
    }
}

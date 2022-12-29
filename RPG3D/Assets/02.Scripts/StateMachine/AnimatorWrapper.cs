using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorWrapper : MonoBehaviour
{
    public bool isPreviousStateFinished { get; }
    public bool isPreviousMachineFinished { get; }
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
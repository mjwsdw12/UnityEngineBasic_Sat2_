using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorWrapper : MonoBehaviour
{
    public bool isPreviousStateFinished => _monitorOnStateHashMem == _monitorOffStateHash;
    public bool isPreviousMachineFinished => _monitorOnMachineHashMem == _monitorOffMachineHash;

    private int _monitorOnStateHash; // 감시자 켜진 애니메이터 상태의 해쉬코드
    private int _monitorOnStateHashMem; // 감시자 켜졌던 애니메이터 상태의 해쉬코드
    private int _monitorOffStateHash; // 감시자 꺼진 애니메이터 상태의 해쉬코드

    private int _monitorOnMachineHash; // 감시자 켜진 애니메이터 서브머신의 해쉬코드
    private int _monitorOnMachineHashMem; // 감시자 켜졌던 애니메이터 서브머신의 해쉬코드
    private int _monitorOffMachineHash; // 감시자 꺼진 애니메이터 서브머신의 해쉬코드

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        foreach (AnimatorStateMonitor monitor in _animator.GetBehaviours<AnimatorStateMonitor>())
        {
            monitor.OnEnter += (hash) =>
            {
                _monitorOnStateHashMem = _monitorOnStateHash;
                _monitorOnStateHash = hash;
            };

            monitor.OnExit += (hash) =>
            {
                _monitorOffStateHash = hash;
            };
        }

        foreach (AnimatorMachineMonitor monitor in _animator.GetBehaviours<AnimatorMachineMonitor>())
        {
            monitor.OnEnter += (hash) =>
            {
                _monitorOnMachineHashMem = _monitorOnMachineHash;
                _monitorOnMachineHash = hash;
            };

            monitor.OnExit += (hash) =>
            {
                _monitorOffMachineHash = hash;
            };
        }
    }

    public void SetBool(string clipName, bool value) => _animator.SetBool(clipName, value);
    public bool GetBool(string clipName) => _animator.GetBool(clipName);
    public void SetInt(string clipName, int value) => _animator.SetInteger(clipName, value);
    public int GetInt(string clipName) => _animator.GetInteger(clipName);
    public void SetFloat(string clipName, float value) => _animator.SetFloat(clipName, value);
    public float GetFloat(string clipName, float value) => _animator.GetFloat(clipName);

    public float GetNormalizedTime() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
}
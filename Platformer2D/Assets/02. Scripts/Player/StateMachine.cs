using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Crouch,
        EOF
    }

    public StateType Current;
    private Dictionary<StateType, StateBase> _states = new Dictionary<StateType, StateBase>();
    private StateBase _CurrentState;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (StateType stateType = StateType.Idle; stateType < StateType.EOF; stateType++)
        {
            AddState(stateType);
        }
        _CurrentState = _states[StateType.Idle];
        Current = StateType.Idle;
    }

    private void AddState(StateType stateType)
    {
        // 이미 상태가 등록 되었다면
        if (_states.ContainsKey(stateType))
            return;

        string stateName = Convert.ToString(stateType);
        string typeName = "State" + stateName;
        Debug.Log($"[StateMachine] : Adding state ... {stateName}");

        Type type = Type.GetType(typeName);

        if (type != null)
        {
            ConstructorInfo constructor = type.GetConstructor(new Type[]
            {
                typeof (StateType),
                typeof (StateMachine)
            });


            StateBase state 
                = constructor.Invoke(new object[2]
                  {
                      stateType,
                      this
                  }) as StateBase;

            _states.Add(stateType, state);
            Debug.Log($"[StateMachine] : {stateName} state is successfully added");
        }
    }

    private void Update()
    {
        ChangeState(_CurrentState.Update());

        if (Input.GetKey(KeyCode.LeftAlt))
            ChangeState(StateType.Jump);
    }

    private void ChangeState(StateType newStateType)
    {
        // 상태가 바뀌지 않았으면
        if (Current == newStateType)
            return;

        // 바꾸려는 상태가 실행 가능하지 않으면
        if (_states[newStateType].IsExecuteOK == false)
            return;

        _CurrentState.ForceStop();              // 기존 상태 중단
        _CurrentState = _states[newStateType];  // 상태 갱신
        _CurrentState.Execute();                // 갱신된 상태 실행
        Current = newStateType;
    }
}

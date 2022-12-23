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
        EdgeGrab,
        LadderUp,
        LadderDown,
        Hurt,
        Die,
        EOF
    }
    public StateType Current;
    private Dictionary<StateType, StateBase> _states = new Dictionary<StateType, StateBase>();
    private StateBase _currentState;
    private bool _isStateChanged;
    private CharacterBase _character;

    private float _h => Input.GetAxis("Horizontal");
    private float _v => Input.GetAxis("Vertical");
    private Vector2 _move;
    public bool IsMovable { get; set; }
    public bool IsDirectionChangable { get; set; }
    // -1 : left , 1 : right
    private int _direction;
    public int Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value < 0)
            {
                _direction = -1;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            else
            {
                _direction = 1;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    [SerializeField] private int _directionInit;

    private Rigidbody2D _rb;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(1.0f, 1.0f);


    public void KnockBack(int knockBackDirection)
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(_knockBackForce.x * knockBackDirection,
                                 _knockBackForce.y),
                     ForceMode2D.Impulse);
    }

    public void ForceChangeState(StateType newStateType)
    {
        _currentState.ForceStop(); 
        _currentState = _states[newStateType]; 
        _currentState.Execute(); 
        Current = newStateType;
    }

    public void StopMove()
    {
        _move.x = 0.0f;
    }

    public void SetMove(Vector2 move)
    {
        _move = move;
    }

    private void Awake()
    {
        _character = GetComponent<CharacterBase>();
        _rb =  GetComponent<Rigidbody2D>();
        Init();
    }

    private void Init()
    {
        for (StateType stateType = StateType.Idle; stateType < StateType.EOF; stateType++)
        {
            AddState(stateType);
        }
        _currentState = _states[StateType.Idle];
        Current = StateType.Idle;

        IsDirectionChangable = true;
        IsMovable = true;

        RegisterShortCuts();
    }

    private void RegisterShortCuts()
    {
        // down actions
        InputHandler.RegisterKeyDownAction(KeyCode.DownArrow, () => ChangeState(StateType.LadderDown));
        InputHandler.RegisterKeyDownAction(KeyCode.UpArrow, () => ChangeState(StateType.LadderUp));

        // press actions
        InputHandler.RegisterKeyPressAction(KeyCode.LeftAlt, () => ChangeState(StateType.Jump));
        InputHandler.RegisterKeyPressAction(KeyCode.DownArrow, () => ChangeState(StateType.Crouch));
        InputHandler.RegisterKeyPressAction(KeyCode.UpArrow, () => ChangeState(StateType.EdgeGrab));
        InputHandler.RegisterKeyPressAction(KeyCode.A, () => ChangeState(StateType.Attack));
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
                typeof(StateType),
                typeof(StateMachine)
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
        _isStateChanged = false;

        if (IsDirectionChangable)
        {
            if (_h < -0.1f)
                Direction = Constants.DIRECTION_LEFT;
            else if (_h > 0.1f)
                Direction = Constants.DIRECTION_RIGHT;
        }

        if (IsMovable)
        {
            _move.x = _h;

            if (Mathf.Abs(_move.x) > 0.1f)
                ChangeState(StateType.Move);
            else
                ChangeState(StateType.Idle);
        }                

        ChangeState(_currentState.Update());
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
        transform.position += new Vector3(_move.x * _character.MoveSpeed, _move.y, 0.0f) * Time.fixedDeltaTime;
    }

    public bool ChangeState(StateType newStateType)
    {
        if (newStateType == StateType.LadderDown)
        {
            Debug.Log("Try to ladder down");
        }

        if (newStateType == StateType.Crouch)
        {
            Debug.Log("Try to crouch");
        }

        // 이미 상태가 해당 프레임에서 한번 바뀌었다면
        if (_isStateChanged)
            return false;

        // 상태가 바뀌지 않았으면
        if (Current == newStateType)
            return false;

        // 바꾸려는 상태가 실행 가능하지 않으면
        if (_states[newStateType].IsExecuteOK == false)
            return false;


        Debug.Log($"State changed {newStateType}");
        _currentState.ForceStop(); // 기존 상태 중단
        _currentState = _states[newStateType]; // 상태 갱신
        _currentState.Execute(); // 갱신된 상태 실행
        Current = newStateType;
        _isStateChanged = true;
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum AIStates
    {
        Think,
        TakeARest,
        MoveLeft,
        MoveRight,
        FollowTarget,
        AttackTarget
    }

    public enum States
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Die,
    }

    private enum IdleStates
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum MoveStates
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum AttackStates
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum HurtStates
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    private enum DieStates
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }

    [Header("States")]
    [SerializeField] private States _state;
    [SerializeField] private IdleStates _idleStates;
    [SerializeField] private MoveStates _moveStates;
    [SerializeField] private AttackStates _attackStates;
    [SerializeField] private HurtStates _hurtStates;
    [SerializeField] private DieStates _dieStates;

    [Header("AI")]
    [SerializeField] private AIStates _aiState;
    [SerializeField] private float _aiTimer;
    [SerializeField] private float _aiThinkTimeMin = 0.1f;
    [SerializeField] private float _aiThinkTimeMax = 3.0f;
    [SerializeField] private bool _aiAutoFollow = false;
    [SerializeField] private bool _aiAttackable = false;
    [SerializeField] private float _aiDetectRange = 2.0f;
    [SerializeField] private float _aiAttackRange = 0.5f;
    [SerializeField] private LayerMask _targetLayer;

    [Header("Movement")]
    [SerializeField] private bool _isMovable;
    [SerializeField] private bool _isDirectionChangable;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _move;
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
                _direction = Constants.DIRECTION_LEFT;
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                _direction = Constants.DIRECTION_RIGHT;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
        }
    }
    [SerializeField] private int _directionInit;

    private AnimationManager _animationManager;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    [SerializeField] private Vector2 _knockBackForce = new Vector2(1.0f, 1.0f);
    public void KnockBack(int knockBackDirection)
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(_knockBackForce.x * knockBackDirection,
                                 _knockBackForce.y),
                     ForceMode2D.Impulse);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _animationManager = GetComponent<AnimationManager>();   
    }

    private void Update()
    {
        UpdateAIState();

        if (_isDirectionChangable)
        {
            if (_move.x < 0.0f)
                Direction = Constants.DIRECTION_LEFT;
            else if (_move.x > 0.0f)
                Direction = Constants.DIRECTION_RIGHT;
        }

        if (_isMovable)
        {
            if (Mathf.Abs(_move.x) > 0.0f)
                ChangeState(States.Move);
            else
                ChangeState(States.Idle);
        }

        UpdateState();
    }

    private void FixedUpdate()
    {
        if (_isMovable == false)
            return;

        _rb.position += new Vector2(_move.x * _moveSpeed, _move.y) * Time.fixedDeltaTime;
    }

    private void UpdateAIState()
    {
        if (_state == States.Hurt ||
            _state == States.Die)
            return;

        if (_aiState < AIStates.FollowTarget)
        {
            if (_aiAutoFollow)
            {
                if (Physics2D.OverlapCircle(transform.position, _aiDetectRange, _targetLayer))
                    _aiState = AIStates.FollowTarget;
            }
            else
            {
                // todo -> check hit by player
            }
        }

        switch (_aiState)
        {
            case AIStates.Think:
                {
                    _aiTimer = Random.Range(_aiThinkTimeMin, _aiThinkTimeMax);
                    _aiState = (AIStates)Random.Range((int)AIStates.TakeARest, (int)AIStates.MoveRight + 1);
                }
                break;
            case AIStates.TakeARest:
                {
                    if (_aiTimer < 0)
                    {
                        _aiState = AIStates.Think;
                    }
                    else
                    {
                        _move.x = 0.0f;
                        _aiTimer -= Time.deltaTime;
                    }
                }
                break;
            case AIStates.MoveLeft:
                {
                    if (_aiTimer < 0)
                    {
                        _aiState = AIStates.Think;
                    }
                    else
                    {
                        _move.x = -1.0f;
                        _aiTimer -= Time.deltaTime;
                    }
                }
                break;
            case AIStates.MoveRight:
                {
                    if (_aiTimer < 0)
                    {
                        _aiState = AIStates.Think;
                    }
                    else
                    {
                        _move.x = 1.0f;
                        _aiTimer -= Time.deltaTime;
                    }
                }
                break;
            case AIStates.FollowTarget:
                {
                    Collider2D target = Physics2D.OverlapCircle(transform.position, _aiDetectRange, _targetLayer);

                    // 타겟이 범위를 벗어났으면
                    if (target == null)
                    {
                        _aiState = AIStates.Think;
                    }
                    // 타겟이 범위 내에 있으면
                    else
                    {
                        // 타겟이 오른쪽에 있으면 
                        if (target.transform.position.x > transform.position.x + _col.size.x)
                        {
                            _move.x = 1.0f;
                        }
                        else if (target.transform.position.x < transform.position.x - _col.size.x)
                        {
                            _move.x = -1.0f;
                        }
                        // 타겟이 공격 반경 내에 들어와있고 공격 가능 하다면
                        if (_aiAttackable &&
                            Vector2.Distance(target.transform.position, transform.position) < _aiAttackRange)
                        {
                            ChangeState(States.Attack);
                            _aiState = AIStates.AttackTarget;
                        }

                    }
                }
                break;
            case AIStates.AttackTarget:
                {
                    if (_state != States.Attack)
                        _aiState = AIStates.FollowTarget;
                }
                break;
            default:
                break;
        }
               
    }


    public void ChangeState(States newState)
    {
        if (_state == newState)
            return;

        // reset old state
        switch (_state)
        {
            case States.Idle:
                _idleStates = IdleStates.Idle;
                break;
            case States.Move:
                _moveStates = MoveStates.Idle;
                break;
            case States.Attack:
                _attackStates = AttackStates.Idle;
                break;
            case States.Hurt:
                _hurtStates = HurtStates.Idle;
                break;
            case States.Die:
                _dieStates = DieStates.Idle;
                break;
            default:
                break;
        }

        // set new state
        switch (newState)
        {
            case States.Idle:
                _idleStates = IdleStates.Prepare;
                break;
            case States.Move:
                _moveStates = MoveStates.Prepare;
                break;
            case States.Attack:
                _attackStates = AttackStates.Prepare;
                break;
            case States.Hurt:
                _hurtStates = HurtStates.Prepare;
                break;
            case States.Die:
                _dieStates = DieStates.Prepare;
                break;
            default:
                break;
        }

        _state = newState;

        UpdateState();
    }

    private void UpdateState()
    {
        switch (_state)
        {
            case States.Idle:
                UpdateIdleState();
                break;
            case States.Move:
                UpdateMoveState();
                break;
            case States.Attack:
                UpdateAttackState();
                break;
            case States.Hurt:
                UpdateHurtState();
                break;
            case States.Die:
                UpdateDieState();
                break;
            default:
                break;
        }
    }

    private void UpdateIdleState()
    {
        switch (_idleStates)
        {
            case IdleStates.Idle:
                break;
            case IdleStates.Prepare:
                {
                    _isMovable = true;
                    _isDirectionChangable = true;
                    _animationManager.Play("Idle");
                    _idleStates = IdleStates.OnAction;
                }
                break;
            case IdleStates.Casting:
                break;
            case IdleStates.OnAction:
                break;
            case IdleStates.Finish:
                break;
            default:
                break;
        }
    }
    private void UpdateMoveState()
    {
        switch (_moveStates)
        {
            case MoveStates.Idle:
                break;
            case MoveStates.Prepare:
                {
                    _isMovable = true;
                    _isDirectionChangable = true;
                    _animationManager.Play("Move");
                    _moveStates = MoveStates.OnAction;
                }
                break;
            case MoveStates.Casting:
                break;
            case MoveStates.OnAction:
                break;
            case MoveStates.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateAttackState()
    {
        switch (_attackStates)
        {
            case AttackStates.Idle:
                break;
            case AttackStates.Prepare:
                {
                    _isMovable = false;
                    _isDirectionChangable = false;
                    _animationManager.Play("Attack");
                    _attackStates = AttackStates.Casting;
                }
                break;
            case AttackStates.Casting:
                {
                    if (_animationManager.IsCastingFinished)
                    {
                        _attackStates = AttackStates.OnAction;
                    }
                }
                break;
            case AttackStates.OnAction:
                {
                    if (_animationManager.GetNormalizedTime() >= 1.0f)
                    {
                        ChangeState(States.Idle);
                    }
                }
                break;
            case AttackStates.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateHurtState()
    {
        switch (_hurtStates)
        {
            case HurtStates.Idle:
                break;
            case HurtStates.Prepare:
                {
                    _isMovable = false;
                    _isDirectionChangable = false;
                    _move.x = 0.0f;
                    _animationManager.Play("Hurt");
                    _hurtStates = HurtStates.OnAction;
                }
                break;
            case HurtStates.Casting:
                break;
            case HurtStates.OnAction:
                {
                    if (_animationManager.GetNormalizedTime() >= 1.0f)
                    {
                        ChangeState(States.Idle);
                    }
                }
                break;
            case HurtStates.Finish:
                break;
            default:
                break;
        }
    }

    private void UpdateDieState()
    {
        switch (_dieStates)
        {
            case DieStates.Idle:
                break;
            case DieStates.Prepare:
                {
                    _isMovable = false;
                    _isDirectionChangable = false;
                    _move.x = 0.0f;
                    _rb.velocity = Vector2.zero;
                    _animationManager.Play("Die");
                    _dieStates = DieStates.OnAction;
                }
                break;
            case DieStates.Casting:
                break;
            case DieStates.OnAction:
                {
                    if (_animationManager.GetNormalizedTime() >= 1.0f)
                        Destroy(gameObject);
                }
                break;
            case DieStates.Finish:
                break;
            default:
                break;
        }
    }
}


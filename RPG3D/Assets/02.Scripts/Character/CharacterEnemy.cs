using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class CharacterEnemy : CharacterBase
{
    [SerializeField] private float _detectRange = 5.0f;
    [SerializeField] private float _attackRange = 1.0f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _aiBehaviorTimeMin = 0.5f;
    [SerializeField] private float _aiBehaviorTimeMax = 3.0f;
    private Transform _target;

    private BehaviorTree _bt;
    private bool _btEnabled = true;

    protected override CharacterStateMachine InitMachine()
    {
        return new EnemyStateMachine(gameObject);
    }

    protected override void UpdateMachine()
    {
        machine.Update();
    }

    protected override void Awake()
    {
        base.Awake();
        BuildBehaviorTree();
        OnHPMin += () =>
        {
            _btEnabled = false;
            machine.ChangeState(CharacterStateMachine.StateType.Die);
        };
    }

    protected override void Update()
    {
        base.Update();

        if (_btEnabled)
            _bt.Tick();
    }

    private void BuildBehaviorTree()
    {
        EnemyMovement movement = GetComponent<EnemyMovement>();

        _bt = new BehaviorTree();

        _bt.StartBuild()
            .Selector()
                .Selector()
                      // in attack range?
                      .Condition(() =>
                      {
                          Collider[] cols = Physics.OverlapSphere(transform.position, _attackRange, _targetLayer);
                          if (cols.Length > 0)
                          {
                              _target = cols[0].transform;
                              return true;
                          }
                          else
                          {
                              _target = null;
                              return false;
                          }
                      })
                          // do attack
                          .Execution(() =>
                          {
                              machine.ChangeState(CharacterStateMachine.StateType.Attack);
                              return machine.currentType == CharacterStateMachine.StateType.Attack ? Status.Success : Status.Failure;
                          })
                      // in detect range?
                      .Condition(() =>
                      {
                          Collider[] cols = Physics.OverlapSphere(transform.position, _detectRange, _targetLayer);
                          if (cols.Length > 0)
                          {
                              _target = cols[0].transform;
                              return true;
                          }
                          else
                          {
                              _target = null;
                              return false;
                          }
                      })
                          // do follow
                          .Execution(() =>
                          {
                              transform.LookAt(_target);
                              machine.ChangeState(CharacterStateMachine.StateType.Move);
                              return machine.currentType == CharacterStateMachine.StateType.Move ? Status.Success : Status.Failure;
                          })
                .ExitCurrentComposite()
                .RandomSelector()
                    .RandomSleep(_aiBehaviorTimeMin, _aiBehaviorTimeMax)
                        .Execution(() =>
                        {
                            transform.eulerAngles = Vector3.up * Random.Range(0.0f, 360.0f);
                            movement.StopMove();
                            machine.ChangeState(CharacterStateMachine.StateType.Move);
                            return machine.currentType == CharacterStateMachine.StateType.Move ? Status.Success : Status.Failure;
                        })
                    .RandomSleep(_aiBehaviorTimeMin, _aiBehaviorTimeMax)
                        .Execution(() =>
                        {
                            transform.eulerAngles = Vector3.up * Random.Range(0.0f, 360.0f);
                            movement.DoMove();
                            machine.ChangeState(CharacterStateMachine.StateType.Move);
                            return machine.currentType == CharacterStateMachine.StateType.Move ? Status.Success : Status.Failure;
                        });



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}

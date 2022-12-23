using System;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            _hp = value;
            _hpBar.value = (float)_hp / _hpMax;
        }
    }
    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _hpMax;
    [SerializeField] private int _damage = 5;
    private EnemyController _controller;
    private CapsuleCollider2D _col;
    [SerializeField] private LayerMask _targetLayer;
    public void Hurt(int damage)
    {
        Hp -= damage;

        DamagePopUp.Create(transform.position + Vector3.up * _col.size.y / 2.0f,
                           damage,
                           1 << gameObject.layer);

        if (_hp > 0)
            _controller.ChangeState(EnemyController.States.Hurt);
        else
            _controller.ChangeState(EnemyController.States.Die);
    }

    private void Awake()
    {
        Hp = _hpMax;
        _controller = GetComponent<EnemyController>();
        _col = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1<<collision.gameObject.layer & _targetLayer) > 0)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.Hurt(_damage);
                player.GetComponent<StateMachine>().KnockBack(_controller.Direction);
            }
        }
    }
}

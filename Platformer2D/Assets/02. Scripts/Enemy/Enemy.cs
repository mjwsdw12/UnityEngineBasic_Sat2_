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
    private EnemyController _controller;
    public void Hurt(int damage)
    {
        Hp -= damage;

        if (_hp > 0)
            _controller.ChangeState(EnemyController.States.Hurt);
        else
            _controller.ChangeState(EnemyController.States.Die);
    }

    private void Awake()
    {
        Hp = _hpMax;
        _controller = GetComponent<EnemyController>();
    }
}

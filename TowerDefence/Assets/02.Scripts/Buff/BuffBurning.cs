using UnityEngine;
public class BuffBurning<T> : IBuff<T>
{
    private int _damage;
    private float _period;
    private float _timer;

    public BuffBurning(int damage, float period)
    {
        _damage = damage;
        _period = period;
    }

    public void OnActive(T target)
    {
    }

    public void OnDeactive(T target)
    {
    }

    public void OnDuration(T target)
    {
        if (_timer <= 0)
        {
            if (target is IHp)
            {
                ((IHp)target).Hurt(_damage);
                _timer = _period;
                UnityEngine.Debug.Log($"{target} is burning... current hp : {((IHp)target).Hp}");
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}

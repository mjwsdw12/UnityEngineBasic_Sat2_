
using UnityEngine;

public class BuffSlowingDown<T> : IBuff<T>
{
    private float _gain;

    public BuffSlowingDown(float gain)
    {
        _gain = gain;
    }

    public void OnActive(T target)
    {
        if (target is ISpeed)
        {
            ((ISpeed)target).Speed /= _gain;
        }
    }

    public void OnDeactive(T target)
    {
        if (target is ISpeed)
        {
            ((ISpeed)target).Speed *= _gain;
        }
    }

    public void OnDuration(T target)
    {
    }
}

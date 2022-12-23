using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BuffManager<T> where T : MonoBehaviour
{
    private T _subject;
    private Dictionary<int, Coroutine> _coroutines;
    private int _capacity = 100;
    private Queue<int> _idQueue;

    public BuffManager(T subject)
    {
        _subject = subject;
        _coroutines = new Dictionary<int, Coroutine>();
        _idQueue = new Queue<int>(Enumerable.Range(1, _capacity));
    }

    public void ActiveBuff(IBuff<T> buff, float duration)
    {
        if (_subject.gameObject.activeSelf == false)
            return;

        if (_idQueue.Count <= 0)
            return;

        int id = _idQueue.Dequeue();
        _coroutines.Add(id, _subject.StartCoroutine(E_ActiveBuff(id, buff, duration)));
    }

    public void ActiveBuff(IBuff<T> buff, Func<bool> condition)
    {
        if (_subject.gameObject.activeSelf == false)
            return;

        if (_idQueue.Count <= 0)
            return;

        int id = _idQueue.Dequeue();
        _coroutines.Add(id, _subject.StartCoroutine(E_ActiveBuff(id, buff, condition)));
    }

    IEnumerator E_ActiveBuff(int id, IBuff<T> buff, float duration)
    {
        buff.OnActive(_subject);

        while (duration > 0)
        {
            buff.OnDuration(_subject);
            duration -= Time.deltaTime;
            yield return null;
        }

        buff.OnDeactive(_subject);

        _coroutines.Remove(id);
        _idQueue.Enqueue(id);
    }

    IEnumerator E_ActiveBuff(int id, IBuff<T> buff, Func<bool> condition)
    {
        buff.OnActive(_subject);

        while (condition.Invoke())
        {
            buff.OnDuration(_subject);
            yield return null;
        }

        buff.OnDeactive(_subject);

        _coroutines.Remove(id);
        _idQueue.Enqueue(id);
    }
}

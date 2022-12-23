using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ObjectPool>("ObjectPool"));
            return _instance;
        }
    }

    private List<ObjectPoolElement> _elements = new List<ObjectPoolElement>();
    public Dictionary<string, Queue<GameObject>> _spawnQueueDictionary = new Dictionary<string, Queue<GameObject>>();


    //==============================================================================
    //***************************** Public Methods *********************************
    //==============================================================================

    /// <summary>
    /// Pool 에 필요한 객체 발주 넣기
    /// </summary>
    public void AddElement(ObjectPoolElement element) => _elements.Add(element);

    /// <summary>
    /// 발주받은 모든 객체를 생성
    /// </summary>
    public void InstantiateAllElements()
    {
        foreach (ObjectPoolElement element in _elements)
        {
            // 대기열 생성
            if (_spawnQueueDictionary.ContainsKey(element.Name) == false)
                _spawnQueueDictionary.Add(element.Name, new Queue<GameObject>());

            for (int i = 0; i < element.Num; i++)
            {
                InstantiateElement(element);
            }
        }
    }

    /// <summary>
    /// 미리 생성해놨던 대기열에서 하나 빼서 소환해줌
    /// </summary>
    public GameObject Spawn(string name, Vector3 spawnPos)
    {
        if (_spawnQueueDictionary.ContainsKey(name) == false)
            return null;

        if (_spawnQueueDictionary[name].Count <= 0)
        {
            ObjectPoolElement element = _elements.Find(e => e.Name == name);

            for (int i = 0; i < Math.Ceiling(Math.Log10(element.Num)); i++)
            {
                InstantiateElement(element);
            }
        }

        GameObject go = _spawnQueueDictionary[name].Dequeue();
        go.transform.SetParent(null);
        go.transform.position = spawnPos;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(true);
        return go;
    }

    public GameObject Spawn(string name, Vector3 spawnPos, Quaternion quaternion)
    {
        if (_spawnQueueDictionary.ContainsKey(name) == false)
            return null;

        if (_spawnQueueDictionary[name].Count <= 0)
        {
            ObjectPoolElement element = _elements.Find(e => e.Name == name);

            for (int i = 0; i < Math.Ceiling(Math.Log10(element.Num)); i++)
            {
                InstantiateElement(element);
            }
        }

        GameObject go = _spawnQueueDictionary[name].Dequeue();
        go.transform.SetParent(null);
        go.transform.position = spawnPos;
        go.transform.rotation = quaternion;
        go.SetActive(true);
        return go;
    }

    /// <summary>
    /// 빌려간거 반납할때 호출하는 함수
    /// </summary>
    public void Return(GameObject go)
    {
        if (_spawnQueueDictionary.ContainsKey(go.name) == false)
        {
            Debug.LogError($"[ObjectPool] : {go.name} 는 왜 가져왔니? 난 빌려준적 없는데..");
            return;
        }

        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        _spawnQueueDictionary[go.name].Enqueue(go);
        go.SetActive(false);
        RearrangeSiblings(go);
    }

    public void Return(GameObject go, float delay)
    {
        StartCoroutine(E_Return(go, delay));
    }

    //==============================================================================
    //***************************** Private Methods ********************************
    //==============================================================================

    private GameObject InstantiateElement(ObjectPoolElement element)
    {
        GameObject go = Instantiate(element.Prefab, transform);
        go.SetActive(false);
        go.name = element.Name;
        _spawnQueueDictionary[element.Name].Enqueue(go);
        RearrangeSiblings(go);
        return go;
    }

    private void RearrangeSiblings(GameObject go)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == go.name)
            {
                go.transform.SetSiblingIndex(i);
                return;
            }
        }
        go.transform.SetAsLastSibling();
    }

    private IEnumerator E_Return(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(go);
    }
}

[Serializable]
public struct ObjectPoolElement
{
    public string Name;
    public GameObject Prefab;
    public int Num;
}
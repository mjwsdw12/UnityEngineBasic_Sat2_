using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolRegister : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolElement> elements;

    private void Awake()
    {
        foreach (ObjectPoolElement element in elements)
        {
            ObjectPool.Instance.AddElement(element);
        }
    }
}

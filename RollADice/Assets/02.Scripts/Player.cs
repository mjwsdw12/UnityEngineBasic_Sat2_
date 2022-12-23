using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public void MoveTo(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    private void Awake()
    {
        Instance = this;
    }
}

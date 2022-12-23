using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    private float _h => Input.GetAxisRaw("Horizontal");
    private float _v => Input.GetAxisRaw("Vertical");

    private void FixedUpdate()
    {
        Vector3 directionVector = new Vector3(_h, 0, _v).normalized;    
        Vector3 deltaMove = directionVector * _moveSpeed * Time.fixedDeltaTime;
        transform.Translate(deltaMove);
    }
}

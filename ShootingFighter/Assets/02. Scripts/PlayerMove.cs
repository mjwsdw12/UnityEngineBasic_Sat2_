using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    private float _h => Input.GetAxisRaw("Horizontal");

    private float _y => Input.GetAxisRaw("Vertical");
    private void FixedUpdate()
    {
        Vector3 directionVector = new Vector3(_h, 0.0f, _y).normalized;
        Vector3 deltaMove = directionVector * _moveSpeed * Time.deltaTime;
        transform.Translate(deltaMove);
    }
}

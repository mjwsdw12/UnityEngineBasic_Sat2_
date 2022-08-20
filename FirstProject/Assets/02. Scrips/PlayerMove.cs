using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 800.0f;
    // Start is called before the first frame update

    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");

    private float r => Input.GetAxis("Mouse X");
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(h, 0.0f, v).normalized;
        Vector3 deltaMove = dir * moveSpeed * Time.deltaTime;
        // transform.position += deltaMove;
        transform.Translate(deltaMove);

        Vector3 deltaRotate = Vector3.up * r * rotateSpeed * Time.deltaTime;
        transform.Rotate(deltaRotate);
    }
}

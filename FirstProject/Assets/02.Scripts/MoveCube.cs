using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public Transform cubeTransform;
    public float moveSpeed = 3.0f;
    // Start is called before the first frame update

    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");

    void Start()
    {
        cubeTransform = gameObject.GetComponent<Transform>();
        cubeTransform = GetComponent<Transform>();
        cubeTransform = this.gameObject.GetComponent<Transform>();

        
        cubeTransform.position = Vector3.up * 2.0f;
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //cubeTransform.position += Vector3.up * Time.deltaTime;
        //transform.position += Vector3.up * Time.deltaTime;

        Vector3 dir = new Vector3(h, 0.0f, v).normalized;
        Vector3 deltaMove = dir * moveSpeed * Time.deltaTime;
        transform.position += deltaMove;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minDistance = 3.0f; // 카메라와 캐릭터간 최소거리
    [SerializeField] private float _maxDistance = 30.0f;
    [SerializeField] private float _wheelSpeed = 500.0f; // 마우스 스크롤 줌 속도
    [SerializeField] private float _xMoveSpeed = 500.0f; // 마우스 x 축 속도값
    [SerializeField] private float _yMoveSpeed = 250.0f;
    [SerializeField] private float _yLimitMin = 5.0f; // 마우스 y 축 제한 ( 월드에서 x 축으로 회전하는데는 제한이 필요하므로)
    [SerializeField] private float _yLimitMax = 80.0f;
    private float _y, _x, _distance; // 마우스 y축, x축, 타겟과의 거리

    private void Awake()
    {
        _distance = Vector3.Distance(transform.position, _target.position);
        _y = transform.eulerAngles.x;
        _x = transform.eulerAngles.y;
        CursorManager.instance.DeactiveCursor();
    }

    private void Update()
    {
        _x += Input.GetAxis("Mouse X") * _xMoveSpeed * Time.deltaTime;
        _y -= Input.GetAxis("Mouse Y") * _yMoveSpeed * Time.deltaTime;
        _x = ClampAngle(_x, -360.0f, 360.0f);
        _y = ClampAngle(_y, _yLimitMin, _yLimitMax);

        _distance -= Input.GetAxis("Mouse ScrollWheel") * _wheelSpeed * Time.deltaTime;
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);

        _target.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(_y, _x, 0);
        transform.position = _target.position - transform.rotation * Vector3.forward * _distance;
    }


    private float ClampAngle(float angle, float min, float max)
    {
        angle %= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }

}

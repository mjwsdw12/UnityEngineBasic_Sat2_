using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float _smoothness;
    private Transform _tr;
    private Camera _camera;

    [SerializeField] private BoxCollider2D _boundShape;
    private float _boundShapeXMin;
    private float _boundShapeXMax;
    private float _boundShapeYMin;
    private float _boundShapeYMax;

    private Transform _target;

    private void Awake()
    {
        _tr = GetComponent<Transform>();        
        _camera = Camera.main;

        _boundShapeXMin = _boundShape.transform.position.x + _boundShape.offset.x - _boundShape.size.x / 2.0f;
        _boundShapeXMax = _boundShape.transform.position.x + _boundShape.offset.x + _boundShape.size.x / 2.0f;
        _boundShapeYMin = _boundShape.transform.position.y + _boundShape.offset.y - _boundShape.size.y / 2.0f;
        _boundShapeYMax = _boundShape.transform.position.y + _boundShape.offset.y + _boundShape.size.y / 2.0f;
    }
    private void Start()
    {
        _target = Player.Instance.transform;
    }
    private void LateUpdate()
    {
        if (_target != null)
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPos = new Vector3(_target.position.x, _target.position.y, _tr.position.z) + _offset;
        Vector3 smoothPos = Vector3.Lerp(_tr.position, targetPos, _smoothness * Time.deltaTime);

        Vector3 camWorldPosLeftBottom = _camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, _camera.nearClipPlane));
        Vector3 camWorldPosRightTop = _camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, _camera.nearClipPlane));
        Vector3 camWorldPosSize = new Vector3(camWorldPosRightTop.x - camWorldPosLeftBottom.x,
                                              camWorldPosRightTop.y - camWorldPosLeftBottom.y,
                                              0.0f);

        // X min bound
        if (smoothPos.x < _boundShapeXMin + camWorldPosSize.x / 2.0f)
            smoothPos.x = _boundShapeXMin + camWorldPosSize.x / 2.0f;
        // X max bound
        else if (smoothPos.x > _boundShapeXMax - camWorldPosSize.x / 2.0f)
                 smoothPos.x = _boundShapeXMax - camWorldPosSize.x / 2.0f;

        // Y min bound
        if (smoothPos.y < _boundShapeYMin + camWorldPosSize.y / 2.0f)
            smoothPos.y = _boundShapeYMin + camWorldPosSize.y / 2.0f;
        // Y max bound
        else if (smoothPos.y > _boundShapeYMax - camWorldPosSize.y / 2.0f)
                 smoothPos.y = _boundShapeYMax - camWorldPosSize.y / 2.0f;

        _tr.position = smoothPos;
    }
}

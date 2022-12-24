using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    public static TowerHandler Instance;
    private GameObject _previewTower;
    private TowerInfo _info;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _nodeLayer;

    public void Handle(TowerInfo info)
    {
        if (_previewTower != null)
            Destroy(_previewTower);

        if (TowerAssets.Instance.TryGetPreviewTower(info, out GameObject prefab))
        {
            _info = info;
            _previewTower = Instantiate(prefab);
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"[TowerHandler] : {info.name} �� �̸����� Ÿ���� ������ �� �����ϴ�. Ÿ�� �������� �̸��� Ȯ���ϼ���");
        }
    }

    public void Cancel()
    {
        if (_previewTower != null)
            Destroy(_previewTower);

        gameObject.SetActive(false);
        _info = null;
    }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();

        if (Input.GetMouseButtonUp(0))
            OnClick();
    }

    private void FixedUpdate()
    {
        if (_previewTower == null)
            return;

        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _nodeLayer))
        {
            _previewTower.transform.position = _hit.collider.transform.position;
            _previewTower.SetActive(true);
        }
        else
        {
            _previewTower.SetActive(false);
        }
    }

    private void OnClick()
    {
        if (_info.BuildPrice > Player.Instance.Money)
        {
            Debug.Log("�ܾ��� �����մϴ�");
            return;
        }

        if (_hit.collider == null)
        {
            Debug.Log("�Ǽ��� ��ġ�� �ùٸ��� �ʽ��ϴ�");
            return;
        }

        if (_hit.collider.GetComponent<Node>().TryBuildTowerHere(_info, out Tower towerBuilt))
        {
            Debug.Log($"{_info.name} Ÿ�� �Ǽ� �Ϸ� ");
            Player.Instance.Money -= _info.BuildPrice;
        }
    }
}

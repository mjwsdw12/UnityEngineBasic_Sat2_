using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool IsTowerExist => TowerBuilt;
    public Tower TowerBuilt;
    private Renderer _renderer;
    private Material _matOrigin;
    [SerializeField] private Material _buildAvailableMat;
    [SerializeField] private Material _buildNotAvailableMat;

    public bool TryBuildTowerHere(TowerInfo info, out Tower towerBuilt)
    {
        towerBuilt = null;

        if (IsTowerExist)
        {
            Debug.Log("해당 위치에는 타워가 이미 존재하므로 건설할 수 없습니다.");
            return false;
        }

        if (TowerAssets.Instance.TryGetTower(info, out Tower towerPrefab))
        {
            TowerBuilt = Instantiate(towerPrefab,
                                      transform.position,
                                      Quaternion.identity);
            TowerBuilt.Node = this;
            return true;
        }

        return false;
    }

    public Tower BuildTowerHere(Tower prefab)
    {
        if (IsTowerExist)
            Destroy(TowerBuilt.gameObject);

        TowerBuilt = Instantiate(prefab,
                                 transform.position,
                                 Quaternion.identity);
        TowerBuilt.Node = this;
        return TowerBuilt;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _matOrigin = _renderer.sharedMaterial;
    }

    private void OnMouseEnter()
    {
        if (IsTowerExist)
            _renderer.material = _buildNotAvailableMat;
        else
            _renderer.material = _buildAvailableMat;
    }

    private void OnMouseExit()
    {
        _renderer.material = _matOrigin;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerUI : MonoBehaviour
{
    public static TowerUI Instance;

    [SerializeField] private Button _upgrade;
    [SerializeField] private Button _sell;
    [SerializeField] private TMP_Text _upgradePrice;
    [SerializeField] private TMP_Text _sellPrice;
    [SerializeField] private Vector3 _offset = Vector3.up;
    private int _nextLevelTowerBuildPrice;

    public void SetUp(Tower tower)
    {
        // upgrade
        if (TowerAssets.Instance.TryGetNextLevelTower(tower.Info, out Tower nextLevelTowerPrefab))
        {
            _upgrade.gameObject.SetActive(true);
            _nextLevelTowerBuildPrice = nextLevelTowerPrefab.Info.BuildPrice;
            _upgradePrice.text = _nextLevelTowerBuildPrice.ToString();

            RefreshUpgradePriceColor();

            _upgrade.onClick.RemoveAllListeners();
            _upgrade.onClick.AddListener(() =>
            {
                Player.Instance.Money -= _nextLevelTowerBuildPrice;
                SetUp(tower.Node.BuildTowerHere(nextLevelTowerPrefab));
            });
        }
        else
        {
            _upgrade.gameObject.SetActive(false);
        }

        // sell
        _sellPrice.text = tower.Info.SellPrice.ToString();
        _sell.onClick.RemoveAllListeners();
        _sell.onClick.AddListener(() =>
        {
            Player.Instance.Money += tower.Info.SellPrice;
            Destroy(tower.gameObject);
            gameObject.SetActive(false);
        });

        // position
        transform.position = tower.transform.position + _offset;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Player.Instance.OnMoneyChanged += RefreshUpgradePriceColor;
        gameObject.SetActive(false);
    }

    private void RefreshUpgradePriceColor(int value)
    {
        if (_nextLevelTowerBuildPrice > value)
            _upgradePrice.color = Color.red;
        else
            _upgradePrice.color = Color.black;
    }

    private void RefreshUpgradePriceColor()
    {
        if (_nextLevelTowerBuildPrice > Player.Instance.Money)
            _upgradePrice.color = Color.red;
        else
            _upgradePrice.color = Color.black;
    }
}

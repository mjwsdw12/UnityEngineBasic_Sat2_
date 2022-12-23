using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpAssets : MonoBehaviour
{
    private static DamagePopUpAssets _instance;
    public static DamagePopUpAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate<DamagePopUpAssets>(Resources.Load<DamagePopUpAssets>("DamagePopUpAssets"));
            return _instance;
        }
    }

    [SerializeField] private List<DamagePopUp> _damagePopUps;
    private Dictionary<LayerMask, DamagePopUp> _damagePopUpDictionary = new Dictionary<LayerMask, DamagePopUp>();

    public DamagePopUp GetDamagePopUp(LayerMask layer) => _damagePopUpDictionary[layer];

    private void Awake()
    {
        foreach (DamagePopUp item in _damagePopUps)
        {
            _damagePopUpDictionary.Add(item.Layer, item);
        }
    }
}

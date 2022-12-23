using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _life;
    [SerializeField] private TMP_Text _money;

    private void Start()
    {
        Player.Instance.OnLifeChanged += (value) => _life.text = value.ToString();
        Player.Instance.OnMoneyChanged += (value) => _money.text = value.ToString();
    }
}

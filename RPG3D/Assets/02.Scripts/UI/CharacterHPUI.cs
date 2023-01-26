using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterHPUI : MonoBehaviour
{
    [SerializeField] CharacterBase _character;

    [SerializeField] private Slider _hpSlider;
    [SerializeField] private TMP_Text _hpText;
    private Transform _cam;

    private void Awake()
    {
        _character.OnHPChanged += (value) =>
        {
            _hpSlider.value = value / _character.hpMax;
            _hpText.text = ((int)value).ToString();
        };
        _cam = Camera.main.transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.position);
    }
}

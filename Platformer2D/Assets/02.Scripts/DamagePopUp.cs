using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{
    public LayerMask Layer;
    private TMP_Text _tmpText;
    private float _fadeTimer = 1.0f;
    private float _fadeSpeed = 1.0f;
    private float _moveSpeedY = 0.8f;
    private Color _color;

    public static DamagePopUp Create(Vector3 pos, int damage, LayerMask targetLayer)
    {
        DamagePopUp damagePopUp = Instantiate(DamagePopUpAssets.Instance.GetDamagePopUp(targetLayer), pos, Quaternion.identity);
        damagePopUp.SetUp(damage);
        return damagePopUp;
    }

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
        _color = _tmpText.color;
    }

    private void Update()
    {
        transform.position += Vector3.up * _moveSpeedY * Time.deltaTime;

        if (_fadeTimer < 0.0f)
        {
            Destroy(gameObject); ;
        }
        else
        {
            _color.a -= _fadeSpeed * Time.deltaTime;
            _tmpText.color = _color;
            _fadeTimer -= Time.deltaTime;
        }
    }

    private void SetUp(int damage)
    {
        _tmpText.text = damage.ToString();
    }
}

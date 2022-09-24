using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopUpText : MonoBehaviour
{
    private TMP_Text _text;
    private Vector3 _popUpPos;
    [SerializeField] private Vector3 _dir = Vector3.up;
    [SerializeField]private float _moveSpeed = 0.5f;
    [SerializeField]private float _fadeSpeed = 0.5f;

    public void PopUp()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void PopUp(string text)
    {
        gameObject.SetActive(false);
        _text.text = text;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _popUpPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = _popUpPos;
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1.0f);
    }

    private void FixedUpdate()
    {
        transform.Translate(_dir * _moveSpeed * Time.fixedDeltaTime);

        float a = (_text.color.a - _fadeSpeed * Time.fixedDeltaTime);

        if (a <= 0.0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
        
        _text.color = new Color(_text.color.r,
                                _text.color.g,
                                _text.color.b,
                                a);
        }



        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceAnimation : MonoBehaviour
{
    public static DiceAnimation Instance;
    public bool isPlaying { get; private set; }

    private Image _image;
    private List<Sprite> _sprites;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _animationDelayTerm;
    private float _timer;
    private void Awake()
    {
        Instance = this;
        _image = GetComponent<Image>();
        LoadSprites();
    }

    private void LoadSprites()
    {
        _sprites = Resources.LoadAll<Sprite>("DiceImages").ToList();
    }

    public bool TryPlayDiceAnimation(int diceValue, Action<int> OnAfterAnimation)
    {
        if (isPlaying)
            return false;

        StartCoroutine(E_DiceAnimation(diceValue, OnAfterAnimation));
        return true;
    }
    IEnumerator E_DiceAnimation(int diceValue, Action<int> OnAfterAnimation)
    {
        isPlaying = true;

        float elapsedTime = 0;
        while (elapsedTime < _animationTime)
        {
            if (_timer < 0)
            {
                _image.sprite = _sprites[Random.Range(0, _sprites.Count)];
                _timer = _animationDelayTerm;
            }
            else
            {
                _timer -= Time.deltaTime;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _image.sprite = _sprites[diceValue - 1];

        OnAfterAnimation(diceValue);
        isPlaying = false;
    }
}

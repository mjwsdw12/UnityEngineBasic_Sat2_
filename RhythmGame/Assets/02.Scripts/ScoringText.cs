using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoringText : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    public static ScoringText Instance;
    private void Awake()
    {
        Instance = this;
        Score = 0;
    }
    #endregion

    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _delta = (int)((_after - _before) / _scoringTime);
            _after = value;
            _score = value;
        }
    }
    [SerializeField] private TMP_Text _scoreText;

    private int _delta;
    private int _before;
    private int _after;
    private float _scoringTime = 0.1f;

    private void Update()
    {
        if (_before < _after)
        {
            _before += (int)(_delta * Time.deltaTime);

            if (_before > _after)
                _before = _after;

            _scoreText.text = _before.ToString();
        }
    }
}

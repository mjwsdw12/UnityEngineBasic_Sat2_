using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoringText : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    public static ScoringText instance;

    private void Awake()
    {
        instance = this;
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

            _delta = (int)((_after - _before) / _scoringtime);
            _after = value;
            _score = value;
        }
    }
    [SerializeField] private TMP_Text _scoreText;

    private int _delta;
    private int _before;
    private int _after;
    private float _scoringtime = 0.1f;

    private void Update()
    {
        if (_before < _after)
        {
            _before += (int)(_delta * Time.deltaTime);

            if (_before >= _after)
                _before = _after;

            _scoreText.text = _before.ToString();
        }
    }
}

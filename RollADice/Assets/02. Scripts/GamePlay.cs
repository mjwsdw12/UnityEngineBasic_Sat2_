using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GamePlay : MonoBehaviour
{
    public static GamePlay Instance;

    private int _starScore;
    public int StarPoint
    {
        get
        {
            return _starScore;
        }
        set
        {
            _starScore = value;
            _starScoreText.text = _starScore.ToString();
        }
    }
    [SerializeField] private TMP_Text _starScoreText;

    private int _nomalDiceNum;

    public int NomalDiceNum
    {
        get
        {
            return _nomalDiceNum;
        }
        set
        {
            _nomalDiceNum = value;
            _normalDiceNumText.text = _nomalDiceNum.ToString();
        }
    }
    [SerializeField] private TMP_Text _normalDiceNumText;

    private int _goldenDiceNum;

    public int GoldenDiceNum
    {
        get
        {
            return _goldenDiceNum;
        }
        set
        {
            _goldenDiceNum = value;
            _goldenDiceNumText.text = _goldenDiceNum.ToString();
        }
    }
    [SerializeField] private TMP_Text _goldenDiceNumText;

    // direction 1 : positive -1 : negative
    private int _direction;

    public int Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value < 0)
            {
                _direction = Constants.DIRECTION_NEGATIVE;
                _inverseIcon.SetActive(true);
            }
            else
            {
                _direction = Constants.DIRECTION_POSITIVE;
                _inverseIcon.SetActive(false);
            }
        }
    }
    [SerializeField] private GameObject _inverseIcon;

    private int _current; // 현재 타일의 id
    private int _tilesCount; // 총 타일 수
    [SerializeField] private List<TileInfo> _tiles;
    private List<TileInfoStar> _starTiles;

    public void RollANormalDice()
    {
        if (NomalDiceNum > 0)
        {
            int diceValue = Random.Range(1, 7);
            if (DiceAnimation.Instance.TryPlayDiceAnimation(diceValue, MovePlayer))
            {
                NomalDiceNum--;
            }
        }
    }

    public void RollAGoldenDice (int diceValue)
    {
        if (GoldenDiceNum > 0)
        {
            if (DiceAnimation.Instance.TryPlayDiceAnimation(diceValue, MovePlayer))
            {
                GoldenDiceNum--;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Direction = Constants.DIRECTION_POSITIVE;
        StarPoint = Constants.STAR_POINT_INIT;
        NomalDiceNum = Constants.NORMAL_DICE_NUM_INIT;
        GoldenDiceNum = Constants.GOLDEN_DICE_NUM_INIT;
        _tilesCount = _tiles.Count;

        // _tiles.Sort((x, y) => x.CompareTo(y));
        _tiles.OrderBy(x => x);
        // if (_tiles[0] > _tiles[1])
        // {
        // }

        _starTiles = new List<TileInfoStar>();
        foreach (TileInfo tileInfo in _tiles)
        {
            // is 연산자
            // 캐스트 후의 결과를 변환하는 연산자
            // 캐스팅 성공하면 true, 실패하면 false 반환
            if (tileInfo is TileInfoStar)
                _starTiles.Add((TileInfoStar)tileInfo);

            // as 연산자
            // 명시적 캐스팅 연산자
            // 캐스팅 실패시 null 반환
            // TileInfoStar tmp = tileInfo as TileInfoStar;
            // if (tmp != null)
            //     _starTiles.Add(tmp);
        }
    }

    private void MovePlayer(int diceValue)
    {


        if(Direction == Constants.DIRECTION_POSITIVE)
        {
            CalcPassedStarTiles(_current, diceValue);

            _current += diceValue;
            if (_current > _tilesCount)
                _current -= _tilesCount;
        }
        else if (Direction == Constants.DIRECTION_NEGATIVE)
        {
            _current -= diceValue;
            if (_current < 0)
                _current += _tilesCount;

            Direction = Constants.DIRECTION_POSITIVE;
        }

        Debug.Log(_current);
        if (_current > 0)
        {
            Player.Instance.MoveTo(_tiles[_current - 1].transform.position);
            _tiles[_current - 1].OnTile();
        }
    }

    private void CalcPassedStarTiles(int previous, int diceValue)
    {
        int tmpSum = 0;
        foreach(TileInfoStar starTile in _starTiles)
        {
            if (previous + diceValue > _tilesCount )
            {
                if (starTile.Id <= previous)
                {
                    if (starTile.Id <= previous + diceValue - _tilesCount)
                    {
                        tmpSum += starTile.starValue;
                    }
                }
                else
                {
                    tmpSum += starTile.starValue;
                }
            }
            else
            {
                if (starTile.Id > previous &&
                starTile.Id < previous + diceValue)
                {
                    tmpSum += starTile.starValue;
                }
            }

        }
        StarPoint += tmpSum;
    }
}

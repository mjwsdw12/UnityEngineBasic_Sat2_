using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    Miss,
    Bad,
    Good,
    Great,
    Cool
}

public class Note : MonoBehaviour
{
    public KeyCode Key;

    public void Hit(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Miss:
                ScoringText.instance.Score += Constants.SCORE_MISS;
                GameStatus.CurrentCombo = 0;
                break;
            case HitType.Bad:
                ScoringText.instance.Score += Constants.SCORE_BAD;
                GameStatus.CurrentCombo = 0;
                break;
            case HitType.Good:
                ScoringText.instance.Score += Constants.SCORE_GOOD;
                GameStatus.CurrentCombo++;
                break;
            case HitType.Great:
                ScoringText.instance.Score += Constants.SCORE_GREAT;
                GameStatus.CurrentCombo++;
                break;
            case HitType.Cool:
                ScoringText.instance.Score += Constants.SCORE_COOL;
                GameStatus.CurrentCombo++;
                break;
            default:
                break;
        }
        PopUpTextManger.Instance.PopUp(hitType);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * NoteManger.NoteSpeedScale * Time.fixedDeltaTime);
    }
}

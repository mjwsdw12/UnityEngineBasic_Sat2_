using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    Bad,
    Miss,
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
            case HitType.Bad:
                ScoringText.Instance.Score += Constants.SCORE_BAD;
                GameStatus.CurrentCombo = 0;
                break;
            case HitType.Miss:
                ScoringText.Instance.Score += Constants.SCORE_MISS;
                GameStatus.CurrentCombo = 0;
                break;
            case HitType.Good:
                ScoringText.Instance.Score += Constants.SCORE_GOOD;
                GameStatus.CurrentCombo++;
                break;
            case HitType.Great:
                ScoringText.Instance.Score += Constants.SCORE_GREAT;
                GameStatus.CurrentCombo++;
                break;
            case HitType.Cool:
                ScoringText.Instance.Score += Constants.SCORE_COOL;
                GameStatus.CurrentCombo++;
                break;
            default:
                break;
        }
        PopUpTextManager.Instance.PopUp(hitType);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * NoteManager.NoteSpeedScale * Time.fixedDeltaTime);
    }
}

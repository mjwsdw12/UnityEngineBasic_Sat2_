using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextManager : MonoBehaviour
{
    public static PopUpTextManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private PopUpText _popUptextBad;
    [SerializeField] private PopUpText _popUptextMiss;
    [SerializeField] private PopUpText _popUptextGood;
    [SerializeField] private PopUpText _popUptextGreat;
    [SerializeField] private PopUpText _popUptextCool;
    [SerializeField] private PopUpText _comboText;
    public void PopUp(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Bad:
                _popUptextBad.PopUp();
                _popUptextBad.transform.Translate(Vector3.back);
                _popUptextMiss.transform.Translate(Vector3.forward);
                _popUptextGood.transform.Translate(Vector3.forward);
                _popUptextGreat.transform.Translate(Vector3.forward);
                _popUptextCool.transform.Translate(Vector3.forward);
                break;
            case HitType.Miss:
                _popUptextMiss.PopUp();
                _popUptextBad.transform.Translate(Vector3.forward);
                _popUptextMiss.transform.Translate(Vector3.back);
                _popUptextGood.transform.Translate(Vector3.forward);
                _popUptextGreat.transform.Translate(Vector3.forward);
                _popUptextCool.transform.Translate(Vector3.forward);
                break;
            case HitType.Good:
                _popUptextGood.PopUp();
                _popUptextBad.transform.Translate(Vector3.forward);
                _popUptextMiss.transform.Translate(Vector3.forward);
                _popUptextGood.transform.Translate(Vector3.back);
                _popUptextGreat.transform.Translate(Vector3.forward);
                _popUptextCool.transform.Translate(Vector3.forward);
                break;
            case HitType.Great:
                _popUptextGreat.PopUp();
                _popUptextBad.transform.Translate(Vector3.forward);
                _popUptextMiss.transform.Translate(Vector3.forward);
                _popUptextGood.transform.Translate(Vector3.forward);
                _popUptextGreat.transform.Translate(Vector3.back);
                _popUptextCool.transform.Translate(Vector3.forward);
                break;
            case HitType.Cool:
                _popUptextCool.PopUp();
                _popUptextBad.transform.Translate(Vector3.forward);
                _popUptextMiss.transform.Translate(Vector3.forward);
                _popUptextGood.transform.Translate(Vector3.forward);
                _popUptextGreat.transform.Translate(Vector3.forward);
                _popUptextCool.transform.Translate(Vector3.back);
                break;
            default:
                break;
        }

        if (GameStatus.CurrentCombo > 1)
            _comboText.PopUp(GameStatus.CurrentCombo.ToString());
    }
}

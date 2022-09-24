using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextManger : MonoBehaviour
{
    public static PopUpTextManger Instance;

    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] private PopUpText _popUptext_Miss;
    [SerializeField] private PopUpText _popUptext_Bad;
    [SerializeField] private PopUpText _popUptext_Good;
    [SerializeField] private PopUpText _popUptext_Great;
    [SerializeField] private PopUpText _popUptext_Cool;
    [SerializeField] private PopUpText _comboText;
    public void PopUp(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Miss:
                _popUptext_Miss.PopUp();
                _popUptext_Miss.transform.Translate(Vector3.back);
                _popUptext_Bad.transform.Translate(Vector3.forward);
                _popUptext_Good.transform.Translate(Vector3.forward);
                _popUptext_Great.transform.Translate(Vector3.forward);
                _popUptext_Cool.transform.Translate(Vector3.forward);
                break;
            case HitType.Bad:
                _popUptext_Bad.PopUp();
                _popUptext_Miss.transform.Translate(Vector3.forward);
                _popUptext_Bad.transform.Translate(Vector3.back);
                _popUptext_Good.transform.Translate(Vector3.forward);
                _popUptext_Great.transform.Translate(Vector3.forward);
                _popUptext_Cool.transform.Translate(Vector3.forward);
                break;
            case HitType.Good:
                _popUptext_Good.PopUp();
                _popUptext_Miss.transform.Translate(Vector3.forward);
                _popUptext_Bad.transform.Translate(Vector3.forward);
                _popUptext_Good.transform.Translate(Vector3.back);
                _popUptext_Great.transform.Translate(Vector3.forward);
                _popUptext_Cool.transform.Translate(Vector3.forward);
                break;
            case HitType.Great:
                _popUptext_Great.PopUp();
                _popUptext_Miss.transform.Translate(Vector3.forward);
                _popUptext_Bad.transform.Translate(Vector3.forward);
                _popUptext_Good.transform.Translate(Vector3.back);
                _popUptext_Great.transform.Translate(Vector3.forward);
                _popUptext_Cool.transform.Translate(Vector3.forward);
                break;
            case HitType.Cool:
                _popUptext_Cool.PopUp();
                _popUptext_Miss.transform.Translate(Vector3.forward);
                _popUptext_Bad.transform.Translate(Vector3.forward);
                _popUptext_Good.transform.Translate(Vector3.forward);
                _popUptext_Great.transform.Translate(Vector3.forward);
                _popUptext_Cool.transform.Translate(Vector3.back);
                break;
            default:
                break;
        }

        if (GameStatus.CurrentCombo > 1)
            _comboText.PopUp(GameStatus.CurrentCombo.ToString());
    }
}

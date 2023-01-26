using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterPlayer : CharacterBase
{
    public int STR = 10;

    [SerializeField] private TargetCaster _targetCasterLeftHand;
    [SerializeField] private TargetCaster _targetCasterRightHand;
    private List<GameObject> _targetsCastedBuffer = new List<GameObject>();
    protected override CharacterStateMachine InitMachine()
    {
        return new PlayerStateMachine(gameObject);
    }

    protected override void UpdateMachine()
    {
        machine.Update();
    }

    public void FootL() { }
    public void FootR() { }
    public void Land() { }

    public void ClearTargetCasters()
    {
        _targetCasterLeftHand.Clear();
        _targetCasterRightHand.Clear();
    }

    public IEnumerable<GameObject> GetAllTargetsCasted()
    {
        _targetsCastedBuffer.Clear();
        _targetsCastedBuffer.AddRange(_targetCasterLeftHand.GetTargets());
        _targetsCastedBuffer.AddRange(_targetCasterRightHand.GetTargets());
        return _targetsCastedBuffer;
    }

    public void LeftHandTargetCasterOn(int on) { _targetCasterLeftHand.on = Convert.ToBoolean(on); }
    public void RightHandTargetCasterOn(int on) { _targetCasterRightHand.on = Convert.ToBoolean(on); }

    public void DamageToLeftHandTargetsCasted()
    {
        foreach (IDamageable target in _targetCasterLeftHand.GetTargets().Select(target => target.GetComponent<IDamageable>()))
        {
            target.Damage(STR);
        }
    }

    public void DamageToRightHandTargetsCasted()
    {
        foreach (IDamageable target in _targetCasterRightHand.GetTargets().Select(target => target.GetComponent<IDamageable>()))
        {
            target.Damage(STR);
        }
    }
}

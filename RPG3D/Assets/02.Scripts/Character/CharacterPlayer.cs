using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : CharacterBase
{
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
}

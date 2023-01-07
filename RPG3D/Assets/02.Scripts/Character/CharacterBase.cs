using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    protected CharacterStateMachine machine;

    protected abstract CharacterStateMachine InitMachine();
    protected abstract void UpdateMachine();

    private void Awake()
    {
        machine = InitMachine();
    }

    private void Update()
    {
        UpdateMachine();
    }
}

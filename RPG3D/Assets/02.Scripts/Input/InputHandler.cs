using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance;
    public static InputHandler main
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<InputHandler>("InputHandler"));
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private bool _mouse0Trigger;
    public bool Mouse0Trigger
    {
        get
        {
            if (_mouse0Trigger)
            {
                _mouse0Trigger = false;
                return true;
            }
            return false;
        }
        set
        {
            _mouse0Trigger = value;
            if (value)
                OnMouse0TriggerActive?.Invoke();
        }
    }
    public event Action OnMouse0TriggerActive;

    private Dictionary<KeyCode, Action> _keyDownActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode, Action> _keyPressActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode, Action> _keyUpActions = new Dictionary<KeyCode, Action>();

    public void RegisterKeyDownAction(KeyCode key, Action action)
    {
        if (_keyDownActions.ContainsKey(key))
            _keyDownActions[key] += action;
        else
            _keyDownActions.Add(key, action);
    }
    public void RegisterKeyPressAction(KeyCode key, Action action)
    {
        if (_keyPressActions.ContainsKey(key))
            _keyPressActions[key] += action;
        else
            _keyPressActions.Add(key, action);
    }
    public void RegisterKeyUpAction(KeyCode key, Action action)
    {
        if (_keyUpActions.ContainsKey(key))
            _keyUpActions[key] += action;
        else
            _keyUpActions.Add(key, action);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Mouse0Trigger = true;
        }

        DoActions(_keyDownActions);
        DoActions(_keyPressActions);
        DoActions(_keyUpActions);
    }

    private void DoActions(Dictionary<KeyCode, Action> actions)
    {
        foreach (var pair in actions)
        {
            if (Input.GetKey(pair.Key))
                pair.Value.Invoke();
        }
    }
}

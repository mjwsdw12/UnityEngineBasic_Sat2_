using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static Dictionary<KeyCode, Func<bool>> _keyDownActions = new Dictionary<KeyCode, Func<bool>>();
    private static Dictionary<KeyCode, Func<bool>> _keyPressActions = new Dictionary<KeyCode, Func<bool>>();

    public static void RegisterKeyDownAction(KeyCode keyCode, Func<bool> action)
    {
        if (_keyDownActions.ContainsKey(keyCode))
        {
            _keyDownActions[keyCode] += action;
        }
        else
        {
            _keyDownActions.Add(keyCode, action);
        }
    }

    public static void RegisterKeyPressAction(KeyCode keyCode, Func<bool> action)
    {
        if (_keyPressActions.ContainsKey(keyCode))
        {
            _keyPressActions[keyCode] += action;
        }
        else
        {
            _keyPressActions.Add(keyCode, action);
        }
    }

    private void Update()
    {
        foreach (KeyValuePair<KeyCode, Func<bool>> pair in _keyDownActions)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                if (pair.Value.Invoke())
                    return;
            }
        }

        foreach (KeyValuePair<KeyCode, Func<bool>> pair in _keyPressActions)
        {
            if (Input.GetKey(pair.Key))
            {
                if (pair.Value.Invoke())
                    return;
            }
        }
    }
}


//public class 철수
//{
//    public int Money
//    {
//        set
//        {
//            // ? : Null 조건 연산자
//            돈으로수행하는작업목록?.Invoke(value);
//        }
//    }
//    public delegate void MoneyHandler(int 돈);
//    public MoneyHandler 돈으로수행하는작업목록;
//
//    public Action<int> 돈으로수행하는액션들;
//    public Func<int, int> 돈으로수행하는함수들;
//}
//
//public class 영희
//{
//    public 철수 철수;
//
//    영희()
//    {
//        철수.돈으로수행하는작업목록 += 사탕사기;
//        철수.돈으로수행하는작업목록(3);
//    }
//    public void 사탕사기(int 금액)
//    {
//        사탕가게.Instance.사탕판매(금액);
//    }
//}
//
//public class 사탕가게
//{
//    public static 사탕가게 Instance;
//    private int 사탕가격 = 3;
//    public bool 사탕판매(int 금액)
//    {
//        if (금액 >= 사탕가격)
//        {
//            return true;
//        }
//        return false;
//    }
//}
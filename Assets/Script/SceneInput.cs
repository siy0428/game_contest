using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneInput : MonoBehaviour
{
    private InputAction enter;
    private int press;
    private int old_press;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput _input = FindObjectOfType<PlayerInput>();
        InputActionMap actionMap = _input.currentActionMap;

        enter = actionMap["Enter"];
        enter.started += IsDown;
        enter.canceled += IsUp;

        press = 0;
        old_press = press;
    }

    // Update is called once per frame
    void Update()
    {
        //最後に入力の更新
        InputUpdate();
    }

    private void InputUpdate()
    {
        //1F前のキー入力状態を参照してキーが離されているかどうかの判定
        if (press != old_press)
        {
            press = 0;  //離されている
        }

        old_press = press;
    }

    private void IsDown(InputAction.CallbackContext obj)
    {
        press = 1;
    }


    private void IsUp(InputAction.CallbackContext obj)
    {
        press = -1;
    }
}

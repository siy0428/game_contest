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
        //�Ō�ɓ��͂̍X�V
        InputUpdate();
    }

    private void InputUpdate()
    {
        //1F�O�̃L�[���͏�Ԃ��Q�Ƃ��ăL�[��������Ă��邩�ǂ����̔���
        if (press != old_press)
        {
            press = 0;  //������Ă���
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

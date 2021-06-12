using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;
    List<Vector3> positions;

    private InputAction left_mouse;
    private InputAction right_mouse;
    private int press;
    private int old_press;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput _input = FindObjectOfType<PlayerInput>();
        InputActionMap actionMap = _input.currentActionMap;

        //テスト用左クリック
        left_mouse = actionMap["LeftMouse"];
        left_mouse.started += IsLeftDown;
        left_mouse.canceled += IsLeftUp;

        //右クリック
        right_mouse = actionMap["RightMouse"];
        right_mouse.started += IsRightDown;
        right_mouse.canceled += IsRightUp;

        press = 0;
        old_press = press;

        positions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (press == 1)
        {
            StartRewind();
        }
        else if (press == -1)
        {
            StopRewind();
        }

        //最後に入力の更新
        InputUpdate();
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        positions.Insert(0, transform.position);
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
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

    private void IsLeftDown(InputAction.CallbackContext obj)
    {
        press = 1;
    }


    private void IsLeftUp(InputAction.CallbackContext obj)
    {
        press = -1;
    }

    private void IsRightDown(InputAction.CallbackContext obj)
    {
        press = 1;
    }

    private void IsRightUp(InputAction.CallbackContext obj)
    {
        press = -1;
    }
}

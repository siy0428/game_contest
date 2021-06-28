using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleSceneManager : MonoBehaviour
{
    public PlayerInput pInput;
    private InputAction Scene;
    private bool enter;

    // Start is called before the first frame update
    void Start()
    {
        //キー取得
        //Input = FindObjectOfType<PlayerInput>();
        InputActionMap ActionMap = pInput.currentActionMap;
        Scene = ActionMap["Scene"];

        Scene.started += InputKey;

        enter = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScene();
    }

    //シーンの切り替え
    void ChangeScene()
    {
        if (enter)
        {
            SceneManager.LoadScene("beta");
        }
    }

    void InputKey(InputAction.CallbackContext obj)
    {
        enter = true;
    }
}

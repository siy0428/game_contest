using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ResultSceneManager : MonoBehaviour
{
    public PlayerInput pInput;
    private InputAction Scene;

    // Start is called before the first frame update
    void Start()
    {
        //キー取得
        //Input = FindObjectOfType<PlayerInput>();
        InputActionMap ActionMap = pInput.currentActionMap;
        Scene = ActionMap["Scene"];

        Scene.started += InputKey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーンの切り替え
    void ChangeScene()
    {
        SceneManager.LoadScene("title");
    }

    void InputKey(InputAction.CallbackContext obj)
    {
        ChangeScene();
    }
}

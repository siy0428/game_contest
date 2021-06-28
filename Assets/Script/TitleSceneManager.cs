using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleSceneManager : MonoBehaviour
{
    public PlayerInput pInput;
    private InputAction Scene;
    public float FadeTime = 0.0f;

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
<<<<<<< HEAD
        //SceneManager.LoadScene("MainScene");
        if (SceneManager.GetActiveScene().name == "title")
            FadeManager.Instance.LoadScene("MainScene", FadeTime);
=======
        SceneManager.LoadScene("beta");
>>>>>>> 7c7833d64c323403aee1c7aa7571e0abd6d0e43e
    }

    void InputKey(InputAction.CallbackContext obj)
    {
        ChangeScene();
    }
}

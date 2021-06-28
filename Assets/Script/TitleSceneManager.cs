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
        //�L�[�擾
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

    //�V�[���̐؂�ւ�
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

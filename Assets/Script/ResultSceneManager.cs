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
        //�L�[�擾
        //Input = FindObjectOfType<PlayerInput>();
        InputActionMap ActionMap = pInput.currentActionMap;
        Scene = ActionMap["Scene"];

        Scene.started += InputKey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�V�[���̐؂�ւ�
    void ChangeScene()
    {
        SceneManager.LoadScene("title");
    }

    void InputKey(InputAction.CallbackContext obj)
    {
        ChangeScene();
    }
}

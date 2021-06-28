using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameSceneManager : MonoBehaviour
{
    //public PlayerInput pInput;
    //private InputAction Scene;
    private PlayerController pc;
    private bool change;

    // Start is called before the first frame update
    void Start()
    {
        ////�L�[�擾
        ////Input = FindObjectOfType<PlayerInput>();
        //InputActionMap ActionMap = pInput.currentActionMap;
        //Scene = ActionMap["Scene"];

        //Scene.started += InputKey;

        pc = FindObjectOfType<PlayerController>();
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�V�[���̑J��
        ChangeScene();
    }

    //�V�[���̐؂�ւ�
    void ChangeScene()
    {
        if(change)
        {
            return;
        }

        //���ݑ��삵�Ă���v���C���[��ID
        int player_id = pc.ControlPlayerID;

        foreach (var player in pc.PlayersData)
        {
            //���삵�Ă���v���C���[���Q�Ƃ��Ă����ꍇ�͎��̃��[�v
            if (player_id == player.PlayerID)
            {
                continue;
            }

            //�����Ă���v���C���[������΃V�[����J�ڂ��Ȃ�
            if (player.IsAlive2)
            {
                //Debug.Log("�����Ă܂�");
                return;
            }
        }

        SceneManager.LoadScene("ResultScene");
        change = true;
        //Debug.Log("�����ȊO�S������ł���̂őJ��");
    }

    //void InputKey(InputAction.CallbackContext obj)
    //{
    //    ChangeScene();
    //}
}

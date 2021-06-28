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
        ////キー取得
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
        //シーンの遷移
        ChangeScene();
    }

    //シーンの切り替え
    void ChangeScene()
    {
        if(change)
        {
            return;
        }

        //現在操作しているプレイヤーのID
        int player_id = pc.ControlPlayerID;

        foreach (var player in pc.PlayersData)
        {
            //操作しているプレイヤーを参照していた場合は次のループ
            if (player_id == player.PlayerID)
            {
                continue;
            }

            //生きているプレイヤーがいればシーンを遷移しない
            if (player.IsAlive2)
            {
                //Debug.Log("生きてます");
                return;
            }
        }

        SceneManager.LoadScene("ResultScene");
        change = true;
        //Debug.Log("自分以外全員死んでいるので遷移");
    }

    //void InputKey(InputAction.CallbackContext obj)
    //{
    //    ChangeScene();
    //}
}

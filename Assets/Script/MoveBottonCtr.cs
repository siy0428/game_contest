using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//機能　　キー　     保存番号
//左　　　　A      down 0 up 1
//右　　　　D      down 2 up 3
//ジャンプ　W           4
//シュート　マオス　　　5
public class MoveBottonCtr : MonoBehaviour
{
    //プレイヤーの座標を取得する用
    public Vector2 playerpostion;

    //移動の方向を決める(-1左、0止まる、1右)
    public int IsMove = 0;

    //キーの状態　　　A
    BehaviorData KeyA = new BehaviorData();

    //キーの状態　　　D
    BehaviorData KeyD = new BehaviorData();

    //キーの状態　　　W
    BehaviorData KeyW = new BehaviorData();

    int KeyADown = 0;
    int KeyDDown = 0;
    int KeyWDown = 0;
    Vector2 moveValue = new Vector2();
    Vector2 moveValuePref = new Vector2();
    public void LeftStickIsUse(InputAction _LeftStick)
    {
        moveValuePref = moveValue;
        moveValue = _LeftStick.ReadValue<Vector2>();

        if(moveValue.x > 0.4f)
        {
            KeyDDown = 1;
        }
        else if(moveValue.x < -0.4f)
        {
            KeyADown = 1;
        }

        if(moveValuePref.x > 0.4f && moveValue.x < 0.4f)
        {
            KeyDDown = -1;
        }

        if(moveValuePref.x < -0.4f && moveValue.x > -0.4f)
        {
            KeyADown = -1;
        }      
    }

    public void JumpBottonIsDown(InputAction.CallbackContext obj)
    {
        KeyWDown = 1;
    }

    public void MoveBottonUse(Player _PlayerCtr)
    {
        //記録が許可がある場合のみ、操作できる
        if(_PlayerCtr.StartBehaviourRecord)
        {
            //移動量計算
            float moveX = 0.0f;

            //左が押したら
            //if (Input.GetKeyDown(KeyCode.A))
            if (KeyADown > 0)
            {
                //左と右が同時に押したことの記入を防ぐため、止まる状態じゃなければ反応しない
                if(IsMove == 0)
                {
                    //移動状態更新
                    IsMove = -1;
                    _PlayerCtr.SetScale(_PlayerCtr.ControlPlayerID,new Vector3(-1.0f, 1.0f, 0.0f));
                    
                    //データ記録作成
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.BottonID = 0;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyA);
                }
            }

            //右が押したら
            //if (Input.GetKeyDown(KeyCode.D))
            if (KeyDDown > 0)
            {
                //左と右が同時に押したことの記入を防ぐため、止まる状態じゃなければ反応しない
                if (IsMove == 0)
                {
                    //移動状態更新
                    IsMove = 1;
                    _PlayerCtr.SetScale(_PlayerCtr.ControlPlayerID, new Vector3(1.0f, 1.0f, 0.0f));

                    //データ記録作成
                    KeyD.StartTime = _PlayerCtr.Timer;
                    KeyD.BottonID = 2;
                    KeyD.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyD);

                }
            }

            //左が離されたら
            //if (Input.GetKeyUp(KeyCode.A))
            if (KeyADown < 0)
            {
                //上と同じ、正しいく記録するため、左へ移動中しか止まらない
                if (IsMove == -1)
                {
                    //移動状態更新
                    IsMove = 0;

                    //データ記録作成
                    KeyA.BottonID = 1;
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyA);
                }

                KeyADown = 0;
            }

            //右が離されたら
            //if (Input.GetKeyUp(KeyCode.D))
            if (KeyDDown < 0)
            {
                //上と同じ、正しいく記録するため、右へ移動中しか止まらない
                if (IsMove == 1)
                {
                    //移動状態更新
                    IsMove = 0;

                    //データ記録作成
                    KeyD.StartTime = _PlayerCtr.Timer;
                    KeyD.BottonID = 3;
                    KeyD.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyD);
                }
                KeyDDown = 0;
            }

            //移動距離計算
            moveX = IsMove * _PlayerCtr.MoveSpeed * Time.deltaTime;

            //ジャンプキー判定
            //if (Input.GetKeyDown(KeyCode.W))
            if (KeyWDown > 0)
            {
                //ジャンプしてない状態のみ、ジャンプできる
                if (!_PlayerCtr.IsJump[_PlayerCtr.ControlPlayerID])
                {
                    //ジャンプ状態更新
                    _PlayerCtr.IsJump[_PlayerCtr.ControlPlayerID] = true;

                    //データ記録作成
                    KeyW.StartTime = _PlayerCtr.Timer;
                    KeyW.BottonID = 4;
                    KeyW.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyW);
                }
                KeyWDown = 0;
            }

            //プレイヤーの座標取得
            playerpostion = _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position;

            //座標変更
            _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position = new Vector2(playerpostion.x + moveX, playerpostion.y);           
        }
    }
}

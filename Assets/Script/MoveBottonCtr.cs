using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void MoveBottonUse(PlayerController _PlayerCtr)
    {
        //記録が許可がある場合のみ、操作できる
        if(_PlayerCtr.StartBehaviourRecord)
        {
            //移動量計算
            float moveX = 0.0f;

            //左が押したら
            if (Input.GetKeyDown(KeyCode.A))
            {
                //左と右が同時に押したことの記入を防ぐため、止まる状態じゃなければ反応しない
                if(IsMove == 0)
                {
                    //移動状態更新
                    IsMove = -1;

                    //データ記録作成
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.BottonID = 0;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyA);
                }
            }

            //右が押したら
            if (Input.GetKeyDown(KeyCode.D))
            {
                //左と右が同時に押したことの記入を防ぐため、止まる状態じゃなければ反応しない
                if (IsMove == 0)
                {
                    //移動状態更新
                    IsMove = 1;

                    //データ記録作成
                    KeyD.StartTime = _PlayerCtr.Timer;
                    KeyD.BottonID = 2;
                    KeyD.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyD);

                }
            }

            //左が離されたら
            if (Input.GetKeyUp(KeyCode.A))
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
            }

            //右が離されたら
            if (Input.GetKeyUp(KeyCode.D))
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
            }

            //移動距離計算
            moveX = IsMove * _PlayerCtr.MoveSpeed * Time.deltaTime;

            //ジャンプキー判定
            if (Input.GetKeyDown(KeyCode.W))
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
            }

            //プレイヤーの座標取得
            playerpostion = _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position;

            //座標変更
            _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position = new Vector2(playerpostion.x + moveX, playerpostion.y);           
        }
    }
}

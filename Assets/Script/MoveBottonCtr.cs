using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//機能　　キー　     保存番号
//左　　　　A      down 0 up 1
//右　　　　D      down 2 up 3
//ジャンプ　Space       4
//シュート　マオス　　　5
//上        W      down 6 up 7
//下        S      down 8 up 9

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

    //キーの状態　　　Space
    BehaviorData KeySpace = new BehaviorData();

    BehaviorData KeyWS = new BehaviorData();

    int KeyADown = 0;
    int KeyDDown = 0;
    int KeySpaceDown = 0;
    int keyWDown = 0;
    int keySDown = 0;

    Vector2 moveValue = new Vector2();
    Vector2 moveValuePref = new Vector2();
    public void LeftStickIsUse(InputAction _LeftStick)
    {
        moveValuePref = moveValue;
        moveValue = _LeftStick.ReadValue<Vector2>();

        if (moveValue.x > 0.4f)
        {
            KeyDDown = 1;
        }
        else if (moveValue.x < -0.4f)
        {
            KeyADown = 1;
        }

        if (moveValue.y > 0.4f)
        {
            keyWDown = 1;
        }
        else if (moveValue.y < -0.4f)
        {
            keySDown = 1;
        }

        if (moveValuePref.x > 0.4f && moveValue.x < 0.4f)
        {
            KeyDDown = -1;
        }

        if (moveValuePref.x < -0.4f && moveValue.x > -0.4f)
        {
            KeyADown = -1;
        }

        if (moveValuePref.y > 0.4f && moveValue.y < 0.4f)
        {
            keyWDown = -1;
        }

        if (moveValuePref.y < -0.4f && moveValue.y > -0.4f)
        {
            keySDown = -1;
        }
    }

    public void JumpBottonIsDown(InputAction.CallbackContext obj)
    {
        KeySpaceDown = 1;
    }

    public void MoveBottonUse(PlayerController _PlayerCtr)
    {
        //記録が許可がある場合のみ、操作できる
        if (_PlayerCtr.StartBehaviourRecord && _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump && _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump2)
        {
            //移動量計算
            float moveX = 0.0f;

            //左が押したら
            if (KeyADown > 0)
            {
                //左と右が同時に押したことの記入を防ぐため、止まる状態じゃなければ反応しない
                if (IsMove == 0)
                {
                    //移動状態更新
                    IsMove = -1;
                    _PlayerCtr.SetScale(_PlayerCtr.ControlPlayerID, new Vector3(-1.0f, 1.0f, 0.0f));

                    //データ記録作成
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.BottonID = 0;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyA);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", true);
                }
            }

            //右が押したら
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
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyD);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", true);
                }
            }

            //左が離されたら
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
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyA);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", false);
                }

                KeyADown = 0;
            }

            //右が離されたら
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
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyD);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", false);
                }
                KeyDDown = 0;
            }

            //移動距離計算
            moveX = IsMove * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].MoveSpeed * Time.deltaTime;

            //ジャンプキー判定
            if (KeySpaceDown > 0)
            {
                //ジャンプしてない状態のみ、ジャンプできる
                if (!_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].IsJump && _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].JumpedTimes < _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].JumpStep)
                {

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetTrigger("doJump");

                    //ジャンプ状態更新
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].IsJump = true;

                    //データ記録作成
                    KeySpace.StartTime = _PlayerCtr.Timer;

                    if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].CheakSkill(SkillID.JUMPSMARSH))
                    {
                        if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].JumpedTimes == 1 && (KeyDDown > 0 || KeyADown > 0 || keyWDown > 0 || keySDown > 0))
                        {
                            if (KeyDDown > 0)
                            {
                                KeySpace.BottonID = 41;
                                _PlayerCtr.SkillDataCtr.JumpSmarshDir = new Vector2(1, 0);
                            }
                            else if (KeyADown > 0)
                            {
                                KeySpace.BottonID = 42;
                                _PlayerCtr.SkillDataCtr.JumpSmarshDir = new Vector2(-1, 0);
                            }
                            else if (keyWDown > 0)
                            {
                                KeySpace.BottonID = 43;
                                _PlayerCtr.SkillDataCtr.JumpSmarshDir = new Vector2(0, 1);

                            }
                            else if (keySDown > 0)
                            {
                                KeySpace.BottonID = 44;
                                _PlayerCtr.SkillDataCtr.JumpSmarshDir = new Vector2(0, -1);
                            }
                            _PlayerCtr.SkillDataCtr.UseJumpSmarsh = true;
                            animators[1].SetTrigger("doDash");
                        }
                        else if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].JumpedTimes == 0)
                        {
                            KeySpace.BottonID = 4;
                        }
                    }
                    else
                    {
                        KeySpace.BottonID = 4;
                    }
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].OnBox = false;
                    KeySpace.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeySpace);
                }
                KeySpaceDown = 0;
            }

            if (keyWDown > 0)
            {
                if(KeySpace.BottonID!=43)
                {
                    //データ記録作成
                    KeyWS.StartTime = _PlayerCtr.Timer;
                    KeyWS.BottonID = 6;
                    KeyWS.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyWS);
                }

            }

            if (keyWDown < 0)
            {
                keyWDown = 0;
            }

            if (keySDown > 0)
            {
                if (KeySpace.BottonID != 44)
                {
                    //データ記録作成
                    KeyWS.StartTime = _PlayerCtr.Timer;
                    KeyWS.BottonID = 8;
                    KeyWS.PlayerID = _PlayerCtr.ControlPlayerID;

                    //記録リストに追加
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyWS);
                }
            }

            if (keySDown < 0)
            {
                keySDown = 0;
            }

            //プレイヤーの座標取得
            playerpostion = _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position;

            //座標変更
            if (!_PlayerCtr.CheakJumpSmarsh(_PlayerCtr.ControlPlayerID))
            {
                _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position = new Vector3(playerpostion.x + moveX, playerpostion.y,
                    _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].transform.position.z);
            }
        }
    }
}

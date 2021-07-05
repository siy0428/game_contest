using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�@�\�@�@�L�[�@     �ۑ��ԍ�
//���@�@�@�@A      down 0 up 1
//�E�@�@�@�@D      down 2 up 3
//�W�����v�@Space       4
//�V���[�g�@�}�I�X�@�@�@5
//��        W      down 6 up 7
//��        S      down 8 up 9

public class MoveBottonCtr : MonoBehaviour
{
    //�v���C���[�̍��W���擾����p
    public Vector2 playerpostion;

    //�ړ��̕��������߂�(-1���A0�~�܂�A1�E)
    public int IsMove = 0;

    //�L�[�̏�ԁ@�@�@A
    BehaviorData KeyA = new BehaviorData();

    //�L�[�̏�ԁ@�@�@D
    BehaviorData KeyD = new BehaviorData();

    //�L�[�̏�ԁ@�@�@Space
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
        //�L�^����������ꍇ�̂݁A����ł���
        if (_PlayerCtr.StartBehaviourRecord && _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump && _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump2)
        {
            //�ړ��ʌv�Z
            float moveX = 0.0f;

            //������������
            if (KeyADown > 0)
            {
                //���ƉE�������ɉ��������Ƃ̋L����h�����߁A�~�܂��Ԃ���Ȃ���Δ������Ȃ�
                if (IsMove == 0)
                {
                    //�ړ���ԍX�V
                    IsMove = -1;
                    _PlayerCtr.SetScale(_PlayerCtr.ControlPlayerID, new Vector3(-1.0f, 1.0f, 0.0f));

                    //�f�[�^�L�^�쐬
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.BottonID = 0;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyA);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", true);
                }
            }

            //�E����������
            if (KeyDDown > 0)
            {
                //���ƉE�������ɉ��������Ƃ̋L����h�����߁A�~�܂��Ԃ���Ȃ���Δ������Ȃ�
                if (IsMove == 0)
                {
                    //�ړ���ԍX�V
                    IsMove = 1;
                    _PlayerCtr.SetScale(_PlayerCtr.ControlPlayerID, new Vector3(1.0f, 1.0f, 0.0f));

                    //�f�[�^�L�^�쐬
                    KeyD.StartTime = _PlayerCtr.Timer;
                    KeyD.BottonID = 2;
                    KeyD.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyD);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", true);
                }
            }

            //���������ꂽ��
            if (KeyADown < 0)
            {
                //��Ɠ����A���������L�^���邽�߁A���ֈړ��������~�܂�Ȃ�
                if (IsMove == -1)
                {
                    //�ړ���ԍX�V
                    IsMove = 0;

                    //�f�[�^�L�^�쐬
                    KeyA.BottonID = 1;
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyA);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", false);
                }

                KeyADown = 0;
            }

            //�E�������ꂽ��
            if (KeyDDown < 0)
            {
                //��Ɠ����A���������L�^���邽�߁A�E�ֈړ��������~�܂�Ȃ�
                if (IsMove == 1)
                {
                    //�ړ���ԍX�V
                    IsMove = 0;

                    //�f�[�^�L�^�쐬
                    KeyD.StartTime = _PlayerCtr.Timer;
                    KeyD.BottonID = 3;
                    KeyD.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyD);

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetBool("isMoving", false);
                }
                KeyDDown = 0;
            }

            //�ړ������v�Z
            moveX = IsMove * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].MoveSpeed * Time.deltaTime;

            //�W�����v�L�[����
            if (KeySpaceDown > 0)
            {
                //�W�����v���ĂȂ���Ԃ̂݁A�W�����v�ł���
                if (!_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].IsJump && _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].JumpedTimes < _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].JumpStep)
                {

                    Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                    animators[1].SetTrigger("doJump");

                    //�W�����v��ԍX�V
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].IsJump = true;

                    //�f�[�^�L�^�쐬
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

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeySpace);
                }
                KeySpaceDown = 0;
            }

            if (keyWDown > 0)
            {
                if(KeySpace.BottonID!=43)
                {
                    //�f�[�^�L�^�쐬
                    KeyWS.StartTime = _PlayerCtr.Timer;
                    KeyWS.BottonID = 6;
                    KeyWS.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
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
                    //�f�[�^�L�^�쐬
                    KeyWS.StartTime = _PlayerCtr.Timer;
                    KeyWS.BottonID = 8;
                    KeyWS.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeyWS);
                }
            }

            if (keySDown < 0)
            {
                keySDown = 0;
            }

            //�v���C���[�̍��W�擾
            playerpostion = _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position;

            //���W�ύX
            if (!_PlayerCtr.CheakJumpSmarsh(_PlayerCtr.ControlPlayerID))
            {
                _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position = new Vector3(playerpostion.x + moveX, playerpostion.y,
                    _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].transform.position.z);
            }
        }
    }
}

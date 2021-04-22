using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�@�\�@�@�L�[�@     �ۑ��ԍ�
//���@�@�@�@A      down 0 up 1
//�E�@�@�@�@D      down 2 up 3
//�W�����v�@W           4
//�V���[�g�@�}�I�X�@�@�@5
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

    //�L�[�̏�ԁ@�@�@W
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
        //�L�^����������ꍇ�̂݁A����ł���
        if(_PlayerCtr.StartBehaviourRecord)
        {
            //�ړ��ʌv�Z
            float moveX = 0.0f;

            //������������
            //if (Input.GetKeyDown(KeyCode.A))
            if (KeyADown > 0)
            {
                //���ƉE�������ɉ��������Ƃ̋L����h�����߁A�~�܂��Ԃ���Ȃ���Δ������Ȃ�
                if(IsMove == 0)
                {
                    //�ړ���ԍX�V
                    IsMove = -1;
                    _PlayerCtr.SetScale(_PlayerCtr.ControlPlayerID,new Vector3(-1.0f, 1.0f, 0.0f));
                    
                    //�f�[�^�L�^�쐬
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.BottonID = 0;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyA);
                }
            }

            //�E����������
            //if (Input.GetKeyDown(KeyCode.D))
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
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyD);

                }
            }

            //���������ꂽ��
            //if (Input.GetKeyUp(KeyCode.A))
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
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyA);
                }

                KeyADown = 0;
            }

            //�E�������ꂽ��
            //if (Input.GetKeyUp(KeyCode.D))
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
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyD);
                }
                KeyDDown = 0;
            }

            //�ړ������v�Z
            moveX = IsMove * _PlayerCtr.MoveSpeed * Time.deltaTime;

            //�W�����v�L�[����
            //if (Input.GetKeyDown(KeyCode.W))
            if (KeyWDown > 0)
            {
                //�W�����v���ĂȂ���Ԃ̂݁A�W�����v�ł���
                if (!_PlayerCtr.IsJump[_PlayerCtr.ControlPlayerID])
                {
                    //�W�����v��ԍX�V
                    _PlayerCtr.IsJump[_PlayerCtr.ControlPlayerID] = true;

                    //�f�[�^�L�^�쐬
                    KeyW.StartTime = _PlayerCtr.Timer;
                    KeyW.BottonID = 4;
                    KeyW.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyW);
                }
                KeyWDown = 0;
            }

            //�v���C���[�̍��W�擾
            playerpostion = _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position;

            //���W�ύX
            _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position = new Vector2(playerpostion.x + moveX, playerpostion.y);           
        }
    }
}

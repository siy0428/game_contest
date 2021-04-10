using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void MoveBottonUse(PlayerController _PlayerCtr)
    {
        //�L�^����������ꍇ�̂݁A����ł���
        if(_PlayerCtr.StartBehaviourRecord)
        {
            //�ړ��ʌv�Z
            float moveX = 0.0f;

            //������������
            if (Input.GetKeyDown(KeyCode.A))
            {
                //���ƉE�������ɉ��������Ƃ̋L����h�����߁A�~�܂��Ԃ���Ȃ���Δ������Ȃ�
                if(IsMove == 0)
                {
                    //�ړ���ԍX�V
                    IsMove = -1;

                    //�f�[�^�L�^�쐬
                    KeyA.StartTime = _PlayerCtr.Timer;
                    KeyA.BottonID = 0;
                    KeyA.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyA);
                }
            }

            //�E����������
            if (Input.GetKeyDown(KeyCode.D))
            {
                //���ƉE�������ɉ��������Ƃ̋L����h�����߁A�~�܂��Ԃ���Ȃ���Δ������Ȃ�
                if (IsMove == 0)
                {
                    //�ړ���ԍX�V
                    IsMove = 1;

                    //�f�[�^�L�^�쐬
                    KeyD.StartTime = _PlayerCtr.Timer;
                    KeyD.BottonID = 2;
                    KeyD.PlayerID = _PlayerCtr.ControlPlayerID;

                    //�L�^���X�g�ɒǉ�
                    _PlayerCtr.RecordBehaviour.AddBehaviour(KeyD);

                }
            }

            //���������ꂽ��
            if (Input.GetKeyUp(KeyCode.A))
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
            }

            //�E�������ꂽ��
            if (Input.GetKeyUp(KeyCode.D))
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
            }

            //�ړ������v�Z
            moveX = IsMove * _PlayerCtr.MoveSpeed * Time.deltaTime;

            //�W�����v�L�[����
            if (Input.GetKeyDown(KeyCode.W))
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
            }

            //�v���C���[�̍��W�擾
            playerpostion = _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position;

            //���W�ύX
            _PlayerCtr.Players[_PlayerCtr.ControlPlayerID].GetComponent<Transform>().position = new Vector2(playerpostion.x + moveX, playerpostion.y);           
        }
    }
}

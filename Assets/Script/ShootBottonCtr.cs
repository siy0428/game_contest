using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBottonCtr : MonoBehaviour
{
    //�V���[�g�s���f�[�^
    BehaviorData KeyShoot = new BehaviorData();

    public float ShootCD = 0.5f;

    public float ShootCDTimer = 0.0f;

    int KeyShootDown = 0;

    public void ShootBottonIsDown(InputAction.CallbackContext obj)
    {
        KeyShootDown = 1;
    }



    //ID�͓���̎�
    public void ShootKeyDown(Player _PlayerCtr,int ID,Vector2 _ShootDir = new Vector2())
    {
        
        if (KeyShootDown > 0 && ID == _PlayerCtr.ControlPlayerID)//����Ώۂ̏���
        {
           // �e�i�Q�[���I�u�W�F�N�g�j�̐���
           GameObject clone = Instantiate(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);

            // �N���b�N�������W�̎擾�i�X�N���[�����W���烏�[���h���W�ɕϊ��j
            //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �����̐����iZ�����̏����Ɛ��K���j
            //Vector3 shotForward = Vector3.Scale((mouseWorldPos - _PlayerCtr.Players[ID].GetComponent<Transform>().position), new Vector3(1, 1, 0)).normalized;
            Vector3 shotForward = new Vector3(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].PlayersForward.x, 0.0f, 0.0f);
            // �e�ɑ��x��^����
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].BulletSpeed;

            //�f�[�^�L�^����
            KeyShoot.PlayerID = _PlayerCtr.ControlPlayerID;

            KeyShoot.BottonID = 5;

            KeyShoot.StartTime = _PlayerCtr.Timer;

            KeyShoot.ShootDir = shotForward;

            //�L�^�ɒǉ�
            _PlayerCtr.RecordBehaviour.AddBehaviour(KeyShoot);

            KeyShootDown = -1;
        }
        else if(ID != _PlayerCtr.ControlPlayerID)//�񑀍�Ώۂ̏���
        {
            // �e�i�Q�[���I�u�W�F�N�g�j�̐���
            GameObject clone = Instantiate(_PlayerCtr.PlayersData[ID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);

            //���ڕ������擾
            Vector3 shotForward = _ShootDir;

            // �e�ɑ��x��^����
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].BulletSpeed;
        }
    }

    private void Update()
    {
        if(KeyShootDown < 0)
        {
            ShootCDTimer += Time.deltaTime;
        }

        if(ShootCDTimer>= ShootCD)
        {
            KeyShootDown = 0;
            ShootCDTimer = 0.0f;
        }
    }
}

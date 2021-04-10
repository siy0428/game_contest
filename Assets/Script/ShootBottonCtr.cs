using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBottonCtr : MonoBehaviour
{
    //�V���[�g�s���f�[�^
    BehaviorData KeyShoot = new BehaviorData();

    //ID�͓���̎�
    public void ShootKeyDown(PlayerController _PlayerCtr,int ID,Vector2 _ShootDir = new Vector2())
    {
        if (Input.GetMouseButtonDown(0) && ID == _PlayerCtr.ControlPlayerID)//����Ώۂ̏���
        {
           // �e�i�Q�[���I�u�W�F�N�g�j�̐���
           GameObject clone = Instantiate(_PlayerCtr.Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);

            // �N���b�N�������W�̎擾�i�X�N���[�����W���烏�[���h���W�ɕϊ��j
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �����̐����iZ�����̏����Ɛ��K���j
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - _PlayerCtr.Players[ID].GetComponent<Transform>().position), new Vector3(1, 1, 0)).normalized;

            // �e�ɑ��x��^����
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.BulletSpeed;

            //�f�[�^�L�^����
            KeyShoot.PlayerID = _PlayerCtr.ControlPlayerID;

            KeyShoot.BottonID = 5;

            KeyShoot.StartTime = _PlayerCtr.Timer;

            KeyShoot.ShootDir = shotForward;

            //�L�^�ɒǉ�
            _PlayerCtr.RecordBehaviour.AddBehaviour(KeyShoot);
        }
        else if(ID != _PlayerCtr.ControlPlayerID)//�񑀍�Ώۂ̏���
        {
            // �e�i�Q�[���I�u�W�F�N�g�j�̐���
            GameObject clone = Instantiate(_PlayerCtr.Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);

            //���ڕ������擾
            Vector3 shotForward = _ShootDir;

            // �e�ɑ��x��^����
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.BulletSpeed;
        }
    }
}

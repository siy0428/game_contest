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

    public List<GameObject> m_BulletsList = new List<GameObject>();

    public void ShootBottonIsDown(InputAction.CallbackContext obj)
    {
        KeyShootDown = 1;
    }

    private Vector3 ShotPos;

    public void SetShotPos(Vector3 pos)
    {
        ShotPos = pos;
    }

    private bool CanShot;

    public void SetCanShot(bool can)
    {
        CanShot = can;
    }

    //ID�͓���̎�
    public void ShootKeyDown(PlayerController _PlayerCtr, int ID, Vector2 _ShootDir = new Vector2())
    {
        if (KeyShootDown > 0 && ID == _PlayerCtr.ControlPlayerID)//����Ώۂ̏���
        {
            //�ˌ��\�ȏꍇ
            if (CanShot)
            {
                //����L�����N�^�[�̃V���[�g�̊Ԋu���擾����
                ShootCD = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].ShootCD;

                // �e�i�Q�[���I�u�W�F�N�g�j�̐���
                GameObject clone = Instantiate(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position + _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion, Quaternion.identity);
                clone.GetComponent<BulletData>().SetTarget(ShotPos);    //�e�̕���


                // �����̐����iZ�����̏����Ɛ��K���j
                Vector3 shotForward = new Vector3(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].PlayersForward.x, 0.0f, 0.0f);

                //�f�[�^�L�^����
                KeyShoot.PlayerID = _PlayerCtr.ControlPlayerID;

                KeyShoot.BottonID = 5;

                KeyShoot.StartTime = _PlayerCtr.Timer;

                KeyShoot.ShootDir = ShotPos;

                //�L�^�ɒǉ�
                _PlayerCtr.RecordBehaviour.AddBehaviour(KeyShoot);

                KeyShootDown = -1;

                clone.GetComponent<Collision>().PlayerID = ID;
                m_BulletsList.Add(clone);
            }
        }
        else if (ID != _PlayerCtr.ControlPlayerID)//�񑀍�Ώۂ̏���
        {
            // �e�i�Q�[���I�u�W�F�N�g�j�̐���
            GameObject clone = Instantiate(_PlayerCtr.PlayersData[ID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position + _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion, Quaternion.identity);

            //���ڕ������擾
            Vector3 shotForward = _ShootDir;
            clone.GetComponent<BulletData>().SetTarget(_ShootDir);

            // �e�ɑ��x��^����
            //clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].BulletSpeed;

            clone.GetComponent<Collision>().PlayerID = ID;
            m_BulletsList.Add(clone);
        }
    }

    private void Start()
    {
        ShotPos = new Vector3(0.0f, 0.0f, 0.0f);
        CanShot = false;
    }

    private void Update()
    {
        if (KeyShootDown < 0)
        {
            ShootCDTimer += Time.deltaTime;
        }

        if (ShootCDTimer >= ShootCD)
        {
            KeyShootDown = 0;
            ShootCDTimer = 0.0f;
        }
    }
}

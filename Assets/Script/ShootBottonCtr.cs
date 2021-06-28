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

    CharacterUIController ChaUICtr;

    PlayerController PCtr;
    public void ShootBottonIsDown(InputAction.CallbackContext obj)
    {
        if(KeyShootDown == 0)
        {
            KeyShootDown = 1;
        }
    }

    private Vector3 ShotPos;

    public void SetShotPos(int id,Vector3 pos)
    {
        PCtr.PlayersData[id].ShootPos = pos;
        //ShotPos = pos;
    }

    private bool CanShot;

    public void SetCanShot(int id ,bool can)
    {
        PCtr.PlayersData[id].CanShoot = can;
        //CanShot = can;
    }

    public bool GetCanShot()
    {
        return CanShot;
    }

    //ID�͓���̎�
    public void ShootKeyDown(PlayerController _PlayerCtr, int ID, Vector2 _ShootDir = new Vector2())
    {
        if (ID == _PlayerCtr.ControlPlayerID)//����Ώۂ̏���
        {
            //�ˌ��\�ȏꍇ
            if (_PlayerCtr.PlayersData[ID].CanShoot && !_PlayerCtr.PlayersData[ID].ShootIntoCD)
            {
                if (_PlayerCtr.BreakStealth(ID))
                {
                    return;
                }

                Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                animators[0].SetTrigger("doAttack");

                //����L�����N�^�[�̃V���[�g�̊Ԋu���擾����
                //ShootCD = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].ShootCD;

                // �e�i�Q�[���I�u�W�F�N�g�j�̐���
                //GameObject clone = Instantiate(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position + _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion, Quaternion.identity);
                //clone.GetComponent<BulletData>().SetTarget(ShotPos);    //�e�̕���
                //clone.GetComponent<BulletData>().SetShootPosition(clone.transform.position);


                // �����̐����iZ�����̏����Ɛ��K���j
                Vector3 shotForward = new Vector3(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].PlayersForward.x, 0.0f, 0.0f);

                //�f�[�^�L�^����
                //KeyShoot.PlayerID = _PlayerCtr.ControlPlayerID;

                //KeyShoot.BottonID = 5;

                //KeyShoot.StartTime = _PlayerCtr.Timer;

                //KeyShoot.ShootDir = ShotPos;

                //�L�^�ɒǉ�
                //_PlayerCtr.RecordBehaviour.AddBehaviour(KeyShoot);

                //KeyShootDown = -1;

                Vector3 fixpos = _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion;
                fixpos = new Vector3(fixpos.x * shotForward.x, fixpos.y, fixpos.z);
                _PlayerCtr.PlayersData[ID].gameObject.GetComponentInChildren<ShootKeeper>().SetParama(_PlayerCtr.PlayersData[ID].ShootPos + fixpos, ID);
                _PlayerCtr.PlayersData[ID].ShotTimes++;
                _PlayerCtr.PlayersData[ID].ShootIntoCD = true;
               
                //clone.GetComponent<Collision>().PlayerID = ID;
                //m_BulletsList.Add(clone);
            }
            else
            {
                //KeyShootDown = 0;
            }
        }
        else if (ID != _PlayerCtr.ControlPlayerID)//�񑀍�Ώۂ̏���
        {
            // �e�i�Q�[���I�u�W�F�N�g�j�̐���
            //GameObject clone = Instantiate(_PlayerCtr.PlayersData[ID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position + _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion, Quaternion.identity);

            //���ڕ������擾
            // Vector3 shotForward = _ShootDir;
            // clone.GetComponent<BulletData>().SetTarget(_ShootDir);
            // clone.GetComponent<BulletData>().SetShootPosition(clone.transform.position);
            // �e�ɑ��x��^����
            //clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].BulletSpeed;

            //clone.GetComponent<Collision>().PlayerID = ID;
            //m_BulletsList.Add(clone);
            if (_PlayerCtr.PlayersData[ID].CanShoot && !_PlayerCtr.PlayersData[ID].ShootIntoCD)
            {
                if (_PlayerCtr.BreakStealth(ID))
                {
                    return;
                }

                Animator[] animators = _PlayerCtr.PlayersData[ID].GetComponentsInChildren<Animator>();
                animators[0].SetTrigger("doAttack");

                Vector3 shotForward = new Vector3(_PlayerCtr.PlayersData[ID].PlayersForward.x, 0.0f, 0.0f);
                Vector3 fixpos = _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion;
                fixpos = new Vector3(fixpos.x * shotForward.x, fixpos.y, fixpos.z);

                //_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].gameObject.GetComponentInChildren<ShootKeeper>().SetParama(ShotPos, ID);
                _PlayerCtr.PlayersData[ID].gameObject.GetComponentInChildren<ShootKeeper>().SetParama(_PlayerCtr.PlayersData[ID].ShootPos + fixpos, ID);
                _PlayerCtr.PlayersData[ID].ShotTimes++;
                if (_PlayerCtr.PlayersData[ID].ShotTimes >= _PlayerCtr.PlayersData[ID].EnableShootTimes)
                {
                    _PlayerCtr.PlayersData[ID].ShootIntoCD = true;
                }
               
            }
        }
    }

    public void ShootKeyDown_Skill(PlayerController _PlayerCtr, int ID, GameObject _SkillBulletObj, Vector2 _ShootDir = new Vector2())
    {
        GameObject clone = Instantiate(_SkillBulletObj, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);
        clone.GetComponent<BulletData>().SetTarget(_ShootDir);    //�e�̕���
        clone.GetComponent<BulletData>().SetShootPosition(clone.transform.position);
        if (_ShootDir.x > 0)
        {
            clone.transform.localScale = new Vector3(-clone.transform.localScale.x, clone.transform.localScale.y, 0);
            clone.transform.position = new Vector3(clone.transform.position.x + _PlayerCtr.SkillDataCtr.CutFixPosition.x, clone.transform.position.y - _PlayerCtr.SkillDataCtr.CutFixPosition.y,0);
        }
        else
        {
            clone.transform.position -= _PlayerCtr.SkillDataCtr.CutFixPosition;
        }
        clone.GetComponent<Collision>().PlayerID = ID;
        m_BulletsList.Add(clone);
    }
    
    public void CreateBullet(PlayerController _PlayerCtr, int ID,Vector3 pos, Vector3 dir)
    {
        Player pl = _PlayerCtr.PlayersData[ID];
        if(pl.Bullet.GetComponent<BulletData>().subBullet)
        {
            GameObject clone = Instantiate(pl.Bullet.GetComponent<BulletData>().subBullet, pos, Quaternion.identity);
            clone.GetComponent<BulletData>().SetTarget(pos + 1000 * dir);    //�e�̕���
            clone.GetComponent<BulletData>().SetShootPosition(new Vector3(clone.transform.position.x, clone.transform.position.y, 1.0f));
            clone.GetComponent<Collision>().PlayerID = ID;
            m_BulletsList.Add(clone);
        }
    }

    private void Start()
    {
        ShotPos = new Vector3(0.0f, 0.0f, 0.0f);
        CanShot = false;

        ChaUICtr = FindObjectOfType<CharacterUIController>();
        PCtr = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        /*
        if (KeyShootDown < 0)
        {
            ShootCDTimer += Time.deltaTime;
        }

        if (ShootCDTimer >= ShootCD)
        {
            KeyShootDown = 0;
            ShootCDTimer = 0.0f;
        }
        */

        for(int i = 0; i < PCtr.PlayersData.Count;i++)
        {
            PCtr.PlayersData[i].ShootCDFunc();
        }

        ChaUICtr.CDMaskUpdate(0);
    }
}

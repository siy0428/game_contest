using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionDir
{
    UPDOWN,
    LEFTRIGHT
}


public class Collision : MonoBehaviour
{
    public int PlayerID;
    private PlayerController PlayerCtr;

    private CollisionDir Cdir;
    private ContactPoint2D[] cp2;

    private EnemyManager em;
    private LoopManager lm;

    private CharacterUIController CUICtr;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();
        cp2 = new ContactPoint2D[1];
        CUICtr = FindObjectOfType<CharacterUIController>();
        em = FindObjectOfType<EnemyManager>();
        lm = FindObjectOfType<LoopManager>();
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetContacts(cp2);

        if (collider.gameObject.tag == "Player")
        {
            Player en = collider.gameObject.GetComponent<Player>();
            if (PlayerID != en.PlayerID)    //�������g�ɓ�����Ȃ�����
            {
                Player py = PlayerCtr.PlayersData[PlayerID];
                if (en.HP > 0)
                {
                    en.Hurt(py.Bullet.GetComponent<BulletData>().m_Attack + py.ATK);
                    BulletData bd = gameObject.GetComponent<BulletData>();
                    DriveOff(gameObject, collider.gameObject, bd.m_Type);
                    ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                    sbc.m_BulletsList.Remove(this.gameObject);
                    GameObject.Destroy(this.gameObject);
                  
                    if (en.HP <= 0)
                    {
                        //PlayerCtr.PlayersData[collider.gameObject.GetComponent<Player>().PlayerID].IsAlive = false;
                        collider.gameObject.SetActive(false);
                        //if (PlayerID != PlayerCtr.ControlPlayerID)
                        //{
                        //    lm.LoopAgain(); //�������[�v�̐���
                        //}
                    }
                    else
                    {
                        if (PlayerID != PlayerCtr.ControlPlayerID)
                        {
                            CUICtr.ChangeHP(en.PlayerID);
                            //Debug.Log(en + "��HP:" + en.HP);
                        }
                    }
                }
            }
        }

        if (collider.gameObject.tag == "Ground")
        {
            BulletData bd = gameObject.GetComponent<BulletData>();
            if (bd.CheakRebound())
            {
                bd.ReboundedTimes++;
                Vector3 od = bd.ReboundDir;
                Debug.Log(cp2[0].normal);

                if (cp2[0].normal.x >= 0.5f || cp2[0].normal.x <= -0.5f)
                {
                    Cdir = CollisionDir.LEFTRIGHT;
                }

                if (Cdir == CollisionDir.UPDOWN)
                {
                    bd.ReboundDir = new Vector3(od.x, -od.y, od.z);
                    Vector2 v = bd.transform.GetComponent<Rigidbody2D>().velocity;
                    bd.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(v.x, -v.y) * Mathf.Pow(bd.ReboundRate, bd.ReboundedTimes);
                }
                else
                {
                    bd.ReboundDir = new Vector3(-od.x, od.y, od.z);
                    Vector2 v = bd.transform.GetComponent<Rigidbody2D>().velocity;
                    bd.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-v.x, v.y) * Mathf.Pow(bd.ReboundRate, bd.ReboundedTimes);
                }
            }
            else
            {
                if(bd.m_Type == BulletType.Sword_1)
                {
                }
                else
                {
                    ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                    sbc.m_BulletsList.Remove(this.gameObject);
                    GameObject.Destroy(this.gameObject);
                }
            }
        }

        if (collider.gameObject.tag == "Bullet")
        {
            if (PlayerID != collider.gameObject.GetComponent<Collision>().PlayerID)
            {
                BulletData mbd = gameObject.GetComponent<BulletData>();
                BulletData bd = collider.gameObject.GetComponent<BulletData>();
                if (mbd.m_Type == BulletType.Sword_1)
                {
                    collider.gameObject.GetComponent<Collision>().PlayerID = PlayerID;
                    bd.BulletSpeed *= 2;
                    bd.SetTarget(bd.m_ShootPosition);
                }
                else
                {
                    ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                    sbc.m_BulletsList.Remove(this.gameObject);
                    GameObject.Destroy(this.gameObject);
                    sbc.m_BulletsList.Remove(collider.gameObject);
                    GameObject.Destroy(collider.gameObject);
                }
            }
        }

        if (collider.gameObject.tag == "Enemy")
        {
            BulletData bd = gameObject.GetComponent<BulletData>();
            DriveOff(gameObject, collider.gameObject, bd.m_Type);

            //�e�̍폜
            ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
            sbc.m_BulletsList.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);

            //�G�̍폜
            sbc.m_BulletsList.Remove(collider.gameObject);
            GameObject.Destroy(collider.gameObject);

            //���X�g����G�̍폜
            em.DestroyEnemy(collider.gameObject);

            //���삵�Ă���v���C���[���|�������ǂ���
            if (PlayerID == PlayerCtr.ControlPlayerID)
            {
                lm.AddDefeat();
            }
        }
    }

    void DriveOff(GameObject obj,GameObject Targetobj, BulletType _Type)
    {
        BulletData bd = obj.GetComponent<BulletData>();

        float angle = Random.Range(bd.DriveOffAngleMin, bd.DriveOffAngleMax)* Mathf.Deg2Rad;

       if(_Type == BulletType.Sword)
        {
            Vector2 dir = new Vector2();
            if(bd.m_Dir.x > 0)
            {
                dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }

            if(bd.m_Dir.x < 0)
            {
                dir = new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle));
            }
            Debug.Log(dir);
            Targetobj.GetComponent<Rigidbody2D>().AddForce(bd.DriveOffFactor * dir);
        }
    }
}

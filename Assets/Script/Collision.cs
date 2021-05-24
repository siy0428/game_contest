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

    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();
        cp2 = new ContactPoint2D[1];
    }

    private void Update()
    {      
        em = FindObjectOfType<EnemyManager>();
        lm = FindObjectOfType<LoopManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetContacts(cp2);

        if (collider.gameObject.tag == "Player")
        {
            if (PlayerID != collider.gameObject.GetComponent<Player>().PlayerID)    //自分自身に当たらない処理
            {
                ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                sbc.m_BulletsList.Remove(this.gameObject);
                GameObject.Destroy(this.gameObject);
                collider.gameObject.SetActive(false);
                if (PlayerID != PlayerCtr.ControlPlayerID)
                {
                    PlayerCtr.PlayersData[collider.gameObject.GetComponent<Player>().PlayerID].IsAlive = false;
                    collider.gameObject.SetActive(false);
                    lm.LoopAgain(); //同じループの生成
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
            //弾の削除
            ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
            sbc.m_BulletsList.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);

            //敵の削除
            sbc.m_BulletsList.Remove(collider.gameObject);
            GameObject.Destroy(collider.gameObject);

            //リストから敵の削除
            em.DestroyEnemy(collider.gameObject);

            //操作しているプレイヤーが倒したかどうか
            if (PlayerID == PlayerCtr.ControlPlayerID)
            {
                lm.AddDefeat();
            }
        }
    }
}

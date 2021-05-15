using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public int PlayerID;
    private PlayerController PlayerCtr;
    private EnemyManager em;
    private LoopManager lm;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();
        em = FindObjectOfType<EnemyManager>();
        lm = FindObjectOfType<LoopManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
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
                //bd.
            }
            else
            {
                ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                sbc.m_BulletsList.Remove(this.gameObject);
                GameObject.Destroy(this.gameObject);
            }
        }

        if (collider.gameObject.tag == "Bullet")
        {
            if (PlayerID != collider.gameObject.GetComponent<Collision>().PlayerID)
            {
                ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                sbc.m_BulletsList.Remove(this.gameObject);
                GameObject.Destroy(this.gameObject);
                sbc.m_BulletsList.Remove(collider.gameObject);
                GameObject.Destroy(collider.gameObject);
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

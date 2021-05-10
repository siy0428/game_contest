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
            if (PlayerID != collider.gameObject.GetComponent<Player>().PlayerID)
            {
                ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                sbc.m_BulletsList.Remove(this.gameObject);
                GameObject.Destroy(this.gameObject);
                PlayerCtr.PlayersData[collider.gameObject.GetComponent<Player>().PlayerID].IsAlive = false;
                collider.gameObject.SetActive(false);
            }
        }

        if (collider.gameObject.tag == "Ground")
        {
            ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
            sbc.m_BulletsList.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);
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
            //íeÇÃçÌèú
            ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
            sbc.m_BulletsList.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);

            //ìGÇÃçÌèú
            sbc.m_BulletsList.Remove(collider.gameObject);
            GameObject.Destroy(collider.gameObject);

            //ÉäÉXÉgÇ©ÇÁìGÇÃçÌèú
            em.DestroyEnemy(collider.gameObject);

            //ëÄçÏÇµÇƒÇ¢ÇÈÉvÉåÉCÉÑÅ[Ç™ì|ÇµÇΩÇ©Ç«Ç§Ç©
            if (PlayerID == PlayerCtr.ControlPlayerID)
            {
                lm.AddDefeat();
            }
        }
    }
}

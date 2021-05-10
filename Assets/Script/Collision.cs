using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public int PlayerID;
    private PlayerController PlayerCtr;
    private EnemyCreate ec;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();
        ec = FindObjectOfType<EnemyCreate>();
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
            //�e�̍폜
            ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
            sbc.m_BulletsList.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);

            //�G�̍폜
            sbc.m_BulletsList.Remove(collider.gameObject);
            GameObject.Destroy(collider.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public int PlayerID;
    private PlayerController PlayerCtr;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(PlayerID != collider.gameObject.GetComponent<Player>().PlayerID)
            {              
                ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                sbc.m_BulletsList.Remove(this.gameObject);
                GameObject.Destroy(this.gameObject);
                PlayerCtr.PlayersData[collider.gameObject.GetComponent<Player>().PlayerID].IsAlive = false;
                collider.gameObject.SetActive(false);
            }
        }

        if(collider.gameObject.tag == "Ground")
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
            if(PlayerID !=collider.gameObject.GetComponent<Collision>().PlayerID)
            {
                ShootBottonCtr sbc = FindObjectOfType<ShootBottonCtr>();
                sbc.m_BulletsList.Remove(this.gameObject);
                GameObject.Destroy(this.gameObject);
                sbc.m_BulletsList.Remove(collider.gameObject);
                GameObject.Destroy(collider.gameObject);
            }
        }
    }
}

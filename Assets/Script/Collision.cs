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
    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();
        cp2 = new ContactPoint2D[1];
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetContacts(cp2);

        if (collider.gameObject.tag == "Player")
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
                Vector3 od = bd.ReboundDir;
                Debug.Log(cp2[0].point);

                //if (cp2[0].normal.x == 1 || cp2[0].normal.x == -1)
                //if(Mathf.Abs(od.y/od.x) <0.15f)
                if(cp2[0].normalImpulse==0)
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

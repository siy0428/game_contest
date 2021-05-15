using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
     Default,
     Arrow,
     Rebound
}


public class BulletData : MonoBehaviour
{
    public BulletType m_Type;

    public Vector3 m_Target;

    public Vector3 m_Dir;

    public bool EnableRebound = false;

    public bool Aiming = false;

    //バレットのスピード
    public float BulletSpeed = 150.0f;

    public float BulletGra = -10.0f;

    public float Timer = 0.0f;

    public float LiveTime = 5.0f;

    public float ReboundRate = 0.7f;

    public float ReboundTimes = 0;

    public float ReboundedTimes = 0;

    public Vector3 ReboundDir = new Vector3(0,0,-1);

    public float m_Attack = 5.0f;

    public float m_ArrowFator = 40.0f;

    private float m_TargetAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer >= LiveTime)
        {
            GameObject.Destroy(gameObject);
        }
        Move();
        Timer += Time.deltaTime;
    }

    public void Move()
    {
        m_Dir = m_Target - transform.position;
        m_Dir.Normalize();

        switch (m_Type)
        {
            case BulletType.Default:
                DefaultMove();
                break;
            case BulletType.Arrow:
                ArrowMove();
                //DefaultMove();
                break;
            case BulletType.Rebound:
                ReboundMove();
                break;
            default:
                break;
        }
    }

    private void DefaultMove()
    {
        transform.position += m_Dir * BulletSpeed * Time.deltaTime;
    }

    private void ArrowMove()
    {
        //目標の偏移角度を求める
        m_TargetAngle = 360.0f - Mathf.Atan2(m_Dir.x, m_Dir.y) * Mathf.Rad2Deg;

        //向きにより、計算式を差別化
        if(m_Dir.x > 0)
        {           
            transform.eulerAngles = new Vector3(0, 0, 90 + m_TargetAngle);

            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, m_ArrowFator);

            transform.Translate(Vector3.right * Time.deltaTime * BulletSpeed);
        }
        else if(m_Dir.x <0)
        {
            transform.eulerAngles = new Vector3(0, 0, m_TargetAngle);

            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, m_ArrowFator);

            transform.Translate(Vector3.right * Time.deltaTime * BulletSpeed);
        }
    }

    private void ReboundMove()
    {
        if(ReboundDir == new Vector3(0,0,-1))
        {
            ReboundDir = m_Dir;
            transform.GetComponent<Rigidbody2D>().velocity = ReboundDir * BulletSpeed;
        }
        else
        {
            Vector2 v = transform.GetComponent<Rigidbody2D>().velocity;
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(v.x, v.y + BulletGra * Time.deltaTime);
        }
        //transform.Translate(ReboundDir * BulletSpeed * Mathf.Pow(ReboundRate, ReboundedTimes) * Time.deltaTime);
        //transform.Translate(new Vector3(0, BulletGra*Time.deltaTime, 0));
        //transform.GetComponent<Rigidbody2D>().velocity = ReboundDir * BulletSpeed * Mathf.Pow(ReboundRate, ReboundedTimes);
    }

    public void SetTarget(Vector3 _Target)
    {
        m_Target = _Target;
    }

    public bool CheakRebound()
    {
        if(EnableRebound)
        {
            if (ReboundedTimes < ReboundTimes)
            {
                return true;
            }
        }

        return false;
    }
}

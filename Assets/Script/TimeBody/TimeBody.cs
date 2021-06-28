using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    [SerializeField]
    private ShootKeeper sk;

    //逆再生用変数
    private bool isRewinding;
    private Rigidbody2D rb2d;
    private List<TimeBodyObject> objects;
    private TimeBodyManager tbm;
    private TimeBodyBulletManager tbbm;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        isRewinding = false;
        objects = new List<TimeBodyObject>();
        rb2d = GetComponent<Rigidbody2D>();
        tbm = FindObjectOfType<TimeBodyManager>();
        tbbm = FindObjectOfType<TimeBodyBulletManager>();
    }

    void Update()
    {
        //逆再生の開始
        if (tbm.GetIsUse())
        {
            StartRewind();
        }
        //逆再生の停止
        else
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        //逆再生
        if (isRewinding)
        {
            Rewind();
        }
        //記録
        else
        {
            Record();
        }
    }

    /// <summary>
    /// 逆再生
    /// </summary>
    void Rewind()
    {
        if (objects.Count > 0)
        {
            //リストの先頭から座標を参照
            transform.position = objects[0].position;
            //逆再生の速度調整
            for (int i = 0; i < tbm.GetMagniflication(); i++)
            {
                //リストの中身が空っぽになったら処理をしない
                if (objects.Count <= 0)
                {
                    break;
                }
                objects.RemoveAt(0);  //座標リストの先頭を削除
            }
        }
        else
        {
            //Debug.Log(gameObject.name + "の逆再生終了");
            StopRewind();           //逆再生の記録を停止
            tbm.SetIsUse(false);    //逆再生を停止
            objects.Clear();        //記録した座標を消去
        }
    }

    /// <summary>
    /// 座標の記録
    /// </summary>
    void Record()
    {
        TimeBodyObject obj = new TimeBodyObject();
        obj.position = transform.position;
        if (sk)
        {
            obj.isShot = sk.GetShotPerFrame();
        }

        //リストの先頭に座標を記録
        objects.Insert(0, obj);
    }

    /// <summary>
    /// 逆再生の開始
    /// </summary>
    public void StartRewind()
    {
        isRewinding = true;
        rb2d.isKinematic = true;
    }

    /// <summary>
    /// 逆再生の停止
    /// </summary>
    public void StopRewind()
    {
        isRewinding = false;
        rb2d.isKinematic = false;
    }
}

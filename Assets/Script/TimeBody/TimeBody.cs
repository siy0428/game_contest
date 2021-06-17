using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    //逆再生用変数
    private bool isRewinding;
    private Rigidbody2D rb2d;
    private List<Vector3> positions;
    private TimeBodyManager tbm;

    // Start is called before the first frame update
    void Start()
    {
        isRewinding = false;
        positions = new List<Vector3>();
        rb2d = GetComponent<Rigidbody2D>();
        tbm = FindObjectOfType<TimeBodyManager>();
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
        if (positions.Count > 0)
        {
            //リストの先頭から座標を参照
            transform.position = positions[0];
            //逆再生の速度調整
            for (int i = 0; i < tbm.GetMagniflication(); i++)
            {
                //リストの中身が空っぽになったら処理をしない
                if (positions.Count <= 0)
                {
                    break;
                }
                positions.RemoveAt(0);  //座標リストの先頭を削除
            }
        }
        else
        {
            StopRewind();
            tbm.SetIsUse(false);
            positions.Clear();
        }
    }

    /// <summary>
    /// 座標の記録
    /// </summary>
    void Record()
    {
        //リストの先頭に座標を記録
        positions.Insert(0, transform.position);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBodyBulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float first_time;
    private float real_time;
    private TimeBodyManager tbm;

    // Start is called before the first frame update
    void Start()
    {
        first_time = Time.realtimeSinceStartup;
        real_time = Time.realtimeSinceStartup;
        tbm = FindObjectOfType<TimeBodyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //逆再生していない間差分の取得
        if (!tbm.GetIsUse())
        {
            float time = real_time - first_time;
        }

        //逆再生中の弾の生成
        else
        {

        }
    }

    /// <summary>
    /// 現在時刻の代入
    /// </summary>
    public void SetFirstTime()
    {
        first_time = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// 指定した時間の代入
    /// </summary>
    /// <param name="time">指定した時刻</param>
    public void SetFirstTime(float time)
    {
        first_time = time;
    }

    /// <summary>
    /// 現在時刻の代入
    /// </summary>
    public void SetRealTime()
    {
        real_time = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// 指定した時間の代入
    /// </summary>
    /// <param name="time">指定した時刻</param>
    public void SetRealTime(float time)
    {
        real_time = time;
    }
}

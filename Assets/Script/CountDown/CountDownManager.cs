using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    private float m_Time;
    private bool m_Start;

    public float GetTime { get { return m_Time; } }
    public void CountStart() { m_Start = true; }    //時間計測開始
    public void CountStop() { m_Start = false; }    //時間計測停止
    public void CountReset() { m_Time = 0.0f; }     //時間リセット

    // Start is called before the first frame update
    void Start()
    {
        m_Time = 0.0f;
        m_Start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Start)
        {
            m_Time += Time.deltaTime;
        }
    }
}

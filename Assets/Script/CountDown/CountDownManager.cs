using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    private float m_Time;
    private bool m_Start;

    public float GetTime { get { return m_Time; } }
    public void CountStart() { m_Start = true; }    //���Ԍv���J�n
    public void CountStop() { m_Start = false; }    //���Ԍv����~
    public void CountReset() { m_Time = 0.0f; }     //���ԃ��Z�b�g

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

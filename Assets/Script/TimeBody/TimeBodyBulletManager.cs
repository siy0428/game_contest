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
        //�t�Đ����Ă��Ȃ��ԍ����̎擾
        if (!tbm.GetIsUse())
        {
            float time = real_time - first_time;
            Debug.Log("��������:" + time);
        }

        //�t�Đ����̒e�̐���
        else
        {

        }
    }

    /// <summary>
    /// ���ݎ����̑��
    /// </summary>
    public void SetFirstTime()
    {
        first_time = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// �w�肵�����Ԃ̑��
    /// </summary>
    /// <param name="time">�w�肵������</param>
    public void SetFirstTime(float time)
    {
        first_time = time;
    }

    /// <summary>
    /// ���ݎ����̑��
    /// </summary>
    public void SetRealTime()
    {
        real_time = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// �w�肵�����Ԃ̑��
    /// </summary>
    /// <param name="time">�w�肵������</param>
    public void SetRealTime(float time)
    {
        real_time = time;
    }
}

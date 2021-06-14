using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    //�t�Đ��p�ϐ�
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
        //�t�Đ��̊J�n
        if (tbm.GetIsUse())
        {
            StartRewind();
        }
        //�t�Đ��̒�~
        else
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        //�t�Đ�
        if (isRewinding)
        {
            Rewind();
        }
        //�L�^
        else
        {
            Record();
        }
    }

    /// <summary>
    /// �t�Đ�
    /// </summary>
    void Rewind()
    {
        if (positions.Count > 0)
        {
            //���X�g�̐擪������W���Q��
            transform.position = positions[0];
            //�t�Đ��̑��x����
            for (int i = 0; i < tbm.GetMagniflication(); i++)
            {
                //���X�g�̒��g������ۂɂȂ����珈�������Ȃ�
                if (positions.Count <= 0)
                {
                    break;
                }
                positions.RemoveAt(0);  //���W���X�g�̐擪���폜
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
    /// ���W�̋L�^
    /// </summary>
    void Record()
    {
        //���X�g�̐擪�ɍ��W���L�^
        positions.Insert(0, transform.position);
    }


    /// <summary>
    /// �t�Đ��̊J�n
    /// </summary>
    public void StartRewind()
    {
        isRewinding = true;
        rb2d.isKinematic = true;
    }

    /// <summary>
    /// �t�Đ��̒�~
    /// </summary>
    public void StopRewind()
    {
        isRewinding = false;
        rb2d.isKinematic = false;
    }
}

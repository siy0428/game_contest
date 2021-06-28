using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    [SerializeField]
    private ShootKeeper sk;

    //�t�Đ��p�ϐ�
    private bool isRewinding;
    private Rigidbody2D rb2d;
    private List<TimeBodyObject> objects;
    private TimeBodyManager tbm;
    private TimeBodyBulletManager tbbm;

    // Start is called before the first frame update
    void Start()
    {
        //������
        isRewinding = false;
        objects = new List<TimeBodyObject>();
        rb2d = GetComponent<Rigidbody2D>();
        tbm = FindObjectOfType<TimeBodyManager>();
        tbbm = FindObjectOfType<TimeBodyBulletManager>();
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
        if (objects.Count > 0)
        {
            //���X�g�̐擪������W���Q��
            transform.position = objects[0].position;
            //�t�Đ��̑��x����
            for (int i = 0; i < tbm.GetMagniflication(); i++)
            {
                //���X�g�̒��g������ۂɂȂ����珈�������Ȃ�
                if (objects.Count <= 0)
                {
                    break;
                }
                objects.RemoveAt(0);  //���W���X�g�̐擪���폜
            }
        }
        else
        {
            //Debug.Log(gameObject.name + "�̋t�Đ��I��");
            StopRewind();           //�t�Đ��̋L�^���~
            tbm.SetIsUse(false);    //�t�Đ����~
            objects.Clear();        //�L�^�������W������
        }
    }

    /// <summary>
    /// ���W�̋L�^
    /// </summary>
    void Record()
    {
        TimeBodyObject obj = new TimeBodyObject();
        obj.position = transform.position;
        if (sk)
        {
            obj.isShot = sk.GetShotPerFrame();
        }

        //���X�g�̐擪�ɍ��W���L�^
        objects.Insert(0, obj);
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

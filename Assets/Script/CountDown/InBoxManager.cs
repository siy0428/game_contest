using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] InBoxs;
    [SerializeField]
    private GameObject RangeBox;
    [SerializeField]
    private CountDownManager Timer;

    private Color DefaultColor;
    private float Alpha;
    private bool DecAlpha;

    void Start()
    {
        //������
        DefaultColor = RangeBox.GetComponent<Renderer>().material.GetColor("_Color");
        Alpha = 0.5f;
        DecAlpha = false;

        Create();
        Timer.CountStart();
    }

    void Update()
    {
        //�s���͈͂̓����x����
        RangeBox.GetComponent<Renderer>().material.SetColor("_Color", DefaultColor);
        DefaultColor.a = Alpha;
        
        //��莞�Ԍo�ߏ���
        TimeUp(3.0f);

        //��莞�Ԍo�ߌ�s���͈͂����X�ɓ�����
        if(DecAlpha)
        {
            Alpha -= 0.01f;
            if (Alpha <= 0.0f)
            {
                DecAlpha = false;
            }
        }
    }

    /// <summary>
    /// �����ʒu�����蔻�萶��
    /// </summary>
    public void Create()
    {
        foreach(GameObject box in InBoxs)
        {
            box.SetActive(true);
        }
        DecAlpha = false;
    }

    /// <summary>
    /// �����ʒu�����蔻��폜
    /// </summary>
    public void Delete()
    {
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
        DecAlpha = true;
    }

    /// <summary>
    /// ��莞�Ԍo�ߏ���
    /// </summary>
    /// <param name="limit">���b�Ԍ�Ɏ��s���邩�w��</param>
    private void TimeUp(float limit)
    {
        if (Timer.GetTime > limit)
        {
            Timer.CountStop();
            Timer.CountReset();
            Delete();
        }
    }
}

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
    private CountDownManager cdm;
    
    private InBoxCreate ibc;
    private PlayerController pc;
    private Color DefaultColor;
    private float Alpha;
    private bool DecAlpha;

    public float GetAlpha { get { return Alpha; } }

    void Start()
    {
        //������
        DefaultColor = RangeBox.GetComponent<Renderer>().material.GetColor("_Color");
        Alpha = 0.5f;
        DecAlpha = false;
        ibc = FindObjectOfType<InBoxCreate>();
        pc = FindObjectOfType<PlayerController>();

        cdm.CountStart();
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
            //�s���͈͐����I������
            if (Alpha <= 0.0f)
            {
                DecAlpha = false;
                ibc.SetIsCraete(false);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// �����ʒu�����蔻��폜
    /// </summary>
    public void Delete()
    {
        //�{�b�N�X�̓����蔻�肾���폜
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
        //�J�E���g�_�E�������X�ɓ�����
        DecAlpha = true;
        //�v���C���[�������ʒu��
        int id = pc.ControlPlayerID;
        pc.PlayersData[id].RespawnPosition();
    }

    /// <summary>
    /// ��莞�Ԍo�ߏ���
    /// </summary>
    /// <param name="limit">���b�Ԍ�Ɏ��s���邩�w��</param>
    private void TimeUp(float limit)
    {
        if (cdm.GetTime > limit)
        {
            cdm.CountStop();
            cdm.CountReset();
            Delete();
        }
    }
}

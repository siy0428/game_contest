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
    private bool IsDecAlpha;

    public float Alpha { get; private set; }
    public void SetAlpha(float a) { Alpha = a; }

    void Start()
    {
        //������
        DefaultColor = RangeBox.GetComponent<Renderer>().material.GetColor("_Color");
        IsDecAlpha = false;
        ibc = FindObjectOfType<InBoxCreate>();
        pc = FindObjectOfType<PlayerController>();

        cdm.CountStart();
    }

    void Update()
    {
        //�s���͈͂̓����x����
        DefaultColor.a = Alpha;
        RangeBox.GetComponent<Renderer>().material.SetColor("_Color", DefaultColor);

        //��莞�Ԍo�ߏ���
        TimeUp(3.0f);

        //��莞�Ԍo�ߌ�s���͈͂����X�ɓ�����
        if (IsDecAlpha)
        {
            Alpha -= 0.01f;
            //�s���͈͐����I������
            if (Alpha <= 0.0f)
            {
                IsDecAlpha = false;
                ibc.SetIsCraete(false);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// �����ʒu�����蔻��폜
    /// </summary>
    private void Finish()
    {
        //�{�b�N�X�̓����蔻�肾���폜
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
        //�J�E���g�_�E�������X�ɓ�����
        IsDecAlpha = true;
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
            Finish();
        }
    }

    /// <summary>
    /// �s�������̍폜
    /// </summary>
    public void Delete()
    {
        cdm.CountStop();
        cdm.CountReset();
        Finish();
        Alpha = 0.0f;
        IsDecAlpha = false;
        ibc.SetIsCraete(false);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Process : MonoBehaviour
{
    private Image img;
    private LoopManager lm;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LoopManager>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ////�����ԕ\��
        img.fillAmount = lm.GetNowTime() / lm.GetTimeLimit() * 0.5f;
        //if (lm.GetNowTime() > 0.0f)
        //{
        //    textComponent.text = "��������" + lm.GetNowTime().ToString("N2");
        //}

        //���i�s�x�\��
        //textComponent.text += "\n" + "�v���Z�X" + lm.GetNowDefeatCount() + "/" + lm.GetFinishDefeatCount();

    }
}

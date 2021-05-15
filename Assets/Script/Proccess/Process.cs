using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Process : MonoBehaviour
{
    private Image img;
    private Text textComponent;
    private LoopManager lm;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GameObject.Find("defeat").GetComponent<Text>();
        lm = FindObjectOfType<LoopManager>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ////���~�`���ԕ\��
        img.fillAmount = lm.GetNowTime() / lm.GetTimeLimit() * 0.5f;

        //���i�s�x�\��
        float parcent = lm.GetNowDefeatCount() / lm.GetFinishDefeatCount() * 100.0f;
        parcent = Mathf.Clamp(parcent, 0.0f, 100.0f);
        textComponent.text = "\n" + "�v���Z�X\n" + parcent + "%";

    }
}

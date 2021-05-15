using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessGauge : MonoBehaviour
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
        ////”¼‰~Œ`ŽžŠÔ•\Ž¦
        img.fillAmount = lm.GetNowTime() / lm.GetTimeLimit() * 0.25f;
    }
}

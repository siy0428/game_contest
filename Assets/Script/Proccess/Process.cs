using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Process : MonoBehaviour
{
    private Text textComponent;
    private LoopManager lm;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GameObject.Find("TestProccessTime").GetComponent<Text>();
        lm = FindObjectOfType<LoopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //仮時間表示
        if (lm.GetNowTime() > 0.0f)
        {
            textComponent.text = "制限時間" + lm.GetNowTime().ToString();
        }
    }
}

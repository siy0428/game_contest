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
        //âºéûä‘ï\é¶
        if (lm.GetNowTime() > 0.0f)
        {
            textComponent.text = "êßå¿éûä‘" + lm.GetNowTime().ToString();
        }
    }
}

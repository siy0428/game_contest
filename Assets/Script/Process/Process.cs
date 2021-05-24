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
        textComponent = GetComponent<Text>();
        lm = FindObjectOfType<LoopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //進行度表示
        float parcent = (float)(lm.GetLoopId()) / lm.GetLoopTotalCount() * 100.0f;
        parcent = Mathf.Clamp(parcent, 0.0f, 100.0f);
        textComponent.text = parcent.ToString() + "%";

    }
}

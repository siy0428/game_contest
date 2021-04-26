using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] InBoxs;
    [SerializeField]
    private CountDownManager Timer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ‰ŠúˆÊ’u“–‚½‚è”»’è¶¬
    /// </summary>
    public void Create()
    {
        foreach(GameObject box in InBoxs)
        {
            box.SetActive(true);
        }
    }

    /// <summary>
    /// ‰ŠúˆÊ’u“–‚½‚è”»’èíœ
    /// </summary>
    public void Delete()
    {
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
    }
}

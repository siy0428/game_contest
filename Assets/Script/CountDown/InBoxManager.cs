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
    /// 初期位置当たり判定生成
    /// </summary>
    public void Create()
    {
        foreach(GameObject box in InBoxs)
        {
            box.SetActive(true);
        }
    }

    /// <summary>
    /// 初期位置当たり判定削除
    /// </summary>
    public void Delete()
    {
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
    }
}

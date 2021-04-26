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
    /// �����ʒu�����蔻�萶��
    /// </summary>
    public void Create()
    {
        foreach(GameObject box in InBoxs)
        {
            box.SetActive(true);
        }
    }

    /// <summary>
    /// �����ʒu�����蔻��폜
    /// </summary>
    public void Delete()
    {
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
    }
}

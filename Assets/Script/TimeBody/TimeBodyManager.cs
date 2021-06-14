using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeBodyManager : MonoBehaviour
{
    [SerializeField]
    private int magnification;

    private bool is_use;

    // Start is called before the first frame update
    void Start()
    {
        is_use = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMagniflication()
    {
        return magnification;
    }

    public void SetIsUse(bool use)
    {
        is_use = use;
    }

    public bool GetIsUse()
    {
        return is_use;
    }
}

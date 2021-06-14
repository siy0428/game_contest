using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeBodyManager : MonoBehaviour
{
    private bool IsUse;

    // Start is called before the first frame update
    void Start()
    {
        IsUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsUse(bool use)
    {
        IsUse = use;
    }

    public bool GetIsUse()
    {
        return IsUse;
    }
}

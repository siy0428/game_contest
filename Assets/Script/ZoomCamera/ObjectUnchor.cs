using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectUnchor : MonoBehaviour
{
    protected PlayerController pc;

    protected void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxCreate : MonoBehaviour
{
    [SerializeField]
    GameObject InBox;

    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create()
    {
        Instantiate(InBox, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
    }
}

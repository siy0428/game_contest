using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{

    public bool DoDestroy = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DoDestroy)
        {
            Destroy(gameObject, 1.0f);
            if (gameObject.GetComponentInParent<DestroyTrigger>() != null)
            {
                gameObject.GetComponentInParent<DestroyTrigger>().DoDestroy = true;
            }
        }
    }
}

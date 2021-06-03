using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_contoroller : MonoBehaviour
{
    float screen_width = 640.0f;
    float screen_height = 320.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if( transform.position.x > screen_width || transform.position.x < -screen_width ||
            transform.position.y > screen_height || transform.position.y < -screen_height)
        {
            Destroy(gameObject);
        }
    }
}

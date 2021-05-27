using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool isActive;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        if (Time.frameCount % 500 == 0)
        {
            rb2d.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        }
    }
}

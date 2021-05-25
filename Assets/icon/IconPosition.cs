using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPosition : MonoBehaviour
{
    public GameObject player;// インスペクターオブジェクトを指定

    public float height;
    Vector3 playerposition;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        playerposition = player.transform.position;
        position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerposition = player.transform.position;

        position.x = playerposition.x;
        position.y = playerposition.y + height;

        this.transform.position = position;
    }
}

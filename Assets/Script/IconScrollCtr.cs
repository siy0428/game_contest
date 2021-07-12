using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScrollCtr : MonoBehaviour
{
    public GameObject[] backImage = new GameObject[4];
    Vector3[] startPos = new Vector3[4];
    public float limit;
    public float scrollingSpeed;
    public Vector3 scrollingDir;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            startPos[i] = backImage[i].GetComponent<Transform>().localPosition;
        }
    }
    void Update()
    {
        Scrolling();
    }

    void Scrolling()
    {
        float scroll = Mathf.Repeat(Time.time * scrollingSpeed, limit);
        for (int i = 0; i < 4; i++)
        {
            backImage[i].GetComponent<Transform>().localPosition = startPos[i] + scrollingDir * scroll;
        }
    }

}

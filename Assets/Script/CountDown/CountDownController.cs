using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownController : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites; 
    [SerializeField]
    InBoxManager inbox;
    [SerializeField]
    int seconds;  

    private SpriteRenderer myRenderer;  
    private float count; 
    private int spriteCount;  
    private int nowSprite;  
    [System.NonSerialized] public bool active;

    void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        spriteCount = sprites.Length;
        nowSprite = 0;
        active = true;
        count = 0.0f;
        myRenderer.sprite = sprites[nowSprite];
    }

    void Update()
    {
        if (active)
        {
            count += Time.deltaTime;
            if (count >= seconds)
            {
                count = 0.0f;
                NextSprite();
            }
            float a = inbox.Alpha;
            myRenderer.color = new Color(1.0f, 1.0f, 1.0f, a);
        }
    }

    void NextSprite()
    {
        if (nowSprite == spriteCount - 1)
        {
            active = false;
        }
        else
        {
            nowSprite = (nowSprite + 1) % spriteCount;
            myRenderer.sprite = sprites[nowSprite];
        }
    }
}
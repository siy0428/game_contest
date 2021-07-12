using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//---------------------------------
//テキストを点滅させるスクリプト
//---------------------------------

public class TextBlinking : MonoBehaviour
{

    public float speed = 1.0f;
    private Text text;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        //テキストコンポーネントを取得
        text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //アルファの値を変更することで点滅
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {

        time += Time.deltaTime * 5.0f * speed;
        //周期的な動きにはSin関数
        color.a = Mathf.Sin(time);

        return color;
    }

}

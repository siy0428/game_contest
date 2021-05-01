using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] InBoxs;
    [SerializeField]
    private GameObject RangeBox;
    [SerializeField]
    private CountDownManager Timer;

    private Color DefaultColor;
    private float Alpha;
    private bool DecAlpha;

    void Start()
    {
        //初期化
        DefaultColor = RangeBox.GetComponent<Renderer>().material.GetColor("_Color");
        Alpha = 0.5f;
        DecAlpha = false;

        Create();
        Timer.CountStart();
    }

    void Update()
    {
        //行動範囲の透明度調整
        RangeBox.GetComponent<Renderer>().material.SetColor("_Color", DefaultColor);
        DefaultColor.a = Alpha;
        
        //一定時間経過処理
        TimeUp(3.0f);

        //一定時間経過後行動範囲を徐々に透明化
        if(DecAlpha)
        {
            Alpha -= 0.01f;
            if (Alpha <= 0.0f)
            {
                DecAlpha = false;
            }
        }
    }

    /// <summary>
    /// 初期位置当たり判定生成
    /// </summary>
    public void Create()
    {
        foreach(GameObject box in InBoxs)
        {
            box.SetActive(true);
        }
        DecAlpha = false;
    }

    /// <summary>
    /// 初期位置当たり判定削除
    /// </summary>
    public void Delete()
    {
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
        DecAlpha = true;
    }

    /// <summary>
    /// 一定時間経過処理
    /// </summary>
    /// <param name="limit">何秒間後に実行するか指定</param>
    private void TimeUp(float limit)
    {
        if (Timer.GetTime > limit)
        {
            Timer.CountStop();
            Timer.CountReset();
            Delete();
        }
    }
}

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
    private CountDownManager cdm;
    
    private InBoxCreate ibc;
    private PlayerController pc;
    private Color DefaultColor;
    private float Alpha;
    private bool DecAlpha;

    public float GetAlpha { get { return Alpha; } }

    void Start()
    {
        //初期化
        DefaultColor = RangeBox.GetComponent<Renderer>().material.GetColor("_Color");
        Alpha = 0.5f;
        DecAlpha = false;
        ibc = FindObjectOfType<InBoxCreate>();
        pc = FindObjectOfType<PlayerController>();

        cdm.CountStart();
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
            //行動範囲制限終了処理
            if (Alpha <= 0.0f)
            {
                DecAlpha = false;
                ibc.SetIsCraete(false);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 初期位置当たり判定削除
    /// </summary>
    public void Delete()
    {
        //ボックスの当たり判定だけ削除
        foreach (GameObject box in InBoxs)
        {
            box.SetActive(false);
        }
        //カウントダウンを徐々に透明化
        DecAlpha = true;
        //プレイヤーを初期位置に
        int id = pc.ControlPlayerID;
        pc.PlayersData[id].RespawnPosition();
    }

    /// <summary>
    /// 一定時間経過処理
    /// </summary>
    /// <param name="limit">何秒間後に実行するか指定</param>
    private void TimeUp(float limit)
    {
        if (cdm.GetTime > limit)
        {
            cdm.CountStop();
            cdm.CountReset();
            Delete();
        }
    }
}

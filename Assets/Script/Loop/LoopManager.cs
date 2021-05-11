using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField]
    private Loop[] loops;

    private int loop_id;        //現在のループの段階
    private int defeat_player;  //操作しているプレイヤーが倒した敵の数
    private float time;         //ループごとの時間制限
    private EnemyManager em;

    // Start is called before the first frame update
    void Start()
    {
        loop_id = 0;
        defeat_player = 0;
        loops[loop_id].Create();            //1つ目のループ生成
        time = loops[loop_id].GetTime();    //1つ目のループの時間取得
    }

    // Update is called once per frame
    void Update()
    {
        //制限時間後の判定
        Finish();

        time -= Time.deltaTime;
    }

    /// <summary>
    /// 制限時間後の判定
    /// </summary>
    public void Finish()
    {
        //タイムオーバーであれば
        if (time <= 0.0f)
        {
            //目標撃破数に到達していた場合次のループ
            if (defeat_player >= loops[loop_id].GetDefeatCount())
            {
                loop_id++;
                defeat_player = 0;
                loops[loop_id].Create();            //次のループ生成
                time = loops[loop_id].GetTime();    //次のループの時間取得
            }

            //撃破目標に到達出来なかった場合同じループ
            else
            {
                defeat_player = 0;
                loops[loop_id].Create();            //同じループの生成
                time = loops[loop_id].GetTime();    //同じループの時間取得
            }
        }
    }

    /// <summary>
    /// 撃破カウントプラス
    /// </summary>
    public void AddDefeat()
    {
        defeat_player++;
    }

    /// <summary>
    /// 現在のループの制限時間取得
    /// </summary>
    /// <returns></returns>
    public float GetLimitTime()
    {
        return loops[loop_id].GetTime();
    }

    /// <summary>
    /// 現在の経過時間取得
    /// </summary>
    /// <returns></returns>
    public float GetNowTime()
    {
        return time;
    }
}

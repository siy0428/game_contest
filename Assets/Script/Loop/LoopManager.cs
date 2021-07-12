using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField]
    private Loop[] loops;

    private bool isRewindStart; //逆再生を開始したかどうか
    private bool isAgain;
    private int loop_id;        //現在のループの段階
    private int defeat_player;  //操作しているプレイヤーが倒した敵の数
    private float time;         //ループごとの時間制限
    private PlayerController pc;
    private TimeBodyManager tbm;

    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        tbm = FindObjectOfType<TimeBodyManager>();
        isRewindStart = false;
        isAgain = false;
        loop_id = 0;
        defeat_player = 0;
        loops[loop_id].Create();            //1つ目のループ生成
        time = loops[loop_id].GetTime();    //1つ目のループの時間取得
    }

    // Update is called once per frame
    void Update()
    {
        //ボックス生成中は処理を行わない
        if (loops[loop_id].IsCreate())
        {
            return;
        }

        //制限時間後の判定
        if (Finish())
        {
            time -= Time.deltaTime;
        }

    }

    /// <summary>
    /// 時間経過後の処理
    /// </summary>
    /// <returns>クリアした場合trueが帰ってくる</returns>
    private bool Finish()
    {
        //タイムオーバーであれば
        if (time <= 0.0f)
        {
            //まだ逆再生を開始していない場合
            if (!isRewindStart)
            {
                tbm.SetIsUse(true);
                isRewindStart = true;
            }

            //逆再生中であれば次のループに移行しない
            if (tbm.GetIsUse())
            {
                return false;
            }

            //目標撃破数に到達していた場合次のループ
            //if (defeat_player >= loops[loop_id].GetDefeatCount())
            if (defeat_player >= loops[loop_id].GetDefeatCount())
            {
                loops[loop_id].AddPlayer();
                pc.ChangePlayer();                  //次のプレイヤーへ操作変更
                loop_id++;
                defeat_player = 0;
                loops[loop_id].Create();            //次のループ生成
                time = loops[loop_id].GetTime();    //次のループの時間取得
                Debug.Log("次のループ");
            }

            //撃破目標に到達出来なかった場合同じループ
            else
            {
                LoopAgain();
            }

            isRewindStart = false;

            return false;
        }

        //同じループの再生
        if (isAgain)
        {
            //まだ逆再生を開始していない場合
            if (!isRewindStart)
            {
                tbm.SetIsUse(true);
                isRewindStart = true;
            }

            //逆再生中であれば次のループに移行しない
            if (tbm.GetIsUse())
            {
                return false;
            }

            LoopAgain();

            isRewindStart = false;
            isAgain = false;

            return false;
        }

        return true;
    }

    public void SetIsAgain(bool again)
    {
        isAgain = again;
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
    public float GetTimeLimit()
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

    /// <summary>
    /// ループごとの目標撃破数の取得
    /// </summary>
    /// <returns></returns>
    public float GetFinishDefeatCount()
    {
        return loops[loop_id].GetDefeatCount();
    }

    /// <summary>
    /// 現在の撃破数取得
    /// </summary>
    /// <returns></returns>
    public float GetNowDefeatCount()
    {
        return defeat_player;
    }

    /// <summary>
    /// もう一度同じループ時の処理
    /// </summary>
    public void LoopAgain()
    {
        pc.PlayerWithoutLoop();             //同じプレイヤーの操作処理
        defeat_player = 0;
        loops[loop_id].Create();            //同じループの生成
        time = loops[loop_id].GetTime();    //同じループの時間取得  
    }

    /// <summary>
    /// ループ管理番号の取得
    /// </summary>
    /// <returns></returns>
    public int GetLoopId()
    {
        return loop_id;
    }

    /// <summary>
    /// ループの総数の取得
    /// </summary>
    /// <returns></returns>
    public int GetLoopTotalCount()
    {
        return loops.Length - 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField]
    private Loop[] loops;

    private int loop_id;        //現在のループの段階
    private int defeat_player;  //操作しているプレイヤーが倒した敵の数

    // Start is called before the first frame update
    void Start()
    {
        loop_id = 0;
        defeat_player = 0;
        loops[loop_id].Create();  //1つ目のループ生成
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 現在のループの目標撃破数に到達しているか
    /// </summary>
    public void Finish()
    {
        //目標撃破数に到達していた場合次のループ
        if (defeat_player >= loops[loop_id].GetDefeatCount())
        {
            loop_id++;
            defeat_player = 0;
            loops[loop_id].Create();    //次のループ生成
        }

        //撃破目標に到達出来なかった場合同じループ
        else
        {
            defeat_player = 0;
            loops[loop_id].Create();    //同じ部分
        }
    }

    public void AddDefeat()
    {
        defeat_player++;
    }
}

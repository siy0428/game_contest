using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの行動データ
public class PlayerBehaviourData
{
   //行動記録リスト
   private List<BehaviorData> Behaviour = new List<BehaviorData>();

    //初期化のため、コンストラクタが必要
    public PlayerBehaviourData()
    {

    }

    //コピーコンストラクタ(記録と保存データの更新用)
    public PlayerBehaviourData(PlayerBehaviourData _data)
    {
        List<BehaviorData> b_data = _data.GetBehaviourData();
        for (int i = 0; i< b_data.Count;i++)
        {
            Behaviour.Add(new BehaviorData(b_data[i]));
        }
    }

    //行動追加
    public void AddBehaviour(BehaviorData _Data)
    {
        Behaviour.Add(new BehaviorData(_Data));
    }

    //リストをクリアする
    public void ClearData()
    {
        Behaviour.Clear();
    }

    //リストのポインタを取得する
    public List<BehaviorData> GetBehaviourData()
    {
        return Behaviour;
    }
}

//一つの行動の構成要素
public class BehaviorData
{
    //プレイヤー番号
    public int PlayerID;

    //キー番号
    public int BottonID;
    
    //行動の時点
    public float StartTime;

    //シュートだけ使う。シュートの方向
    public Vector2 ShootDir;

    //初期化のため、コンストラクタが必要
    public BehaviorData()
    {
        BottonID = -1;
    }

    //コピーコンストラクタ
    public BehaviorData(BehaviorData _data)
    {
        PlayerID = _data.PlayerID;
        BottonID = _data.BottonID;
        StartTime = _data.StartTime;
        ShootDir = _data.ShootDir;
    }
}


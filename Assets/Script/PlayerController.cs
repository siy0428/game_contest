using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //!!!!　自殺するのは　U　キーで

    //プレイヤーリスト
    public List<GameObject> Players = new List<GameObject>();

    public List<Vector3> PlayersForward = new List<Vector3>();

    //今操作しているプレイヤー番号
    public int ControlPlayerID = 0;

    //行動記録モード
    public bool StartBehaviourRecord = true;

    //バックアップモード(今使っていない、ただの復元フラグとして、使ってる)
    public bool StartFallBack = false;

    //バックアップのスピード倍率(今使っていない）
    public float FallBackSpeed = 5.0f;

    //移動速度
    public float MoveSpeed = 5.0f;

    //移動のスクリプトポイント
    private MoveBottonCtr MoveCtr;

    //シュートのスクリプトポイント
    private ShootBottonCtr ShootCtr;

    //プレイヤークラス作っていないので、必要の機能をそのまま、リストにしちゃった
    //ジャンプ状態リスト
    public List<bool> IsJump = new List<bool>();

    //一回のループ内のタイマー
    public float Timer = 0.0f;

    //読み込み用データ
    public PlayerBehaviourData SavedBehaviour = new PlayerBehaviourData();

    //記録用データ
    public PlayerBehaviourData RecordBehaviour = new PlayerBehaviourData();

    //生存状態リスト
    public List<bool> IsAlive = new List<bool>();

    //バックアップ用初期位置リスト
    public List<Vector2> StartPositon = new List<Vector2>();

    //移動状態リスト
    public List<int> IsMove = new List<int>();

    //ジャンプに使うｙ方向の力
    public float JumpFocre = 30000.0f;

    //バレットのインスタンスサンプル
    public GameObject Bullet;

    //バレットのスピード
    public float BulletSpeed = 150.0f;

    private bool IsDead = false;

    // Start is called before the first frame update
    public void Start()
    {
        //スクリプト取得
        GetBottonCtr();
        
        //初期化
        for (int i = 0; i<Players.Count;i++)
        {
            IsJump.Add(false);
            IsAlive.Add(true);
            IsMove.Add(0);
            StartPositon.Add(Players[i].GetComponent<Transform>().position);
        }

        PlayersForward.Add(new Vector3(1.0f, 1.0f, 0.0f));
        PlayersForward.Add(new Vector3(-1.0f, 1.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {

        if(!StartFallBack)
        {
            //最初のループ内相手が動いていないので、下記の操作をジャンプする
            if (SavedBehaviour.GetBehaviourData().Count > 1)
            {
                //保存されたデータの取得操作を一回のみ発生する。
                var savedata = SavedBehaviour.GetBehaviourData();

                //保存されたデータの反応方法
                for (int i = 0; i < savedata.Count; i++)
                {
                    //記録時点になったら
                    if (Timer > savedata[i].StartTime)
                    {
                        //キーの番号ごとに、対応の状態を変更する
                        switch (savedata[i].BottonID)
                        {
                            case 0:
                                IsMove[savedata[i].PlayerID] = -1;
                                PlayersForward[i] = new Vector3(-1.0f, 1.0f, 0.0f);
                                break;
                            case 1:
                                IsMove[savedata[i].PlayerID] = 0;
                                break;
                            case 2:
                                IsMove[savedata[i].PlayerID] = 1;
                                PlayersForward[i] = new Vector3(1.0f, 1.0f, 0.0f);
                                break;
                            case 3:
                                IsMove[savedata[i].PlayerID] = 0;
                                break;
                            case 4:
                                IsJump[savedata[i].PlayerID] = true;
                                break;
                            case 5://シュートだけ、直接シュートする
                                ShootCtr.ShootKeyDown(this, savedata[i].PlayerID, savedata[i].ShootDir);
                                break;
                            default:
                                break;
                        }
                        //保存データの対応キー番号を-1で、反応完了データを無効化する
                        savedata[i].BottonID = -1;
                    }
                }
            }

            //全てのプレイヤーの動きを更新する
            for(int i =0; i<Players.Count;i++)
            {
                //非操作対象
                if(i!= ControlPlayerID)
                {
                    Vector2 pos = Players[i].GetComponent<Transform>().position;
                    Players[i].GetComponent<Transform>().position = new Vector2(pos.x + IsMove[i] * MoveSpeed * Time.deltaTime, pos.y);
                    Players[i].GetComponent<Transform>().localScale = PlayersForward[i];
                    if(IsJump[i])
                    {
                        Jump(i);
                    }

                }
                else//操作対象
                {
                    MoveCtr.MoveBottonUse(this);
                    ShootCtr.ShootKeyDown(this,i);
                    if (IsJump[i])
                    {
                        Jump(i);
                    }
                }
            }

            //次のループへのキー
            
            if (IsDead)
            {
                IsAlive[ControlPlayerID] = false;
                IsDead = false;
            }

            //タイマー累加
            Timer += Time.deltaTime;

            //ループ終了判定
            if (IsAlive[ControlPlayerID])
            {
                //相手全員死だ
                for (int i = 0; i < IsAlive.Count; i++)
                {
                    if (i != ControlPlayerID)
                    {
                        if (IsAlive[i])
                        {
                            return;
                        }
                    }
                }

                StartBehaviourRecord = false;
                StartFallBack = true;
            }
            else//自身が死んだ
            {
                StartBehaviourRecord = false;
                StartFallBack = true;
            }
        }
        else
        {
            //データを初期値に戻る
            Timer = 0.0f;
            for (int i = 0; i < Players.Count; i++)
            {
                IsJump[i] =false;
                IsAlive[i] = true;
                Players[i].GetComponent<Transform>().position =  StartPositon[i];
            }

            //操作対象変更
            ControlPlayerID++;
            ControlPlayerID %= Players.Count;

            StartFallBack = false;

            //保存データの更新
            SavedBehaviour = new PlayerBehaviourData(RecordBehaviour);

            //記録データの削除
            RecordBehaviour.ClearData();

            //新しい記録準備完了
            StartBehaviourRecord = true;
        }
    }

    //スクリプト取得
    void GetBottonCtr()
    {
        MoveCtr = FindObjectOfType<MoveBottonCtr>();       
        ShootCtr = FindObjectOfType<ShootBottonCtr>();
    }

    //ジャンプ操作
    void Jump(int ID)
    {
        Players[ID].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpFocre));
        IsJump[ID] = false;
    }

    public void ToNextIsDown(InputAction.CallbackContext obj)
    {
        IsDead = true;
    }

    public void SetScale(int ID,Vector3 _Scale)
    {
        PlayersForward[ID] = _Scale;
        Players[ID].GetComponent<Transform>().localScale = _Scale;
    }
}

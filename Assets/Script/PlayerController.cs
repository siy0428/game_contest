using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //!!!!　自殺するのは　U　キーで

    //プレイヤーリスト
    public List<GameObject> Players = new List<GameObject>();

    //今操作しているプレイヤー番号
    public int ControlPlayerID = 0;

    //行動記録モード
    public bool StartBehaviourRecord = true;

    //バックアップモード(今使っていない、ただの復元フラグとして、使ってる)
    public bool StartFallBack = false;

    //バックアップのスピード倍率(今使っていない）
    public float FallBackSpeed = 5.0f;

    //移動のスクリプトポイント
    private MoveBottonCtr MoveCtr;

    //シュートのスクリプトポイント
    private ShootBottonCtr ShootCtr;

    //スキルの設定データを取得用
    public SkillData SkillDataCtr;

    //一回のループ内のタイマー
    public float Timer = 0.0f;

    //読み込み用データ
    public PlayerBehaviourData SavedBehaviour = new PlayerBehaviourData();

    //記録用データ
    public PlayerBehaviourData RecordBehaviour = new PlayerBehaviourData();

    //ジャンプに使うｙ方向の力
    public float JumpFocre = 5500.0f;

    //操作しているキャラクターが死んだか
    private bool IsDead = false;

    //取得したキャラクターのデータ
    public List<Player> PlayersData = new List<Player>();

    // Start is called before the first frame update
    public void Start()
    {
        //スクリプト取得
        GetBottonCtr();
        SkillDataCtr = FindObjectOfType<SkillData>();
        //初期化
        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData.Add(Players[i].GetComponent<Player>());//キャラクターデータ取得
            PlayersData[i].PlayerID = i;
            //if (i == ControlPlayerID)
            //{
            //    Players[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            //}
            //else
            //{
            //    Players[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartFallBack)
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
                        if (!savedata[i].Used)
                        {
                            //キーの番号ごとに、対応の状態を変更する
                            switch (savedata[i].BottonID)
                            {
                                case 0:
                                    PlayersData[savedata[i].PlayerID].IsMove = -1;
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    break;
                                case 1:
                                    PlayersData[savedata[i].PlayerID].IsMove = 0;
                                    break;
                                case 2:
                                    PlayersData[savedata[i].PlayerID].IsMove = 1;
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                    break;
                                case 3:
                                    PlayersData[savedata[i].PlayerID].IsMove = 0;
                                    break;
                                case 4:
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    break;
                                case 5://シュートだけ、直接シュートする
                                    ShootCtr.ShootKeyDown(this, savedata[i].PlayerID, savedata[i].ShootDir);
                                    break;
                                case 41:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(1, 0);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                case 42:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(-1, 0);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                case 43:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(0, 1);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                case 44:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(0, -1);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                default:
                                    break;
                            }
                            savedata[i].Used = true;
                        }
                    }
                }
            }

            //全てのプレイヤーの動きを更新する
            for (int i = 0; i < Players.Count; i++)
            {
                if (PlayersData[i].IsAlive)
                {
                    //非操作対象
                    if (i != ControlPlayerID)
                    {
                        if (!CheakJumpSmarsh(i))
                        {
                            Vector2 pos = Players[i].GetComponent<Transform>().position;
                            Players[i].GetComponent<Transform>().position = new Vector2(pos.x + PlayersData[i].IsMove * PlayersData[i].MoveSpeed * Time.deltaTime, pos.y);
                        }
                        Players[i].GetComponent<Transform>().localScale = PlayersData[i].PlayersForward;
                        if (PlayersData[i].IsJump)
                        {
                            Jump(i);
                        }

                    }
                    else if (i == ControlPlayerID)//操作対象
                    {
                        MoveCtr.MoveBottonUse(this);
                        ShootCtr.ShootKeyDown(this, i);
                        if (PlayersData[i].IsJump)
                        {
                            Jump(i);
                        }
                    }

                    JumpSmarsh(i);

                    if (Players[i].GetComponent<Rigidbody2D>().velocity.y == 0)
                    {
                        PlayersData[i].JumpedTimes = 0;
                    }
                }
            }

            //次のループへのキー

            if (IsDead)
            {
                PlayersData[ControlPlayerID].IsAlive = false;
                IsDead = false;
            }

            //タイマー累加
            Timer += Time.deltaTime;

            //ループ終了判定
            if (PlayersData[ControlPlayerID].IsAlive)
            {
                //相手全員死んだ
                for (int i = 0; i < PlayersData.Count; i++)
                {
                    if (i != ControlPlayerID)
                    {
                        if (PlayersData[i].IsAlive)
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
            //操作対象変更
            ChangePlayer();

            StartFallBack = false;
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
        if (PlayersData[ID].JumpedTimes < 1)
        {
            Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector2(Players[ID].GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(2 * 9.81f * Players[ID].GetComponent<Rigidbody2D>().gravityScale * JumpFocre * Players[ID].GetComponent<Player>().JumpMass));
        }
        else
        {
            if (!PlayersData[ID].CheakSkill(SkillID.JUMPSMARSH))
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector2(Players[ID].GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(9.81f * Players[ID].GetComponent<Rigidbody2D>().gravityScale * JumpFocre * Players[ID].GetComponent<Player>().JumpMass));
            }
            else
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshSpeed, Players[ID].GetComponent<Rigidbody2D>().velocity.y + SkillDataCtr.JumpSmarshDir.y * SkillDataCtr.JumpSmarshSpeed, 0.0f);

            }
        }
        PlayersData[ID].JumpedTimes += 1;
        PlayersData[ID].IsJump = false;
    }

    public void ToNextIsDown(InputAction.CallbackContext obj)
    {
        IsDead = true;
    }

    public void SetScale(int ID, Vector3 _Scale)
    {
        PlayersData[ID].PlayersForward = _Scale;
        Players[ID].GetComponent<Transform>().localScale = _Scale;
    }

    public bool CheakJumpSmarsh(int ID)
    {
        if (PlayersData[ID].CheakSkill(SkillID.JUMPSMARSH) && SkillDataCtr.UseJumpSmarsh)
        {
            return true;
        }
        return false;
    }

    private void JumpSmarsh(int ID)
    {
        if (CheakJumpSmarsh(ID))
        {
            if (!PlayersData[ID].OnBox)
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(Players[ID].GetComponent<Rigidbody2D>().velocity.x - SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshAngular, Players[ID].GetComponent<Rigidbody2D>().velocity.y - SkillDataCtr.JumpSmarshDir.y * SkillDataCtr.JumpSmarshAngular, 0.0f);
            }
            else
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            SkillDataCtr.JumpSmarsh();
        }
    }

    public void ChangePlayer()
    {
        //データを初期値に戻る
        Timer = 0.0f;
        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData[i].IsJump = false;
            PlayersData[i].IsAlive = true;
            Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
            Players[i].SetActive(true);
        }

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();

        //Players[ControlPlayerID].GetComponent<SpriteRenderer>().sortingOrder = 0;
        ControlPlayerID++;
        ControlPlayerID %= Players.Count;
        //Players[ControlPlayerID].GetComponent<SpriteRenderer>().sortingOrder = 1;

        //保存データの更新
        SavedBehaviour = new PlayerBehaviourData(RecordBehaviour);

        //記録データの削除
        RecordBehaviour.ClearData();

        //新しい記録準備完了
        StartBehaviourRecord = true;
    }

    public void PlayerWithoutLoop()
    {
        Timer = 0.0f;

        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData[i].IsJump = false;
            PlayersData[i].IsAlive = true;
            Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
            Players[i].SetActive(true);
        }

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();

        for(int i = 0; i < SavedBehaviour.GetBehaviourData().Count; i++)
        {
            SavedBehaviour.GetBehaviourData()[i].Used = false;
        }
        RecordBehaviour.ClearData();
        StartBehaviourRecord = true;
    }
}
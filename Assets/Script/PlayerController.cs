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

    private SkillBottonCtr SkillCtr;

    private CharacterUIController ChaUICtr;

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
        ChaUICtr.ChangeUI(ControlPlayerID);
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
                        if (!savedata[i].Used && PlayersData[savedata[i].PlayerID].IsAlive)
                        {
                            Animator[] animators = PlayersData[savedata[i].PlayerID].GetComponentsInChildren<Animator>();
                            //キーの番号ごとに、対応の状態を変更する
                            switch (savedata[i].BottonID)
                            {
                                case 0:
                                    PlayersData[savedata[i].PlayerID].IsMove = -1;
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    animators[1].SetBool("isMoving", true);
                                    PlayersData[savedata[i].PlayerID].ShootSensorDir = new Vector2(-1, 0);
                                    break;
                                case 1:
                                    PlayersData[savedata[i].PlayerID].IsMove = 0;
                                    animators[1].SetBool("isMoving", false);
                                    break;
                                case 2:
                                    PlayersData[savedata[i].PlayerID].IsMove = 1;
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                    animators[1].SetBool("isMoving", true);
                                    PlayersData[savedata[i].PlayerID].ShootSensorDir = new Vector2(1, 0);
                                    break;
                                case 3:
                                    PlayersData[savedata[i].PlayerID].IsMove = 0;
                                    animators[1].SetBool("isMoving", false);
                                    break;
                                case 4:
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    animators[1].SetTrigger("doJump");
                                    break;
                                case 5://シュートだけ、直接シュートする
                                    //ShootCtr.ShootKeyDown(this, savedata[i].PlayerID, savedata[i].ShootDir);
                                    //animators[0].SetTrigger("doAttack");
                                    break;
                                case 6:
                                    PlayersData[savedata[i].PlayerID].ShootSensorDir = new Vector2(0, 1);
                                    break;
                                case 8:
                                    PlayersData[savedata[i].PlayerID].ShootSensorDir = new Vector2(0, -1);
                                    break;
                                case 41:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(1, 0);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    animators[1].SetTrigger("doDash");
                                    break;
                                case 42:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(-1, 0);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    animators[1].SetTrigger("doDash");
                                    break;
                                case 43:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(0, 1);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    PlayersData[savedata[i].PlayerID].ShootSensorDir = new Vector2(0, 1);
                                    animators[1].SetTrigger("doDash");
                                    break;
                                case 44:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(0, -1);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    PlayersData[savedata[i].PlayerID].ShootSensorDir = new Vector2(0, -1);
                                    animators[1].SetTrigger("doDash");
                                    break;
                                case 61:
                                    SkillDataCtr.UseCut = true;
                                    animators[1].SetBool("isKamae", true) ;
                                    break;
                                case 62:
                                    SkillDataCtr.UseStealth = true;
                                    break;
                                case 63:
                                    ShootCtr.ShootKeyDown_Skill(this, savedata[i].PlayerID, SkillDataCtr.KumaBoomBulletObj, savedata[i].ShootDir);
                                    break;
                                case 66:
                                    ShootCtr.ShootKeyDown_Skill(this, savedata[i].PlayerID, SkillDataCtr.CutBulletObj, savedata[i].ShootDir);
                                    SkillDataCtr.UseCut = false;
                                    animators[1].SetBool("isKamae", false);
                                    break;
                                case 67:
                                    SkillDataCtr.UseStealth = false;
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

                    Animator[] animators = Players[i].GetComponentsInChildren<Animator>();

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
                        SkillDataCtr.playerStealth(PlayersData[i]);
                        ShootCtr.ShootKeyDown(this, i);
                    }
                    else if (i == ControlPlayerID)//操作対象
                    {
                        MoveCtr.MoveBottonUse(this);
                        ShootCtr.ShootKeyDown(this, i);
                        if (PlayersData[i].IsJump)
                        {
                            Jump(i);
                        }

                        float limittime = -1, maxtime = -1;

                        switch (PlayersData[ControlPlayerID].SkillIDs[0])
                        {
                            case SkillID.Cut:
                                limittime = SkillDataCtr.CutLimitTime;
                                maxtime = SkillDataCtr.MaxLimitTime;
                                SkillCtr.CheakSKillOver(limittime, maxtime);
                                break;
                            case SkillID.Stealth:
                                break;
                            case SkillID.Boom:
                                break;
                            default:
                                break;
                        }
                    }

                    JumpSmarsh(i);
                    SkillDataCtr.playerStealth(PlayersData[i]);
                    animators[1].SetFloat("vspeed", Players[i].GetComponent<Rigidbody2D>().velocity.y);

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
        SkillCtr = FindObjectOfType<SkillBottonCtr>();
        ChaUICtr = FindObjectOfType<CharacterUIController>();
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
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshSpeed, Players[ID].GetComponent<Rigidbody2D>().velocity.y + SkillDataCtr.JumpSmarshDir.y * SkillDataCtr.JumpSmarshSpeed * 0.5f, 0.0f);
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
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(Players[ID].GetComponent<Rigidbody2D>().velocity.x - SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshAngular * Time.deltaTime, Players[ID].GetComponent<Rigidbody2D>().velocity.y - SkillDataCtr.JumpSmarshDir.y* 0.5f * SkillDataCtr.JumpSmarshAngular * Time.deltaTime, 0.0f);         
            }
            else
            {                
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -0.01f, 0.0f);
            }
            if(SkillDataCtr.JumpSmarsh())
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -0.01f, 0.0f);
            }
        }
    }

    public void ChangePlayer()
    {
        //データを初期値に戻る
        Timer = 0.0f;
        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData[i].HP = PlayersData[i].MaxHP;
            PlayersData[i].IsJump = false;
            PlayersData[i].IsAlive = true;
            PlayersData[i].ShootTimer = 0.0f;
            PlayersData[i].ShotTimes = 0;
            PlayersData[i].ShootIntoCD = false;
            PlayersData[i].CanShoot = false;
            PlayersData[i].EnableMoveJump = true;
            PlayersData[i].EnableMoveJump2 = true;
            Players[i].GetComponentInChildren<LostLifeCtr>().LostLifeReset();
            Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
            Players[i].SetActive(true);
        }

        ChaUICtr.ChangeHP(PlayersData[ControlPlayerID].PlayerID);

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();
        ShootCtr.ShootCDTimer = 0.0f;
       
        SkillCtr.SkillKeyDown = 0;
        SkillCtr.SkillTimer = 0.0f;
        SkillCtr.SkillCDTimer = 0.0f;
        SkillCtr.SkillIntoCD = false;
        SkillDataCtr.EnableUseCut = true;
        SkillDataCtr.UseCut = false;
        SkillDataCtr.StealthReset();
        SkillDataCtr.BoomReset();
        ControlPlayerID++;
        ControlPlayerID %= Players.Count;

        ChaUICtr.ChangeUI(ControlPlayerID);
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
            PlayersData[i].HP = PlayersData[i].MaxHP;
            PlayersData[i].IsJump = false;
            PlayersData[i].IsAlive = true;
            PlayersData[i].ShootTimer = 0.0f;
            PlayersData[i].ShotTimes = 0;
            PlayersData[i].ShootIntoCD = false;
            PlayersData[i].CanShoot = false;
            PlayersData[i].EnableMoveJump = true;
            PlayersData[i].EnableMoveJump2 = true;
            Players[i].GetComponentInChildren<LostLifeCtr>().LostLifeReset();
            Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
            Players[i].SetActive(true);
        }

        ChaUICtr.ChangeHP(PlayersData[ControlPlayerID].PlayerID);

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();
        ShootCtr.ShootCDTimer = 0.0f;

        SkillCtr.SkillKeyDown = 0;
        SkillCtr.SkillTimer = 0.0f;
        SkillCtr.SkillCDTimer = 0.0f;
        SkillCtr.SkillIntoCD = false;
        SkillDataCtr.EnableUseCut = true;
        SkillDataCtr.UseCut = false;
        SkillDataCtr.StealthReset();
        SkillDataCtr.BoomReset();

        for (int i = 0; i < SavedBehaviour.GetBehaviourData().Count; i++)
        {
            SavedBehaviour.GetBehaviourData()[i].Used = false;
        }
        RecordBehaviour.ClearData();
        StartBehaviourRecord = true;
    }


    public float CheakSkillCD()
    {
        float rate = 0.0f;
        switch (PlayersData[ControlPlayerID].SkillIDs[0])
        {
            case SkillID.JUMPSMARSH:
                if(SkillDataCtr.UseJumpSmarsh)
                {
                    rate = 1.0f;
                }
                else
                {
                    rate = 0.0f;
                }
                break;
            case SkillID.Cut:
                rate = 1.0f - SkillCtr.SkillCDTimer / SkillCtr.SkillCD;
                if(!SkillDataCtr.UseCut)
                {
                    rate = 0;
                }
                break;
            case SkillID.Stealth:
                rate = 1.0f - SkillDataCtr.StealthCDTimer / SkillDataCtr.StealthCDTime;
                if(!SkillDataCtr.UseStealth && !SkillCtr.SkillIntoCD)
                {
                    rate = 0;
                }
                break;
            case SkillID.Boom:
                rate = 1.0f - SkillDataCtr.BoomCDTimer / SkillDataCtr.BoomCD;
                if(SkillDataCtr.EnableUseBoom && PlayersData[ControlPlayerID].CanShoot)
                {
                    rate = 0;
                }
                break;
            default:
                break;
        }
        return rate;
    }

    public float CheakShootCD()
    {
        float rate = 0.0f;

        rate = 1.0f - PlayersData[ControlPlayerID].ShootTimer / PlayersData[ControlPlayerID].ShootCD;
        if(PlayersData[ControlPlayerID].CanShoot && !PlayersData[ControlPlayerID].ShootIntoCD)
        {
            rate = 0;
        }

        return rate;
    }

    public bool BreakStealth(int ID)
    {

        if(PlayersData[ID].SkillIDs[0] == SkillID.Stealth && SkillDataCtr.UseStealth)
        {
            return true;
            //SkillDataCtr.UseStealth = false;
            //PlayersData[ID].EnableMoveJump2 = true;
            //if (ID == ControlPlayerID)
            //{
            //    SkillCtr.SkillIntoCD = true;
            //}
        }
        return false;
    }
}
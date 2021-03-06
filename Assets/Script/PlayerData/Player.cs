using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //キャラクターの認識番号
    public int PlayerID;

    //キャラクタ－のヘルスポイント
    public float HP;

    public float MaxHP;

    //キャラクターによる攻撃力の修正値
    public float ATK;

    //キャラクターによるダメージ減少の修正値
    public float DEF;

    //移動速度
    public float MoveSpeed;

    //ジャンプの段数
    public int JumpStep;

    //ジャンプの基本マス数
    public int JumpMass;

    //キャラクターが持ているスキル番号
    public List<SkillID> SkillIDs;

    //移動状態
    public int IsMove;

    public bool OnBox = true;

    public bool EnableMoveJump = true;
    public bool EnableMoveJump2 = true;

    public float DisableMoveTime = 0.3f;

    public float DMTimer = 0.0f;

    //ジャンプ状態
    public bool IsJump;

    //ジャンプした段数
    public int JumpedTimes;

    //生存状態
    public bool IsAlive;
    public bool IsAlive2;

    //キャラクターの初期位置
    public Vector3 StartPoStartPositon;

    //キャラクターの向き方向
    public Vector3 PlayersForward;

    //バレットのインスタンスサンプル
    public GameObject Bullet;

    //バレットのスピード
    public float BulletSpeed = 150.0f;

    //シュートの間隔時間
    public float ShootCD = 0.5f;

    public float ShootTimer = 0.0f;

    public Vector3 ShootPos = new Vector3();

    public bool CanShoot = false;

    public bool ShootIntoCD = false;

    public int EnableShootTimes = 1;

    public int ShotTimes = 0;

    public float ShootCD2 = 0.1f;

    public Vector2 ShootSensorDir = new Vector2(1, 0);

    public Vector3 ShootFixPostion = new Vector3();

    public Vector3 Offset;

    //キャラクターアイコン
    public Sprite AvatarObj;

    //普通の攻撃アイコン
    public Sprite AttackIconObj;

    public Sprite AttackMaskImObj;

    //スキルアイコン
    public Sprite SkillIconObj;

    //スキルのボタンイメージ
    public Sprite SkillBottonIm;

    public Sprite SkillMaskImObj;

    //読み込み用データ
    public PlayerBehaviourData SavedBehaviour = new PlayerBehaviourData();

    //記録用データ
    public PlayerBehaviourData RecordBehaviour = new PlayerBehaviourData();


    // Start is called before the first frame update
    void Awake()
    {
        IsMove = 0;
        IsJump = false;
        IsAlive = true;
        IsAlive2 = true;
        JumpedTimes = 0;
        StartPoStartPositon = gameObject.GetComponent<Transform>().position;
        PlayersForward = gameObject.GetComponent<Transform>().localScale;
        ShootSensorDir = new Vector2(PlayersForward.x, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!EnableMoveJump)
        {
            if(DMTimer < DisableMoveTime)
            {
                DMTimer += Time.deltaTime;
            }
            if(DMTimer >= DisableMoveTime)
            {
                EnableMoveJump = true;
            }
        }
    }

    public void RespawnPosition()
    {
        transform.position = StartPoStartPositon;
    }

    public bool CheakSkill(SkillID _SkillID)
    {
        for(int i = 0; i<SkillIDs.Count;i++)
        {
            if(SkillIDs[i] == _SkillID)
            {
                return true;
            }
        }

        return false;
    }

    public void Hurt(float _Demage,bool IsAddScore = false,GameObject obj = null)
    {

        if(SkillIDs[0] != SkillID.Stealth)
        {
            HP -= _Demage;
            GetComponentInChildren<LostLifeCtr>().AddLostLife((int)_Demage);           
        }
        else
        {
            SkillData sd = FindObjectOfType<SkillData>();
            if(!sd.UseStealth)
            {
                HP -= _Demage;
                GetComponentInChildren<LostLifeCtr>().AddLostLife((int)_Demage);
            }
        }

        if(IsAddScore)
        {
            CharacterUIController cuc = FindObjectOfType<CharacterUIController>();
            int s = (int)_Demage;
            cuc.AddScore(s * 100);
            obj.GetComponent<AddScoreCtr>().SetAddScore(s * 100);
        }
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            OnBox = true;
        }
    }

    public void ShootCDFunc()
    {
        if(ShootIntoCD)
        {
            ShootTimer += Time.deltaTime;


            if(EnableShootTimes - ShotTimes < 1)
            {
                if (ShootTimer >= ShootCD)
                {
                    ShootTimer = 0.0f;
                    ShootIntoCD = false;
                    ShotTimes = 0;
                }
            }
            else
            {
                if (ShootTimer >= ShootCD2)
                {
                    ShootTimer = 0.0f;
                    ShootIntoCD = false;
                }

            }
        }
    }
}

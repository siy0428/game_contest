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

    //ジャンプ状態
    public bool IsJump;

    //ジャンプした段数
    public int JumpedTimes;

    //生存状態
    public bool IsAlive;

    //キャラクターの初期位置
    public Vector2 StartPoStartPositon;

    //キャラクターの向き方向
    public Vector3 PlayersForward;

    //バレットのインスタンスサンプル
    public GameObject Bullet;

    //バレットのスピード
    public float BulletSpeed = 150.0f;

    //シュートの間隔時間
    public float ShootCD = 0.5f;

    public Vector3 ShootFixPostion = new Vector3();

    public Vector3 ObjectPosition;

    //キャラクターアイコン
    public Sprite AvatarObj;

    //普通の攻撃アイコン
    public Sprite AttackIconObj;

    //ユニットスキルアイコン
    public Sprite SkillIconObj;
    
    // Start is called before the first frame update
    void Awake()
    {
        IsMove = 0;
        IsJump = false;
        IsAlive = true;
        JumpedTimes = 0;
        StartPoStartPositon = gameObject.GetComponent<Transform>().position;
        PlayersForward = gameObject.GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            OnBox = true;
        }
    }
}

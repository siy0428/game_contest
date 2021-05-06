using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //キャラクターの認識番号
    public int PlayerID;

    //キャラクタ−のヘルスポイント
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

    //プレイヤーの位置（オブジェクト用）
    public Vector3 ObjectDefaultPosition;

    // Start is called before the first frame update
    void Start()
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

    //最初の場所に戻る
    public void RespawnPosition()
    {
        transform.position = StartPoStartPositon;
    }
}

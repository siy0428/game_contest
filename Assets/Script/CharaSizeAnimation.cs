using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSizeAnimation : MonoBehaviour
{
    // アニメーター変数
    Animator animator;

    // Scale最大値
    Vector3 MaxScale;
    // Scale最低値
    Vector3 MinScale;
    // Scale格納変数
    Vector3 Scale;

    // 収縮間隔
    [SerializeField]
    private float ShrinkFreInter;
    // 経過時間格納
    private float ShrinkTmpTime;
    // ジャンプ収縮間隔
    [SerializeField]
    private float JumpFreInter;
    // 経過時間格納
    float JumpTmpTime;
    // 縮小下限
    public float Shrink;
    // 下がり幅
    public float FallHeight;

    // ジャンプ判別
    bool IsJump;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();

        MaxScale = this.transform.localScale;
        MinScale = this.transform.localScale * Shrink;
        Scale = this.transform.localScale;

        ShrinkTmpTime = 0.0f;
        JumpTmpTime = 0.0f;

        IsJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプ
        if (Input.GetKeyDown("space") && !IsJump) // ジャンプ中ではない状態でスペースを押せばジャンプが出来る
        {
            Scale.y = MinScale.y;
            //animator.SetTrigger("Jump"); // ジャンプモーション遷移
            IsJump = true;
        }

        // ジャンプしてすぐにサイズを元に戻す
        if (IsJump)
        {
            if (JumpTmpTime >= JumpFreInter)
            {
                Scale.y = MaxScale.y;
                JumpTmpTime = 0;
            }
            JumpTmpTime += Time.deltaTime;
        }

        if(Input.GetKeyDown("tab"))// 空中から地上に着地した際の条件
        {
            Scale.y = MinScale.y;
            IsJump = false;
        }

        if (!IsJump)// 収縮しない条件
        {
            // キャラクター収縮
            if (ShrinkTmpTime >= ShrinkFreInter)
            {
                if (Scale.y < MinScale.y)
                {
                    if (FallHeight < 0)
                        FallHeight *= -1.0f;
                }
                else if (Scale.y > MaxScale.y)
                {
                    ShrinkTmpTime = 0;
                    FallHeight *= -1.0f;
                }
                Scale.y += FallHeight;
            }
            ShrinkTmpTime += Time.deltaTime;
        }
        this.transform.localScale = Scale;
    }
}   
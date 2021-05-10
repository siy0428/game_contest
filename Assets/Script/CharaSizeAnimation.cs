using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSizeAnimation : MonoBehaviour
{
    // �A�j���[�^�[�ϐ�
    Animator animator;

    // Scale�ő�l
    Vector3 MaxScale;
    // Scale�Œ�l
    Vector3 MinScale;
    // Scale�i�[�ϐ�
    Vector3 Scale;

    // ���k�Ԋu
    [SerializeField]
    private float ShrinkFreInter;
    // �o�ߎ��Ԋi�[
    private float ShrinkTmpTime;
    // �W�����v���k�Ԋu
    [SerializeField]
    private float JumpFreInter;
    // �o�ߎ��Ԋi�[
    float JumpTmpTime;
    // �k������
    public float Shrink;
    // �����蕝
    public float FallHeight;

    // �W�����v����
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
        // �W�����v
        if (Input.GetKeyDown("space") && !IsJump) // �W�����v���ł͂Ȃ���ԂŃX�y�[�X�������΃W�����v���o����
        {
            Scale.y = MinScale.y;
            //animator.SetTrigger("Jump"); // �W�����v���[�V�����J��
            IsJump = true;
        }

        // �W�����v���Ă����ɃT�C�Y�����ɖ߂�
        if (IsJump)
        {
            if (JumpTmpTime >= JumpFreInter)
            {
                Scale.y = MaxScale.y;
                JumpTmpTime = 0;
            }
            JumpTmpTime += Time.deltaTime;
        }

        if(Input.GetKeyDown("tab"))// �󒆂���n��ɒ��n�����ۂ̏���
        {
            Scale.y = MinScale.y;
            IsJump = false;
        }

        if (!IsJump)// ���k���Ȃ�����
        {
            // �L�����N�^�[���k
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
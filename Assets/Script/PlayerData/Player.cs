using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�L�����N�^�[�̔F���ԍ�
    public int PlayerID;

    //�L�����N�^�|�̃w���X�|�C���g
    public float HP;

    //�L�����N�^�[�ɂ��U���͂̏C���l
    public float ATK;

    //�L�����N�^�[�ɂ��_���[�W�����̏C���l
    public float DEF;

    //�ړ����x
    public float MoveSpeed;

    //�W�����v�̒i��
    public int JumpStep;

    //�W�����v�̊�{�}�X��
    public int JumpMass;

    //�L�����N�^�[�����Ă���X�L���ԍ�
    public List<SkillID> SkillIDs;

    //�ړ����
    public int IsMove;

    public bool OnBox = true;

    //�W�����v���
    public bool IsJump;

    //�W�����v�����i��
    public int JumpedTimes;

    //�������
    public bool IsAlive;

    //�L�����N�^�[�̏����ʒu
    public Vector2 StartPoStartPositon;

    //�L�����N�^�[�̌�������
    public Vector3 PlayersForward;

    //�o���b�g�̃C���X�^���X�T���v��
    public GameObject Bullet;

    //�o���b�g�̃X�s�[�h
    public float BulletSpeed = 150.0f;

    //�V���[�g�̊Ԋu����
    public float ShootCD = 0.5f;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField]
    protected Vector3[] spawns;
    [SerializeField]
    protected int defeat_count;
    [SerializeField]
    protected float time_limit;
    [SerializeField]
    private bool player_add = false;

    protected int enemy_count;    //1���[�v�ŏo������G�̐�
    protected EnemyCreate ec;
    protected EnemyManager em;
    protected InBoxCreate ibc;
    protected PlayerController pc;

    // Start is called before the first frame update
    void Awake()
    {
        //������
        enemy_count = spawns.Length;
        ec = FindObjectOfType<EnemyCreate>();
        em = FindObjectOfType<EnemyManager>();
        ibc = FindObjectOfType<InBoxCreate>();
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer()
    {
        //�v���C���[�ǉ�����
        if (player_add)
        {
            pc.AddDisplayPlayer();
        }
    }

    /// <summary>
    /// �C���X�y�N�^�Őݒ肵�����W�ɓG�𐶐�
    /// </summary>
    virtual public void Create()
    {
        //�G���X�g�̍폜
        em.AllDestroyEnemy();

        foreach (var player in pc.PlayersData)
        {
            player.RespawnPosition();
        }

        foreach (var spawn in spawns)
        {
            ec.Create(spawn);
        }

        ibc.Create();
    }

    /// <summary>
    /// �ڕW���j���̎擾
    /// </summary>
    /// <returns></returns>
    public int GetDefeatCount()
    {
        return defeat_count;
    }

    /// <summary>
    /// ���[�v���Ƃ̐������Ԃ̎擾
    /// </summary>
    /// <returns></returns>
    public float GetTime()
    {
        return time_limit;
    }

    public bool IsCreate()
    {
        return ibc.GetIsCreate();
    }
}

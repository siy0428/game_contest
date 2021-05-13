using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField]
    private Vector3[] spawns;
    [SerializeField]
    private int defeat_count;
    [SerializeField]
    private float time_limit;

    private int enemy_count;    //1���[�v�ŏo������G�̐�
    private EnemyCreate ec;
    private EnemyManager em;
    private InBoxCreate ibc;
    private PlayerController pc;

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

    /// <summary>
    /// �C���X�y�N�^�Őݒ肵�����W�ɓG�𐶐�
    /// </summary>
    public void Create()
    {
        //�G���X�g�̍폜
        em.AllDestroyEnemy();

        ibc.Create();

        foreach(var player in pc.PlayersData)
        {
            player.RespawnPosition();
        }

        foreach (var spawn in spawns)
        {
            ec.Create(spawn);
        }
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

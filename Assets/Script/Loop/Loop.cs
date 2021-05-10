using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField]
    private Vector3[] spawns;
    [SerializeField]
    private int defeat_count;

    private int enemy_count;    //1���[�v�ŏo������G�̐�
    private EnemyCreate ec;

    // Start is called before the first frame update
    void Awake()
    {
        //������
        enemy_count = spawns.Length;
        ec = FindObjectOfType<EnemyCreate>();
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
}

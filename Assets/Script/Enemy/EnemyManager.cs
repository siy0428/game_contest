using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���X�g�֓G�̃I�u�W�F�N�g�ǉ�
    /// </summary>
    /// <param name="enemy">�G�I�u�W�F�N�g</param>
    public void AddEnemy(GameObject enemy)
    {
        enemys.Add(enemy);
    }

    /// <summary>
    /// ���X�g�̎擾
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetEnemy()
    {
        return enemys;
    }

    /// <summary>
    /// ���X�g����w�肵���G�̃I�u�W�F�N�g�폜
    /// </summary>
    /// <param name="enemy">�G�I�u�W�F�N�g</param>
    public void DestroyEnemy(GameObject enemy)
    {
        enemys.Remove(enemy);
    }

    /// <summary>
    /// �S�Ă̗v�f�̍폜
    /// </summary>
    public void AllDestroyEnemy()
    {
        foreach(var enemy in enemys)
        {
            Destroy(enemy);
        }

        enemys.Clear();
    }
}

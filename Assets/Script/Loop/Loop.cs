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

    private int enemy_count;    //1ループで出現する敵の数
    private EnemyCreate ec;
    private EnemyManager em;

    // Start is called before the first frame update
    void Awake()
    {
        //初期化
        enemy_count = spawns.Length;
        ec = FindObjectOfType<EnemyCreate>();
        em = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// インスペクタで設定した座標に敵を生成
    /// </summary>
    public void Create()
    {
        //敵リストの削除
        em.AllDestroyEnemy();

        foreach (var spawn in spawns)
        {
            ec.Create(spawn);
        }
    }

    /// <summary>
    /// 目標撃破数の取得
    /// </summary>
    /// <returns></returns>
    public int GetDefeatCount()
    {
        return defeat_count;
    }

    /// <summary>
    /// ループごとの制限時間の取得
    /// </summary>
    /// <returns></returns>
    public float GetTime()
    {
        return time_limit;
    }
}

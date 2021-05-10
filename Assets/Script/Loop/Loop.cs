using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField]
    private Vector3[] spawns;
    [SerializeField]
    private int defeat_count;

    private int enemy_count;    //1ループで出現する敵の数
    private EnemyCreate ec;

    // Start is called before the first frame update
    void Awake()
    {
        //初期化
        enemy_count = spawns.Length;
        ec = FindObjectOfType<EnemyCreate>();
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
}

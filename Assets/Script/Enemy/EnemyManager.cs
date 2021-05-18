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
    /// リストへ敵のオブジェクト追加
    /// </summary>
    /// <param name="enemy">敵オブジェクト</param>
    public void AddEnemy(GameObject enemy)
    {
        enemys.Add(enemy);
    }

    /// <summary>
    /// リストの取得
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetEnemy()
    {
        return enemys;
    }

    /// <summary>
    /// リストから指定した敵のオブジェクト削除
    /// </summary>
    /// <param name="enemy">敵オブジェクト</param>
    public void DestroyEnemy(GameObject enemy)
    {
        enemys.Remove(enemy);
    }

    /// <summary>
    /// 全ての要素の削除
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

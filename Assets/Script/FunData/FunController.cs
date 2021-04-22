using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFunHit
{
    public GameObject enemy;
    public float distance;

    public void Reset()
    {
        enemy = null;
        distance = 0.0f;
    }
}

public class FunController : MonoBehaviour
{
    [SerializeField]
    private GameObject fun;
    private RangeObject _object;
    private EnemyFunHit enemyFunHit;

    // Start is called before the first frame update
    void Start()
    {
        _object = fun.GetComponent<RangeObject>();
        enemyFunHit = new EnemyFunHit();
    }

    // Update is called once per frame
    void Update()
    {
        //扇の中で一番近い敵の情報のリセット
        enemyFunHit.Reset();

        //敵の扇当たり判定
        EnemyFunCollision();
    }

    /// <summary>
    /// 敵と扇の当たり判定
    /// </summary>
    void EnemyFunCollision()
    {
        //エネミーの取得
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //扇に当たっている敵の取得
        foreach (GameObject enemy in enemys)
        {
            CircleCollider2D rad = enemy.GetComponent<CircleCollider2D>();
            Vector2 enemyPos = enemy.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = _object.RotateAngle + (90.0f - _object.Angle / 2);
            float endDeg = startDeg + _object.Angle;
            float radius = _object.Radius;
            bool funHit = MathUtils.IsInsideOfSector(enemyPos, center, startDeg, endDeg, radius, rad.radius);

            //扇に当たっている場合
            if (funHit)
            {
                Vector3 dist = enemyPos - center;
                //比較対象がまだない場合暫定で敵のオブジェクトを格納
                if (enemyFunHit.enemy == null)
                {
                    enemyFunHit.enemy = enemy;
                    enemyFunHit.distance = dist.magnitude;
                }
                else
                {
                    //現在比較している敵との距離の方が短い場合
                    if (dist.magnitude < enemyFunHit.distance)
                    {
                        enemyFunHit.enemy = enemy;
                        enemyFunHit.distance = dist.magnitude;
                    }
                }
            }
            //敵の色を白に設定
            enemy.GetComponent<Renderer>().material.color = Color.white;
        }
        //扇範囲内の敵は赤色
        if (enemyFunHit.enemy != null)
        {
            enemyFunHit.enemy.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}

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

public class Player : MonoBehaviour
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
        //î‚Ì’†‚Åˆê”Ô‹ß‚¢“G‚Ìî•ñ‚ÌƒŠƒZƒbƒg
        enemyFunHit.Reset();

        //“G‚Ìî“–‚½‚è”»’è
        EnemyFunCollision();
    }

    /// <summary>
    /// “G‚Æî‚Ì“–‚½‚è”»’è
    /// </summary>
    void EnemyFunCollision()
    {
        //ƒGƒlƒ~[‚Ìæ“¾
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //î‚É“–‚½‚Á‚Ä‚¢‚é“G‚Ìæ“¾
        foreach (GameObject enemy in enemys)
        {
            CircleCollider2D rad = enemy.GetComponent<CircleCollider2D>();
            Vector2 enemyPos = enemy.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = _object.RotateAngle + (90.0f - _object.Angle / 2);
            float endDeg = startDeg + _object.Angle;
            float radius = _object.Radius;
            bool funHit = MathUtils.IsInsideOfSector(enemyPos, center, startDeg, endDeg, radius, rad.radius);

            //î‚É“–‚½‚Á‚Ä‚¢‚éê‡
            if (funHit)
            {
                Vector3 dist = enemyPos - center;
                //”äŠr‘ÎÛ‚ª‚Ü‚¾‚È‚¢ê‡b’è‚Å“G‚ÌƒIƒuƒWƒFƒNƒg‚ğŠi”[
                if (enemyFunHit.enemy == null)
                {
                    enemyFunHit.enemy = enemy;
                    enemyFunHit.distance = dist.magnitude;
                }
                else
                {
                    //Œ»İ”äŠr‚µ‚Ä‚¢‚é“G‚Æ‚Ì‹——£‚Ì•û‚ª’Z‚¢ê‡
                    if (dist.magnitude < enemyFunHit.distance)
                    {
                        enemyFunHit.enemy = enemy;
                        enemyFunHit.distance = dist.magnitude;
                    }
                }
            }
            //“G‚ÌF‚ğ”’‚Éİ’è
            enemy.GetComponent<Renderer>().material.color = Color.white;
        }
        //î”ÍˆÍ“à‚Ì“G‚ÍÔF
        if (enemyFunHit.enemy != null)
        {
            enemyFunHit.enemy.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}

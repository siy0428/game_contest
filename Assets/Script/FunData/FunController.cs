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
        //��̒��ň�ԋ߂��G�̏��̃��Z�b�g
        enemyFunHit.Reset();

        //�G�̐���蔻��
        EnemyFunCollision();
    }

    /// <summary>
    /// �G�Ɛ�̓����蔻��
    /// </summary>
    void EnemyFunCollision()
    {
        //�G�l�~�[�̎擾
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //��ɓ������Ă���G�̎擾
        foreach (GameObject enemy in enemys)
        {
            //�G�̐F�𔒂ɐݒ�
            enemy.GetComponent<Renderer>().material.color = Color.white;

            //�G�ƃv���C���[�̃x�N�g��
            Vector3 dir = enemy.transform.position - this.transform.position;

            CircleCollider2D rad = enemy.GetComponent<CircleCollider2D>();
            Vector2 enemyPos = enemy.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = _object.RotateAngle + (90.0f - _object.Angle / 2);
            float endDeg = startDeg + _object.Angle;
            float radius = _object.Radius;
            bool funHit = MathUtils.IsInsideOfSector(enemyPos, center, startDeg, endDeg, radius, rad.radius);

            //�v���C���[����G�ɐL�т�x�N�g��
            RaycastHit2D hit = Physics2D.Linecast(transform.position, enemyPos, 1);
            Debug.DrawLine(transform.position, enemyPos, Color.red, 1);

            //null�łȂ����
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    continue;
                }
            }

            //��ɓ������Ă���ꍇ
            if (funHit)
            {
                Vector3 dist = enemyPos - center;
                //��r�Ώۂ��܂��Ȃ��ꍇ�b��œG�̃I�u�W�F�N�g���i�[
                if (!enemyFunHit.enemy)
                {
                    enemyFunHit.enemy = enemy;
                    enemyFunHit.distance = dist.magnitude;
                }
                else
                {
                    //���ݔ�r���Ă���G�Ƃ̋����̕����Z���ꍇ
                    if (dist.magnitude < enemyFunHit.distance)
                    {
                        enemyFunHit.enemy = enemy;
                        enemyFunHit.distance = dist.magnitude;
                    }
                }
            }

            //��͈͓��̓G�͐ԐF
            if (enemyFunHit.enemy)
            {
                enemyFunHit.enemy.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    private EnemyManager em;

    // Start is called before the first frame update
    void Awake()
    {
        em = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create(Vector3 pos)
    {
        var obj = Instantiate(enemy.gameObject, pos, Quaternion.identity);
        em.AddEnemy(obj);
    }
}

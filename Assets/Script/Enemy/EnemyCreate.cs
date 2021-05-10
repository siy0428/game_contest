using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private List<Vector3> spawns;

    private EnemyManager em;

    // Start is called before the first frame update
    void Start()
    {
        em = FindObjectOfType<EnemyManager>();
        Create();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create()
    {
        foreach (var spawn in spawns)
        {
            var obj = Instantiate(enemy.gameObject, spawn, Quaternion.identity);
            em.AddEnemy(obj);
        }
    }
}

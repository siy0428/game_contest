using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemys;

    // Start is called before the first frame update
    void Start()
    {
        enemys = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemy in enemys)
        {
            Debug.Log(enemy.gameObject.name);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        enemys.Add(enemy);
    }

    public List<GameObject> GetEnemy()
    {
        return enemys;
    }
}

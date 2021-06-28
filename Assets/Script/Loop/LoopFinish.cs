using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopFinish : Loop
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Create()
    {
        GameSceneManager.instance.Result();

        Debug.Log("クリア!!");
        //敵リストの削除
        em.AllDestroyEnemy();

        foreach (var player in pc.PlayersData)
        {
            player.RespawnPosition();
        }
    }

}

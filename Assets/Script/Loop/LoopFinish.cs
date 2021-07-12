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

        Debug.Log("ÅŒã‚Ü‚Å“’B!!CLEAR");
        //“GƒŠƒXƒg‚Ìíœ
        em.AllDestroyEnemy();

        foreach (var player in pc.PlayersData)
        {
            player.RespawnPosition();
        }
    }

}

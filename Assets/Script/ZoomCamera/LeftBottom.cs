using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBottom : ObjectUnchor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ˆê”Ô¶‚É‚¢‚éƒLƒƒƒ‰‚Æ‰º‚É‚¢‚éƒLƒƒƒ‰‚Ì”»•Ê
        int bottom_id = 0;
        int left_id = 0;
        int count = 0;
        var players = pc.Players;
        foreach (var player in players)
        {
            if (players[count].transform.position.y > players[bottom_id].transform.position.y)
            {
                bottom_id = count;
            }
            if (players[count].transform.position.x < players[left_id].transform.position.x)
            {
                left_id = count;
            }
            count++;
        }

        transform.position = new Vector3(players[left_id].transform.position.x, players[bottom_id].transform.position.y, 0.0f);
    }
}

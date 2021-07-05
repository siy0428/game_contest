using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTop : ObjectUnchor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ˆê”Ô‰E‚É‚¢‚éƒLƒƒƒ‰‚Æã‚É‚¢‚éƒLƒƒƒ‰‚Ì”»•Ê
        int top_id = 0;
        int right_id = 0;
        int count = 0;
        var players = pc.Players;
        foreach (var player in players)
        {
            if (players[count].transform.position.y < players[top_id].transform.position.y)
            {
                top_id = count;
            }
            if (players[count].transform.position.x > players[right_id].transform.position.x)
            {
                right_id = count;
            }
            count++;
        }

        transform.position = new Vector3(players[right_id].transform.position.x, players[top_id].transform.position.y, 0.0f);
    }
}

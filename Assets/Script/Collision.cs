using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private Player Playerctr;
    // Start is called before the first frame update
    void Start()
    {
        Playerctr = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        if (Playerctr.PlayersData[Playerctr.ControlPlayerID].IsJump)
        {
            Playerctr.PlayersData[Playerctr.ControlPlayerID].IsJump = false;
        }
        
    }
}

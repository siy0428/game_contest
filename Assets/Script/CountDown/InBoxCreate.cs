using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject InBox;
    [SerializeField]
    private Player[] Players;
    [SerializeField]
    private PlayerController PlayerController;

    private bool IsCreate;

    public void SetIsCraete(bool set) { IsCreate = set; }

    // Start is called before the first frame update
    void Start()
    {
        IsCreate = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in Players)
        {
            if (!player.IsAlive && !IsCreate)
            {
                Create(player.transform.position);
                Debug.Log(player.transform.position);
            }
        }
    }

    public void Create(Vector3 pos)
    {
        Instantiate(InBox, pos, Quaternion.identity);
        IsCreate = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxCreate : MonoBehaviour
{
    [SerializeField]
    private InBoxManager InBox;
    [SerializeField]
    private Player[] Players;

    private PlayerController pc;
    private bool IsCreate;

    public void SetIsCraete(bool set) { IsCreate = set; }
    public bool GetIsCreate() { return IsCreate; }

    // Start is called before the first frame update
    void Awake()
    {
        IsCreate = false;
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var a_player in Players)
        {
            //生きてたら次の処理
            if (a_player.IsAlive)
            {
                continue;
            }

            //すでに生成していたら次の処理
            if (IsCreate)
            {
                continue;
            }

            Create();
        }
    }

    private void Create(GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, Quaternion.identity);
        IsCreate = true;
    }

    private void Create(GameObject obj, Vector3 pos, float alpha)
    {
        var box = Instantiate(obj, pos, Quaternion.identity);
        var ibm = box.GetComponent<InBoxManager>();
        ibm.SetAlpha(alpha);

        IsCreate = true;
    }

    public void Create()
    {
        foreach (var player in Players)
        {
            int id = pc.ControlPlayerID;    //現在操作しているプレイヤーのID
            float alpha = 0.0f;

            if (pc.Players[id].gameObject.name == player.name)
            {
                alpha = 0.5f;
            }
            var pos = player.StartPoStartPositon + player.Offset;
            Create(InBox.gameObject, pos, alpha);
        }
    }
}

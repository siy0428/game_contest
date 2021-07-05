using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoxCreate : MonoBehaviour
{
    [SerializeField]
    private InBoxManager InBox;

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
        foreach (var player in pc.GetAppPlayers())
        {
            int id = pc.ControlPlayerID;    //åªç›ëÄçÏÇµÇƒÇ¢ÇÈÉvÉåÉCÉÑÅ[ÇÃID
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

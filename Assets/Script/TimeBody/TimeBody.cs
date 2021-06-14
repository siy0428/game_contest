using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    //‹tÄ¶—p•Ï”
    private bool isRewinding;
    private Rigidbody2D rb2d;
    private List<Vector3> positions;
    private TimeBodyManager tbm;

    // Start is called before the first frame update
    void Start()
    {
        isRewinding = false;
        positions = new List<Vector3>();
        rb2d = GetComponent<Rigidbody2D>();
        tbm = FindObjectOfType<TimeBodyManager>();
    }

    void Update()
    {
        //‹tÄ¶‚ÌŠJn
        if(tbm.GetIsUse())
        {
            StartRewind();
        }
        //‹tÄ¶‚Ì’â~
        else
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        //‹tÄ¶
        if (isRewinding)
        {
            Rewind();
        }
        //‹L˜^
        else
        {
            Record();
        }
    }

    /// <summary>
    /// ‹tÄ¶
    /// </summary>
    void Rewind()
    {
        if (positions.Count > 0)
        {
            //ƒŠƒXƒg‚Ìæ“ª‚©‚çÀ•W‚ğQÆ
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
            tbm.SetIsUse(false);
            positions.Clear();
        }
    }

    /// <summary>
    /// À•W‚Ì‹L˜^
    /// </summary>
    void Record()
    {
        //ƒŠƒXƒg‚Ìæ“ª‚ÉÀ•W‚ğ‹L˜^
        positions.Insert(0, transform.position);
    }


    /// <summary>
    /// ‹tÄ¶‚ÌŠJn
    /// </summary>
    public void StartRewind()
    {
        isRewinding = true;
        rb2d.isKinematic = true;
    }

    /// <summary>
    /// ‹tÄ¶‚Ì’â~
    /// </summary>
    public void StopRewind()
    {
        isRewinding = false;
        rb2d.isKinematic = false;
    }
}

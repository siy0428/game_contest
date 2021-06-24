using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBodyBulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private float first_time;
    private float real_time;
    private TimeBodyManager tbm;

    // Start is called before the first frame update
    void Start()
    {
        first_time = Time.realtimeSinceStartup;
        real_time = Time.realtimeSinceStartup;
        tbm = FindObjectOfType<TimeBodyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //‹tÄ¶‚µ‚Ä‚¢‚È‚¢ŠÔ·•ª‚Ìæ“¾
        if (!tbm.GetIsUse())
        {
            float time = real_time - first_time;
            Debug.Log("·•ªŠÔ:" + time);
        }

        //‹tÄ¶’†‚Ì’e‚Ì¶¬
        else
        {

        }
    }

    /// <summary>
    /// Œ»İ‚Ì‘ã“ü
    /// </summary>
    public void SetFirstTime()
    {
        first_time = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// w’è‚µ‚½ŠÔ‚Ì‘ã“ü
    /// </summary>
    /// <param name="time">w’è‚µ‚½</param>
    public void SetFirstTime(float time)
    {
        first_time = time;
    }

    /// <summary>
    /// Œ»İ‚Ì‘ã“ü
    /// </summary>
    public void SetRealTime()
    {
        real_time = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// w’è‚µ‚½ŠÔ‚Ì‘ã“ü
    /// </summary>
    /// <param name="time">w’è‚µ‚½</param>
    public void SetRealTime(float time)
    {
        real_time = time;
    }
}

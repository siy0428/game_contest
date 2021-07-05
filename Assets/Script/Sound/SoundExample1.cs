using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SE‚ğ“ü‚ê‚é—á

public class SoundExample1 : MonoBehaviour
{
    public AudioClip[] audioClip;
    AudioSource audioSource;

    void Start()
    {
        //Component‚ğæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //// ¶
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    //‰¹‚ğ–Â‚ç‚·
        //    audioSource.PlayOneShot(audioClip[0]);
        //}
        PlaySE();
    }

    public void PlaySE()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    //‰¹
        //    audioSource.PlayOneShot(audioClip[1]);
        //}

    }
}
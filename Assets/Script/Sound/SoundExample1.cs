using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SE�������

public class SoundExample1 : MonoBehaviour
{
    public AudioClip[] audioClip;
    AudioSource audioSource;

    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //// ��
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    //����炷
        //    audioSource.PlayOneShot(audioClip[0]);
        //}
        PlaySE();
    }

    public void PlaySE()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    //��
        //    audioSource.PlayOneShot(audioClip[1]);
        //}

    }
}
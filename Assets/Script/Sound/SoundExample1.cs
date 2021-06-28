using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SEを入れる例

public class SoundExample1 : MonoBehaviour
{
    public AudioClip[] audioClip;
    AudioSource audioSource;

    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //音を鳴らす
            audioSource.PlayOneShot(audioClip[0]);
        }
        PlaySE();
    }

    public void PlaySE()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //音
            audioSource.PlayOneShot(audioClip[1]);
        }

    }
}
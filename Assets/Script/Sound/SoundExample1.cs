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

        DebugPlaySE();
        DebugPlayBGM();

    }

    //SE関連
    public void DebugPlaySE()
    {
        // 左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //音を鳴らす
            audioSource.PlayOneShot(audioClip[0]);
        }

        //右キー
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //音
            audioSource.PlayOneShot(audioClip[1]);
        }

    }

    //BGM関連
    public void DebugPlayBGM()
    {
        //上
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // BGMの再生
            audioSource.Play();
        }
        //下
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //BGMの停止
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // BGMの一時停止
            audioSource.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // BGMの再開
            audioSource.UnPause();
        }


    }
}
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

        DebugPlaySE();
        DebugPlayBGM();

    }

    //SE�֘A
    public void DebugPlaySE()
    {
        // ��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //����炷
            audioSource.PlayOneShot(audioClip[0]);
        }

        //�E�L�[
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //��
            audioSource.PlayOneShot(audioClip[1]);
        }

    }

    //BGM�֘A
    public void DebugPlayBGM()
    {
        //��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // BGM�̍Đ�
            audioSource.Play();
        }
        //��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //BGM�̒�~
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // BGM�̈ꎞ��~
            audioSource.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // BGM�̍ĊJ
            audioSource.UnPause();
        }


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEVolumeInspecter : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource SEAudioSource;   //���ʒ����Ώۂ�AudioSource

    // Start is called before the first frame update
    void Start()
    {
        //�ǂݍ���
        SEAudioSource = this.GetComponent<AudioSource>();
        SEVolChange();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�I�v�V�����p�@�X���C�_�[�𓮂������Ƃ��ɌĂ�ōX�V
    public void SEVolChange()
    {
        float vol;
        vol = (float)PlayerPrefs.GetInt("VOLUME_SE", 5);
        SEAudioSource.volume = vol * 0.1f;                //0.0~1.0�Ȃ̂ŁA0~10��0.1���|����
    }
}

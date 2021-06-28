using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolumeInspecter : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource BGMAudioSource;   //���ʒ����Ώۂ�AudioSource

    // Start is called before the first frame update
    void Start()
    {
        //�ǂݍ���
        BGMAudioSource = this.GetComponent<AudioSource>();
        BGMVolChange();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�I�v�V�����p�@�X���C�_�[�𓮂������Ƃ��ɌĂ�ōX�V
    public void BGMVolChange()
    {
        float vol;
        vol = (float)PlayerPrefs.GetInt("VOLUME_BGM", 5);
        BGMAudioSource.volume = vol * 0.1f;                //0.0~1.0�Ȃ̂ŁA0~10��0.1���|����
    }
}

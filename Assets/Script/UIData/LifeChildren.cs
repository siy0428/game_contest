using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeChildren : MonoBehaviour
{
    public Sprite[] m_sprite = new Sprite[4];

    // ���g�̃X�v���C�g�R���|�[�l���g
    private SpriteRenderer selfSR;

    // Start is called before the first frame update
    void Start()
    {
        // ���g�̃X�v���C�g�R���|�[�l���g���擾���Ă���
        selfSR = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HeartLess()
    {
        selfSR.sprite = m_sprite[1];
    }
    public void HeartHalf()
    {
        selfSR.sprite = m_sprite[2];
    }
    public void HeartFull()
    {
        selfSR.sprite = m_sprite[3];
    }


}

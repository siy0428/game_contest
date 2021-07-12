using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//---------------------------------
//�e�L�X�g��_�ł�����X�N���v�g
//---------------------------------

public class TextBlinking : MonoBehaviour
{

    public float speed = 1.0f;
    private Text text;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        //�e�L�X�g�R���|�[�l���g���擾
        text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //�A���t�@�̒l��ύX���邱�Ƃœ_��
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {

        time += Time.deltaTime * 5.0f * speed;
        //�����I�ȓ����ɂ�Sin�֐�
        color.a = Mathf.Sin(time);

        return color;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_contoroller : MonoBehaviour
{
    float screen_width = 640.0f;
    float screen_height = 320.0f;

    // �e�I�u�W�F�N�g�iInspector�ŃI�u�W�F�N�g���w��j
    [SerializeField] // Inspector�ő���ł���悤�ɑ�����ǉ�
    private GameObject bullet;
    // �e�I�u�W�F�N�g��Rigidbody2D�̓��ꕨ
    private Rigidbody2D rb2d;
    // �e�I�u�W�F�N�g�̈ړ��W���i���x�����p�j

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > screen_width || transform.position.x < -screen_width ||
            transform.position.y > screen_height || transform.position.y < -screen_height)
        {
            Destroy(gameObject);
        }
    }
    // ENEMY�ƐڐG�����Ƃ��̊֐�
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ENEMY�ɒe���ڐG������e�͏��ł���
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}

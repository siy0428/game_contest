using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akari_Contoroller : MonoBehaviour
{
    public GameObject bullet;
    float bulletspeed;

    Rigidbody2D rb;

    float jumpForce = 5000.0f;       // �W�����v���ɉ������
    float jumpThreshold = 2.0f;    // �W�����v�������肷�邽�߂�臒l
    float runForce = 50.0f;       // ����n�߂ɉ������
    float runSpeed = 100.0f;       // �����Ă���Ԃ̑��x
    float runThreshold = 2.0f;   // ���x�؂�ւ�����̂��߂�臒l
    bool isGround = true;        // �n�ʂƐڒn���Ă��邩�Ǘ�����t���O
    int key = 0;                 // ���E�̓��͊Ǘ�
    float stateEffect = 1;       // ��Ԃɉ����ĉ��ړ����x��ς��邽�߂̌W��

    void Start()
    {
        bulletspeed = 100.0f;  //�e�̑��x
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputKey();          // ���͂��擾
        Move();                 // ���͂ɉ����Ĉړ�����

        if (Input.GetMouseButtonDown(0))
        {

            // �e�i�Q�[���I�u�W�F�N�g�j�̐���
            GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);

            // �N���b�N�������W�̎擾�i�X�N���[�����W���烏�[���h���W�ɕϊ��j
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �����̐����iZ�����̏����Ɛ��K���j
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

            // �e�ɑ��x��^����
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * bulletspeed;

        }
    }

    void GetInputKey()
    {
        key = 0;
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
            key = 1;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            key = -1;
    }

    void Move()
    {
        // �ڒn���Ă��鎞��Space�L�[�����ŃW�����v
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                this.rb.AddForce(transform.up * this.jumpForce);
                isGround = false;
            }
        }

        // ���E�̈ړ��B���̑��x�ɒB����܂ł�Addforce�ŗ͂������A����ȍ~��transform.position�𒼐ڏ��������ē��ꑬ�x�ňړ�����
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            this.rb.AddForce(transform.right * key * this.runForce * stateEffect); //�����͂̏ꍇ�� key �̒l��0�ɂȂ邽�߈ړ����Ȃ�
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
        }

    }
    //���n����
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!isGround)
                isGround = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!isGround)
                isGround = true;
        }
    }
}

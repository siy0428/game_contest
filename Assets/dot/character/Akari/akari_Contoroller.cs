using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akari_Contoroller : MonoBehaviour
{
    public GameObject bullet;
    float bulletspeed;

    Rigidbody2D rb;

    float jumpForce = 5000.0f;       // ジャンプ時に加える力
    float jumpThreshold = 2.0f;    // ジャンプ中か判定するための閾値
    float runForce = 50.0f;       // 走り始めに加える力
    float runSpeed = 100.0f;       // 走っている間の速度
    float runThreshold = 2.0f;   // 速度切り替え判定のための閾値
    bool isGround = true;        // 地面と接地しているか管理するフラグ
    int key = 0;                 // 左右の入力管理
    float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数

    void Start()
    {
        bulletspeed = 100.0f;  //弾の速度
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputKey();          // 入力を取得
        Move();                 // 入力に応じて移動する

        if (Input.GetMouseButtonDown(0))
        {

            // 弾（ゲームオブジェクト）の生成
            GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);

            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

            // 弾に速度を与える
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
        // 接地している時にSpaceキー押下でジャンプ
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                this.rb.AddForce(transform.up * this.jumpForce);
                isGround = false;
            }
        }

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            this.rb.AddForce(transform.right * key * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
        }

    }
    //着地判定
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

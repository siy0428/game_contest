using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_contoroller : MonoBehaviour
{
    float screen_width = 640.0f;
    float screen_height = 320.0f;

    // 弾オブジェクト（Inspectorでオブジェクトを指定）
    [SerializeField] // Inspectorで操作できるように属性を追加
    private GameObject bullet;
    // 弾オブジェクトのRigidbody2Dの入れ物
    private Rigidbody2D rb2d;
    // 弾オブジェクトの移動係数（速度調整用）

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
    // ENEMYと接触したときの関数
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ENEMYに弾が接触したら弾は消滅する
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private RangeObject ro;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトに合わせて回転
        if (ro.FunCollision())
        {
            //扇の範囲内にいる一番近いオブジェクトの座標取得
            var vec1 = ro.FunCollision().transform.position;   
            
            //プレイヤーだったら中心座標に合わせた調整座標を加算する
            if(ro.FunCollision().tag == "Player")
            {
                var player = ro.FunCollision().GetComponent<Player>();
                vec1 += player.Offset;
            }

            var vec2 = transform.position;
            var angle = GetAim(vec1, vec2);
            angle = (angle + 360) % 360;
            //キャラの向きに合わせて反転
            if (ro.Direction.x == 1.0f)
            {
                angle += 180.0f;
            }
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.right);
        }
    }

    /// <summary>
    /// 2点間の角度の取得
    /// </summary>
    /// <param name="p1">オブジェクトの座標</param>
    /// <param name="p2">オブジェクトの座標</param>
    /// <returns>角度</returns>
    private float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }
}

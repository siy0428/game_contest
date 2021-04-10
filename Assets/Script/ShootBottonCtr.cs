using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBottonCtr : MonoBehaviour
{
    //シュート行動データ
    BehaviorData KeyShoot = new BehaviorData();

    //IDは動作の主
    public void ShootKeyDown(PlayerController _PlayerCtr,int ID,Vector2 _ShootDir = new Vector2())
    {
        if (Input.GetMouseButtonDown(0) && ID == _PlayerCtr.ControlPlayerID)//操作対象の処理
        {
           // 弾（ゲームオブジェクト）の生成
           GameObject clone = Instantiate(_PlayerCtr.Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);

            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - _PlayerCtr.Players[ID].GetComponent<Transform>().position), new Vector3(1, 1, 0)).normalized;

            // 弾に速度を与える
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.BulletSpeed;

            //データ記録生成
            KeyShoot.PlayerID = _PlayerCtr.ControlPlayerID;

            KeyShoot.BottonID = 5;

            KeyShoot.StartTime = _PlayerCtr.Timer;

            KeyShoot.ShootDir = shotForward;

            //記録に追加
            _PlayerCtr.RecordBehaviour.AddBehaviour(KeyShoot);
        }
        else if(ID != _PlayerCtr.ControlPlayerID)//非操作対象の処理
        {
            // 弾（ゲームオブジェクト）の生成
            GameObject clone = Instantiate(_PlayerCtr.Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);

            //直接方向を取得
            Vector3 shotForward = _ShootDir;

            // 弾に速度を与える
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.BulletSpeed;
        }
    }
}

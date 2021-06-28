using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    // プレイヤーのステータスを参照
    private Player _State;

    // 自身の子オブジェクト(ハート)の配列
    private LifeChildren[] _Hearts;

    // ライフ比較用に保持するための変数
    private float _beforeLife;
    // ゲーム上のライフオブジェクト最大数
    private const int MAXLIFE = 20;

    void Start()
    {
        // プレイヤーステートマネージャースクリプトを取得
        _State = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        // 比較用に現在ライフを保持しておく
        _beforeLife = _State.HP;
        // ライフ分の配列初期化
        _Hearts = new LifeChildren[20];

        // 子オブジェクトのスクリプトを取得
        _getChildObjScripts();
    }

    //void Update()
    //{
    //    _checkPlayerLife();
    //}

    //private void _checkPlayerLife()
    //{
    //    // プレイヤーの現在ライフを取得
    //    //int nowLife = _State.getPlayerStatus(0);

    //    // 比較用に保持したライフと現在値が変化したかどうか
    //    if (nowLife == 0)
    //    {
    //        // ０になったときはゲームオーバー処理を呼ぶ
    //    }
    //    else if (_beforeLife != nowLife)
    //    {
    //        // 増えたか減ったかの判定をする
    //        bool incDec;
    //        if (_beforeLife > nowLife)
    //        {
    //            // 減る処理に決定
    //            incDec = true;
    //        }
    //        else
    //        {
    //            // 増える処理に決定
    //            incDec = false;
    //        }

    //        // 現在のライフがどこのオブジェクトに該当するかの添え字に変換する
    //        // 現在値を２０で割ったものを１０倍する
    //        float val = (((float)nowLife / (float)MAXLIFE) * 10.0f);
    //        // 小数点を切り上げしてindexに変換
    //        int objIndex = (int)Math.Ceiling(val);

    //        if (val % 1 == 0)
    //        {
    //            // 割ったあまりが０なら
    //            if (incDec)
    //            {
    //                // １つ前のライフが０になったという証拠なのでハートを空にする
    //                _Hearts[objIndex].HeartLess();
    //            }
    //            else
    //            {
    //                // １つ前のライフが１になったという証拠なのでハートを全にする
    //                _Hearts[objIndex].HeartFull();
    //            }
    //        }
    //        else
    //        {
    //            // 割ったあまりが0.5なら
    //            if (incDec)
    //            {
    //                // ライフを半分にする
    //                _Hearts[objIndex - 1].HeartHalf();
    //            }
    //            else
    //            {
    //                // １つ前のライフを半分にする
    //                _Hearts[objIndex].HeartHalf();
    //            }
    //        }
    //        // 次の比較用に現在ライフを保持しておく
    //        _beforeLife = nowLife;
    //    }
    //}


    // 子オブジェクトを取得して格納
    private void _getChildObjScripts()
    {
        // ループ用変数
        int i = 0;
        foreach (Transform child in transform)
        {
            //取得した順番に座標を決める
            float px = i > 9 ? (8 * i) - (8 * 10) : 8 * i;
            float py = i > 9 ? 0 : 8.0f;
            child.transform.localPosition = new Vector3(px, py, 0);
            _Hearts[i] = child.GetComponent<LifeChildren>();
            i++;
        }
    }
}
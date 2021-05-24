using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//参考　https://tech.pjin.jp/blog/2017/07/14/unity_ugui_sync_rendermode/

public class IconController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = new Canvas();

    [SerializeField]
    private Transform targetTfm;

    private RectTransform canvasRectTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0.5f, 0);  //アイコンの座標調整に使う


    //操作しているキャラクターが死んだか
    private bool IsDead = false;

    //プレイヤーリスト
    public List<GameObject> Players = new List<GameObject>();

    //取得したキャラクターのデータ
    public List<Player> PlayersData = new List<Player>();


    void Start()
    {
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        myRectTfm = GetComponent<RectTransform>();
        //初期化
        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData.Add(Players[i].GetComponent<Player>());//キャラクターデータ取得
            PlayersData[i].PlayerID = i;
            //if (i == ControlPlayerID)
            //{
            //    Players[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
            //}
            //else
            //{
            //    Players[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            //}
        }

    }

    void Update()
    {
        Vector2 pos;


        switch (canvas.renderMode)
        {
            //RenderModeごとに描画が変わるので、動的に対応する

            case RenderMode.ScreenSpaceOverlay:
                myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);

                break;

            case RenderMode.ScreenSpaceCamera:
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTfm, screenPos, Camera.main, out pos);
                myRectTfm.localPosition = pos;
                break;

            case RenderMode.WorldSpace:
                myRectTfm.LookAt(Camera.main.transform);

                break;
        }

        //全てのプレイヤーの動きを更新する
        for (int i = 0; i < Players.Count; i++)
        {
            if (!PlayersData[i].IsAlive)
            {

            }
            if (IsDead)
            {
                PlayersData[i].IsAlive = false;
                IsDead = false;
            }

        }
    }
}
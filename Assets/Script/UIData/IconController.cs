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
    [SerializeField]
    private Player player;

    [SerializeField]
    private Vector3 offset;

    private RectTransform canvasRectTfm;
    private RectTransform myRectTfm;
    private PlayerController pc;
    private Image image;

    void Start()
    {
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        myRectTfm = GetComponent<RectTransform>();
        pc = FindObjectOfType<PlayerController>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        //操作していないプレイヤーだったら処理を行わない
        if(pc.ControlPlayerID != player.PlayerID)
        {
            image.enabled = false;
            return;
        }

        //アイコンの描画
        image.enabled = true;

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
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Q�l�@https://tech.pjin.jp/blog/2017/07/14/unity_ugui_sync_rendermode/

public class IconController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform targetTfm;

    private RectTransform canvasRectTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 70.0f, 0);  //�A�C�R���̍��W�����Ɏg��

    void Start()
    {
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        myRectTfm = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 pos;

        switch (canvas.renderMode)
        {
            //RenderMode���Ƃɕ`�悪�ς��̂ŁA���I�ɑΉ�����

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Q�l�@https://tech.pjin.jp/blog/2017/07/14/unity_ugui_sync_rendermode/

public class IconController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = new Canvas();

    [SerializeField]
    private Transform targetTfm;

    private RectTransform canvasRectTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 0.5f, 0);  //�A�C�R���̍��W�����Ɏg��


    //���삵�Ă���L�����N�^�[�����񂾂�
    private bool IsDead = false;

    //�v���C���[���X�g
    public List<GameObject> Players = new List<GameObject>();

    //�擾�����L�����N�^�[�̃f�[�^
    public List<Player> PlayersData = new List<Player>();


    void Start()
    {
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        myRectTfm = GetComponent<RectTransform>();
        //������
        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData.Add(Players[i].GetComponent<Player>());//�L�����N�^�[�f�[�^�擾
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

        //�S�Ẵv���C���[�̓������X�V����
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
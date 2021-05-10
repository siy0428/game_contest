using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


//�Q�l:https://enjoy-freely.com/%E3%80%90unity%E3%80%91%E3%83%87%E3%83%BC%E3%82%BF%E4%BF%9D%E5%AD%98%E3%82%BB%E3%83%BC%E3%83%96%E3%81%A7%E3%81%8D%E3%82%8B%E3%82%88%E3%81%86%E3%81%AB%E3%81%97%E3%82%88%E3%81%86%EF%BC%81/

public class ClickPositionCreate : MonoBehaviour
{
    // ����������Prefab
    public GameObject Tilemap;
    // �N���b�N�����ʒu���W
    private Vector3 clickPosition;
    // Use this for initialization
    void Start()
    {
        string[] saveStringList = new string[3];

        for (int i = 0; i < saveStringList.Length; i++)
        {
            //�Z�[�u�f�[�^����ꍇ�́A"Save_Data"Key�������o�����
            //�Z�[�u�f�[�^���Ȃ��ꍇ�́A"No Data"Key�������o�����
            string saveString = PlayerPrefs.GetString("Save_Data" + i, "No Data");
            //�[���ɓ����Ă�f�[�^��ǂݍ���
            Debug.Log(saveString);
        }
        for (int i = 0; i < saveStringList.Length; i++)
        {
            //�f�[�^��ݒ肷��
            PlayerPrefs.SetString("Save_Data" + i, "Have Data" + i);
            //�ݒ肵���f�[�^��ۑ�
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X���͂ō��N���b�N�i0�j�𗣂����u��
        if (Input.GetMouseButtonUp(1))
        {
            // �����ł̒��ӓ_�͍��W�̈�����Vector2��n���̂ł͂Ȃ��AVector3��n�����Ƃł���B
            // Vector3�Ń}�E�X���N���b�N�����ʒu���W���擾����
            clickPosition = Input.mousePosition;
            // Z���C��
            clickPosition.z = 10f;
            // �I�u�W�F�N�g���� : �I�u�W�F�N�g(GameObject), �ʒu(Vector3), �p�x(Quaternion)
            // ScreenToWorldPoint(�ʒu(Vector3))�F�X�N���[�����W�����[���h���W�ɕϊ�����
            Instantiate(Tilemap, Camera.main.ScreenToWorldPoint(clickPosition), Tilemap.transform.rotation);
        }
    }
}

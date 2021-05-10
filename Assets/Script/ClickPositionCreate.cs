using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

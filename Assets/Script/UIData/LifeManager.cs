using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    // �v���C���[�̃X�e�[�^�X���Q��
    private Player _State;

    // ���g�̎q�I�u�W�F�N�g(�n�[�g)�̔z��
    private LifeChildren[] _Hearts;

    // ���C�t��r�p�ɕێ����邽�߂̕ϐ�
    private float _beforeLife;
    // �Q�[����̃��C�t�I�u�W�F�N�g�ő吔
    private const int MAXLIFE = 20;

    void Start()
    {
        // �v���C���[�X�e�[�g�}�l�[�W���[�X�N���v�g���擾
        _State = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        // ��r�p�Ɍ��݃��C�t��ێ����Ă���
        _beforeLife = _State.HP;
        // ���C�t���̔z�񏉊���
        _Hearts = new LifeChildren[20];

        // �q�I�u�W�F�N�g�̃X�N���v�g���擾
        _getChildObjScripts();
    }

    //void Update()
    //{
    //    _checkPlayerLife();
    //}

    //private void _checkPlayerLife()
    //{
    //    // �v���C���[�̌��݃��C�t���擾
    //    //int nowLife = _State.getPlayerStatus(0);

    //    // ��r�p�ɕێ��������C�t�ƌ��ݒl���ω��������ǂ���
    //    if (nowLife == 0)
    //    {
    //        // �O�ɂȂ����Ƃ��̓Q�[���I�[�o�[�������Ă�
    //    }
    //    else if (_beforeLife != nowLife)
    //    {
    //        // �����������������̔��������
    //        bool incDec;
    //        if (_beforeLife > nowLife)
    //        {
    //            // ���鏈���Ɍ���
    //            incDec = true;
    //        }
    //        else
    //        {
    //            // �����鏈���Ɍ���
    //            incDec = false;
    //        }

    //        // ���݂̃��C�t���ǂ��̃I�u�W�F�N�g�ɊY�����邩�̓Y�����ɕϊ�����
    //        // ���ݒl���Q�O�Ŋ��������̂��P�O�{����
    //        float val = (((float)nowLife / (float)MAXLIFE) * 10.0f);
    //        // �����_��؂�グ����index�ɕϊ�
    //        int objIndex = (int)Math.Ceiling(val);

    //        if (val % 1 == 0)
    //        {
    //            // ���������܂肪�O�Ȃ�
    //            if (incDec)
    //            {
    //                // �P�O�̃��C�t���O�ɂȂ����Ƃ����؋��Ȃ̂Ńn�[�g����ɂ���
    //                _Hearts[objIndex].HeartLess();
    //            }
    //            else
    //            {
    //                // �P�O�̃��C�t���P�ɂȂ����Ƃ����؋��Ȃ̂Ńn�[�g��S�ɂ���
    //                _Hearts[objIndex].HeartFull();
    //            }
    //        }
    //        else
    //        {
    //            // ���������܂肪0.5�Ȃ�
    //            if (incDec)
    //            {
    //                // ���C�t�𔼕��ɂ���
    //                _Hearts[objIndex - 1].HeartHalf();
    //            }
    //            else
    //            {
    //                // �P�O�̃��C�t�𔼕��ɂ���
    //                _Hearts[objIndex].HeartHalf();
    //            }
    //        }
    //        // ���̔�r�p�Ɍ��݃��C�t��ێ����Ă���
    //        _beforeLife = nowLife;
    //    }
    //}


    // �q�I�u�W�F�N�g���擾���Ċi�[
    private void _getChildObjScripts()
    {
        // ���[�v�p�ϐ�
        int i = 0;
        foreach (Transform child in transform)
        {
            //�擾�������Ԃɍ��W�����߂�
            float px = i > 9 ? (8 * i) - (8 * 10) : 8 * i;
            float py = i > 9 ? 0 : 8.0f;
            child.transform.localPosition = new Vector3(px, py, 0);
            _Hearts[i] = child.GetComponent<LifeChildren>();
            i++;
        }
    }
}
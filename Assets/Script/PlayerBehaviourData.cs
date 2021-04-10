using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̍s���f�[�^
public class PlayerBehaviourData
{
   //�s���L�^���X�g
   private List<BehaviorData> Behaviour = new List<BehaviorData>();

    //�������̂��߁A�R���X�g���N�^���K�v
    public PlayerBehaviourData()
    {

    }

    //�R�s�[�R���X�g���N�^(�L�^�ƕۑ��f�[�^�̍X�V�p)
    public PlayerBehaviourData(PlayerBehaviourData _data)
    {
        List<BehaviorData> b_data = _data.GetBehaviourData();
        for (int i = 0; i< b_data.Count;i++)
        {
            Behaviour.Add(new BehaviorData(b_data[i]));
        }
    }

    //�s���ǉ�
    public void AddBehaviour(BehaviorData _Data)
    {
        Behaviour.Add(new BehaviorData(_Data));
    }

    //���X�g���N���A����
    public void ClearData()
    {
        Behaviour.Clear();
    }

    //���X�g�̃|�C���^���擾����
    public List<BehaviorData> GetBehaviourData()
    {
        return Behaviour;
    }
}

//��̍s���̍\���v�f
public class BehaviorData
{
    //�v���C���[�ԍ�
    public int PlayerID;

    //�L�[�ԍ�
    public int BottonID;
    
    //�s���̎��_
    public float StartTime;

    //�V���[�g�����g���B�V���[�g�̕���
    public Vector2 ShootDir;

    //�������̂��߁A�R���X�g���N�^���K�v
    public BehaviorData()
    {
        BottonID = -1;
    }

    //�R�s�[�R���X�g���N�^
    public BehaviorData(BehaviorData _data)
    {
        PlayerID = _data.PlayerID;
        BottonID = _data.BottonID;
        StartTime = _data.StartTime;
        ShootDir = _data.ShootDir;
    }
}


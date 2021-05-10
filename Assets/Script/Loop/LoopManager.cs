using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField]
    private Loop[] loops;

    private int loop_id;        //���݂̃��[�v�̒i�K
    private int defeat_player;  //���삵�Ă���v���C���[���|�����G�̐�

    // Start is called before the first frame update
    void Start()
    {
        loop_id = 0;
        defeat_player = 0;
        loops[loop_id].Create();  //1�ڂ̃��[�v����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���݂̃��[�v�̖ڕW���j���ɓ��B���Ă��邩
    /// </summary>
    public void Finish()
    {
        //�ڕW���j���ɓ��B���Ă����ꍇ���̃��[�v
        if (defeat_player >= loops[loop_id].GetDefeatCount())
        {
            loop_id++;
            defeat_player = 0;
            loops[loop_id].Create();    //���̃��[�v����
        }

        //���j�ڕW�ɓ��B�o���Ȃ������ꍇ�������[�v
        else
        {
            defeat_player = 0;
            loops[loop_id].Create();    //��������
        }
    }

    public void AddDefeat()
    {
        defeat_player++;
    }
}

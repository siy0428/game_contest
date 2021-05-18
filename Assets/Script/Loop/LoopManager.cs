using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [SerializeField]
    private Loop[] loops;

    private int loop_id;        //���݂̃��[�v�̒i�K
    private int defeat_player;  //���삵�Ă���v���C���[���|�����G�̐�
    private float time;         //���[�v���Ƃ̎��Ԑ���
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        loop_id = 0;
        defeat_player = 0;
        loops[loop_id].Create();            //1�ڂ̃��[�v����
        time = loops[loop_id].GetTime();    //1�ڂ̃��[�v�̎��Ԏ擾
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //�{�b�N�X�������͏������s��Ȃ�
        if (loops[loop_id].IsCreate())
        {
            return;
        }

        //�������Ԍ�̔���
        Finish();

        time -= Time.deltaTime;
    }

    /// <summary>
    /// ���Ԍo�ߌ�̏���
    /// </summary>
    /// <returns>�N���A�����ꍇtrue���A���Ă���</returns>
    private void Finish()
    {
        //�^�C���I�[�o�[�ł����
        if (time <= 0.0f)
        {
            //�ڕW���j���ɓ��B���Ă����ꍇ���̃��[�v
            if (defeat_player >= loops[loop_id].GetDefeatCount())
            {
                pc.ChangePlayer();                  //���̃v���C���[�֑���ύX
                loop_id++;
                defeat_player = 0;
                loops[loop_id].Create();            //���̃��[�v����
                time = loops[loop_id].GetTime();    //���̃��[�v�̎��Ԏ擾
            }

            //���j�ڕW�ɓ��B�o���Ȃ������ꍇ�������[�v
            else
            {
                LoopAgain();
            }
        }
    }

    /// <summary>
    /// ���j�J�E���g�v���X
    /// </summary>
    public void AddDefeat()
    {
        defeat_player++;
    }

    /// <summary>
    /// ���݂̃��[�v�̐������Ԏ擾
    /// </summary>
    /// <returns></returns>
    public float GetTimeLimit()
    {
        return loops[loop_id].GetTime();
    }

    /// <summary>
    /// ���݂̌o�ߎ��Ԏ擾
    /// </summary>
    /// <returns></returns>
    public float GetNowTime()
    {
        return time;
    }

    /// <summary>
    /// ���[�v���Ƃ̖ڕW���j���̎擾
    /// </summary>
    /// <returns></returns>
    public float GetFinishDefeatCount()
    {
        return loops[loop_id].GetDefeatCount();
    }

    /// <summary>
    /// ���݂̌��j���擾
    /// </summary>
    /// <returns></returns>
    public float GetNowDefeatCount()
    {
        return defeat_player;
    }

    /// <summary>
    /// ������x�������[�v���̏���
    /// </summary>
    public void LoopAgain()
    {
        pc.PlayerWithoutLoop();             //�����v���C���[�̑��쏈��
        defeat_player = 0;
        loops[loop_id].Create();            //�������[�v�̐���
        time = loops[loop_id].GetTime();    //�������[�v�̎��Ԏ擾  
    }

    /// <summary>
    /// ���[�v�Ǘ��ԍ��̎擾
    /// </summary>
    /// <returns></returns>
    public int GetLoopId()
    {
        return loop_id;
    }

    /// <summary>
    /// ���[�v�̑����̎擾
    /// </summary>
    /// <returns></returns>
    public int GetLoopTotalCount()
    {
        return loops.Length - 1;
    }
}

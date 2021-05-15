using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //!!!!�@���E����̂́@U�@�L�[��

    //�v���C���[���X�g
    public List<GameObject> Players = new List<GameObject>();

    //�����삵�Ă���v���C���[�ԍ�
    public int ControlPlayerID = 0;

    //�s���L�^���[�h
    public bool StartBehaviourRecord = true;

    //�o�b�N�A�b�v���[�h(���g���Ă��Ȃ��A�����̕����t���O�Ƃ��āA�g���Ă�)
    public bool StartFallBack = false;

    //�o�b�N�A�b�v�̃X�s�[�h�{��(���g���Ă��Ȃ��j
    public float FallBackSpeed = 5.0f;

    //�ړ��̃X�N���v�g�|�C���g
    private MoveBottonCtr MoveCtr;

    //�V���[�g�̃X�N���v�g�|�C���g
    private ShootBottonCtr ShootCtr;

    //�X�L���̐ݒ�f�[�^���擾�p
    public SkillData SkillDataCtr;

    //���̃��[�v���̃^�C�}�[
    public float Timer = 0.0f;

    //�ǂݍ��ݗp�f�[�^
    public PlayerBehaviourData SavedBehaviour = new PlayerBehaviourData();

    //�L�^�p�f�[�^
    public PlayerBehaviourData RecordBehaviour = new PlayerBehaviourData();

    //�W�����v�Ɏg���������̗�
    public float JumpFocre = 5500.0f;

    //���삵�Ă���L�����N�^�[�����񂾂�
    private bool IsDead = false;

    //�擾�����L�����N�^�[�̃f�[�^
    public List<Player> PlayersData = new List<Player>();

    // Start is called before the first frame update
    public void Start()
    {
        //�X�N���v�g�擾
        GetBottonCtr();
        SkillDataCtr = FindObjectOfType<SkillData>();
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

    // Update is called once per frame
    void Update()
    {
        if (!StartFallBack)
        {
            //�ŏ��̃��[�v�����肪�����Ă��Ȃ��̂ŁA���L�̑�����W�����v����
            if (SavedBehaviour.GetBehaviourData().Count > 1)
            {
                //�ۑ����ꂽ�f�[�^�̎擾��������̂ݔ�������B
                var savedata = SavedBehaviour.GetBehaviourData();

                //�ۑ����ꂽ�f�[�^�̔������@
                for (int i = 0; i < savedata.Count; i++)
                {
                    //�L�^���_�ɂȂ�����
                    if (Timer > savedata[i].StartTime)
                    {
                        if (!savedata[i].Used)
                        {
                            //�L�[�̔ԍ����ƂɁA�Ή��̏�Ԃ�ύX����
                            switch (savedata[i].BottonID)
                            {
                                case 0:
                                    PlayersData[savedata[i].PlayerID].IsMove = -1;
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    break;
                                case 1:
                                    PlayersData[savedata[i].PlayerID].IsMove = 0;
                                    break;
                                case 2:
                                    PlayersData[savedata[i].PlayerID].IsMove = 1;
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                    break;
                                case 3:
                                    PlayersData[savedata[i].PlayerID].IsMove = 0;
                                    break;
                                case 4:
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    break;
                                case 5://�V���[�g�����A���ڃV���[�g����
                                    ShootCtr.ShootKeyDown(this, savedata[i].PlayerID, savedata[i].ShootDir);
                                    break;
                                case 41:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(1, 0);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                case 42:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(-1, 0);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                case 43:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(0, 1);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                case 44:
                                    SkillDataCtr.JumpSmarshDir = new Vector2(0, -1);
                                    PlayersData[savedata[i].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                    PlayersData[savedata[i].PlayerID].IsJump = true;
                                    PlayersData[savedata[i].PlayerID].OnBox = false;
                                    SkillDataCtr.UseJumpSmarsh = true;
                                    break;
                                default:
                                    break;
                            }
                            savedata[i].Used = true;
                        }
                    }
                }
            }

            //�S�Ẵv���C���[�̓������X�V����
            for (int i = 0; i < Players.Count; i++)
            {
                if (PlayersData[i].IsAlive)
                {
                    //�񑀍�Ώ�
                    if (i != ControlPlayerID)
                    {
                        if (!CheakJumpSmarsh(i))
                        {
                            Vector2 pos = Players[i].GetComponent<Transform>().position;
                            Players[i].GetComponent<Transform>().position = new Vector2(pos.x + PlayersData[i].IsMove * PlayersData[i].MoveSpeed * Time.deltaTime, pos.y);
                        }
                        Players[i].GetComponent<Transform>().localScale = PlayersData[i].PlayersForward;
                        if (PlayersData[i].IsJump)
                        {
                            Jump(i);
                        }

                    }
                    else if (i == ControlPlayerID)//����Ώ�
                    {
                        MoveCtr.MoveBottonUse(this);
                        ShootCtr.ShootKeyDown(this, i);
                        if (PlayersData[i].IsJump)
                        {
                            Jump(i);
                        }
                    }

                    JumpSmarsh(i);

                    if (Players[i].GetComponent<Rigidbody2D>().velocity.y == 0)
                    {
                        PlayersData[i].JumpedTimes = 0;
                    }
                }
            }

            //���̃��[�v�ւ̃L�[

            if (IsDead)
            {
                PlayersData[ControlPlayerID].IsAlive = false;
                IsDead = false;
            }

            //�^�C�}�[�݉�
            Timer += Time.deltaTime;

            //���[�v�I������
            if (PlayersData[ControlPlayerID].IsAlive)
            {
                //����S������
                for (int i = 0; i < PlayersData.Count; i++)
                {
                    if (i != ControlPlayerID)
                    {
                        if (PlayersData[i].IsAlive)
                        {
                            return;
                        }
                    }
                }

                StartBehaviourRecord = false;
                StartFallBack = true;
            }
            else//���g������
            {
                StartBehaviourRecord = false;
                StartFallBack = true;
            }
        }
        else
        {
            //����ΏەύX
            ChangePlayer();

            StartFallBack = false;
        }
    }

    //�X�N���v�g�擾
    void GetBottonCtr()
    {
        MoveCtr = FindObjectOfType<MoveBottonCtr>();
        ShootCtr = FindObjectOfType<ShootBottonCtr>();
    }

    //�W�����v����
    void Jump(int ID)
    {
        if (PlayersData[ID].JumpedTimes < 1)
        {
            Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector2(Players[ID].GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(2 * 9.81f * Players[ID].GetComponent<Rigidbody2D>().gravityScale * JumpFocre * Players[ID].GetComponent<Player>().JumpMass));
        }
        else
        {
            if (!PlayersData[ID].CheakSkill(SkillID.JUMPSMARSH))
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector2(Players[ID].GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(9.81f * Players[ID].GetComponent<Rigidbody2D>().gravityScale * JumpFocre * Players[ID].GetComponent<Player>().JumpMass));
            }
            else
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshSpeed, Players[ID].GetComponent<Rigidbody2D>().velocity.y + SkillDataCtr.JumpSmarshDir.y * SkillDataCtr.JumpSmarshSpeed, 0.0f);

            }
        }
        PlayersData[ID].JumpedTimes += 1;
        PlayersData[ID].IsJump = false;
    }

    public void ToNextIsDown(InputAction.CallbackContext obj)
    {
        IsDead = true;
    }

    public void SetScale(int ID, Vector3 _Scale)
    {
        PlayersData[ID].PlayersForward = _Scale;
        Players[ID].GetComponent<Transform>().localScale = _Scale;
    }

    public bool CheakJumpSmarsh(int ID)
    {
        if (PlayersData[ID].CheakSkill(SkillID.JUMPSMARSH) && SkillDataCtr.UseJumpSmarsh)
        {
            return true;
        }
        return false;
    }

    private void JumpSmarsh(int ID)
    {
        if (CheakJumpSmarsh(ID))
        {
            if (!PlayersData[ID].OnBox)
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(Players[ID].GetComponent<Rigidbody2D>().velocity.x - SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshAngular, Players[ID].GetComponent<Rigidbody2D>().velocity.y - SkillDataCtr.JumpSmarshDir.y * SkillDataCtr.JumpSmarshAngular, 0.0f);
            }
            else
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            SkillDataCtr.JumpSmarsh();
        }
    }

    public void ChangePlayer()
    {
        //�f�[�^�������l�ɖ߂�
        Timer = 0.0f;
        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData[i].IsJump = false;
            PlayersData[i].IsAlive = true;
            Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
            Players[i].SetActive(true);
        }

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();

        //Players[ControlPlayerID].GetComponent<SpriteRenderer>().sortingOrder = 0;
        ControlPlayerID++;
        ControlPlayerID %= Players.Count;
        //Players[ControlPlayerID].GetComponent<SpriteRenderer>().sortingOrder = 1;

        //�ۑ��f�[�^�̍X�V
        SavedBehaviour = new PlayerBehaviourData(RecordBehaviour);

        //�L�^�f�[�^�̍폜
        RecordBehaviour.ClearData();

        //�V�����L�^��������
        StartBehaviourRecord = true;
    }

    public void PlayerWithoutLoop()
    {
        Timer = 0.0f;

        for (int i = 0; i < Players.Count; i++)
        {
            PlayersData[i].IsJump = false;
            PlayersData[i].IsAlive = true;
            Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
            Players[i].SetActive(true);
        }

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();

        for(int i = 0; i < SavedBehaviour.GetBehaviourData().Count; i++)
        {
            SavedBehaviour.GetBehaviourData()[i].Used = false;
        }
        RecordBehaviour.ClearData();
        StartBehaviourRecord = true;
    }
}
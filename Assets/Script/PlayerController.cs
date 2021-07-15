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

    private SkillBottonCtr SkillCtr;

    private CharacterUIController ChaUICtr;

    private BGCtr BgCtr;

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

    //���̃��[�v���̃v���C���[��
    public int DisPlayPlayerNums = 2;

    // Start is called before the first frame update
    public void Awake()
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

            if (i < DisPlayPlayerNums)
            {
                Players[i].SetActive(true);
            }
            else
            {
                Players[i].SetActive(false);
            }
        }
        ChaUICtr.ChangeUI(ControlPlayerID);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartFallBack)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                //�ŏ��̃��[�v�����肪�����Ă��Ȃ��̂ŁA���L�̑�����W�����v����
                if (PlayersData[i].PlayerID != ControlPlayerID && PlayersData[i].SavedBehaviour.GetBehaviourData().Count > 1)
                {
                    //�ۑ����ꂽ�f�[�^�̎擾��������̂ݔ�������B
                    var savedata = PlayersData[i].SavedBehaviour.GetBehaviourData();

                    //�ۑ����ꂽ�f�[�^�̔������@
                    for (int j = 0; j < savedata.Count; j++)
                    {
                        //�L�^���_�ɂȂ�����
                        if (Timer > savedata[j].StartTime)
                        {
                            if (!savedata[j].Used && PlayersData[savedata[j].PlayerID].IsAlive)
                            {
                                Animator[] animators = PlayersData[savedata[j].PlayerID].GetComponentsInChildren<Animator>();
                                //�L�[�̔ԍ����ƂɁA�Ή��̏�Ԃ�ύX����
                                switch (savedata[j].BottonID)
                                {
                                    case 0:
                                        PlayersData[savedata[j].PlayerID].IsMove = -1;
                                        PlayersData[savedata[j].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                        animators[1].SetBool("isMoving", true);
                                        PlayersData[savedata[j].PlayerID].ShootSensorDir = new Vector2(-1, 0);
                                        break;
                                    case 1:
                                        PlayersData[savedata[j].PlayerID].IsMove = 0;
                                        animators[1].SetBool("isMoving", false);
                                        break;
                                    case 2:
                                        PlayersData[savedata[j].PlayerID].IsMove = 1;
                                        PlayersData[savedata[j].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                        animators[1].SetBool("isMoving", true);
                                        PlayersData[savedata[j].PlayerID].ShootSensorDir = new Vector2(1, 0);
                                        break;
                                    case 3:
                                        PlayersData[savedata[j].PlayerID].IsMove = 0;
                                        animators[1].SetBool("isMoving", false);
                                        break;
                                    case 4:
                                        PlayersData[savedata[j].PlayerID].IsJump = true;
                                        PlayersData[savedata[j].PlayerID].OnBox = false;
                                        animators[1].SetTrigger("doJump");
                                        break;
                                    case 5://�V���[�g�����A���ڃV���[�g����
                                           //ShootCtr.ShootKeyDown(this, savedata[i].PlayerID, savedata[i].ShootDir);
                                           //animators[0].SetTrigger("doAttack");
                                        break;
                                    case 6:
                                        PlayersData[savedata[j].PlayerID].ShootSensorDir = new Vector2(0, 1);
                                        break;
                                    case 8:
                                        PlayersData[savedata[j].PlayerID].ShootSensorDir = new Vector2(0, -1);
                                        break;
                                    case 41:
                                        SkillDataCtr.JumpSmarshDir = new Vector2(1, 0);
                                        PlayersData[savedata[j].PlayerID].PlayersForward = new Vector3(1.0f, 1.0f, 0.0f);
                                        PlayersData[savedata[j].PlayerID].IsJump = true;
                                        PlayersData[savedata[j].PlayerID].OnBox = false;
                                        SkillDataCtr.UseJumpSmarsh = true;
                                        animators[1].SetTrigger("doDash");
                                        break;
                                    case 42:
                                        SkillDataCtr.JumpSmarshDir = new Vector2(-1, 0);
                                        PlayersData[savedata[j].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                        PlayersData[savedata[j].PlayerID].IsJump = true;
                                        PlayersData[savedata[j].PlayerID].OnBox = false;
                                        SkillDataCtr.UseJumpSmarsh = true;
                                        animators[1].SetTrigger("doDash");
                                        break;
                                    case 43:
                                        SkillDataCtr.JumpSmarshDir = new Vector2(0, 1);
                                        PlayersData[savedata[j].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                        PlayersData[savedata[j].PlayerID].IsJump = true;
                                        PlayersData[savedata[j].PlayerID].OnBox = false;
                                        SkillDataCtr.UseJumpSmarsh = true;
                                        PlayersData[savedata[j].PlayerID].ShootSensorDir = new Vector2(0, 1);
                                        animators[1].SetTrigger("doDash");
                                        break;
                                    case 44:
                                        SkillDataCtr.JumpSmarshDir = new Vector2(0, -1);
                                        PlayersData[savedata[j].PlayerID].PlayersForward = new Vector3(-1.0f, 1.0f, 0.0f);
                                        PlayersData[savedata[j].PlayerID].IsJump = true;
                                        PlayersData[savedata[j].PlayerID].OnBox = false;
                                        SkillDataCtr.UseJumpSmarsh = true;
                                        PlayersData[savedata[j].PlayerID].ShootSensorDir = new Vector2(0, -1);
                                        animators[1].SetTrigger("doDash");
                                        break;
                                    case 61:
                                        SkillDataCtr.UseCut = true;
                                        animators[1].SetBool("isKamae", true);
                                        break;
                                    case 62:
                                        SkillDataCtr.UseStealth = true;
                                        break;
                                    case 63:
                                        ShootCtr.ShootKeyDown_Skill(this, savedata[j].PlayerID, SkillDataCtr.KumaBoomBulletObj, savedata[j].ShootDir);
                                        break;
                                    case 66:
                                        ShootCtr.ShootKeyDown_Skill(this, savedata[j].PlayerID, SkillDataCtr.CutBulletObj, savedata[j].ShootDir);
                                        SkillDataCtr.UseCut = false;
                                        animators[1].SetBool("isKamae", false);
                                        break;
                                    case 67:
                                        SkillDataCtr.UseStealth = false;
                                        break;
                                    default:
                                        break;
                                }
                                savedata[j].Used = true;
                            }
                        }
                    }
                }

            }

            //�S�Ẵv���C���[�̓������X�V����
            for (int i = 0; i < Players.Count; i++)
            {
                if (PlayersData[i].IsAlive)
                {

                    Animator[] animators = Players[i].GetComponentsInChildren<Animator>();

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
                        SkillDataCtr.playerStealth(PlayersData[i]);
                        ShootCtr.ShootKeyDown(this, i);
                    }
                    else if (i == ControlPlayerID)//����Ώ�
                    {
                        MoveCtr.MoveBottonUse(this);
                        ShootCtr.ShootKeyDown(this, i);
                        if (PlayersData[i].IsJump)
                        {
                            Jump(i);
                        }

                        float limittime = -1, maxtime = -1;

                        switch (PlayersData[ControlPlayerID].SkillIDs[0])
                        {
                            case SkillID.Cut:
                                limittime = SkillDataCtr.CutLimitTime;
                                maxtime = SkillDataCtr.MaxLimitTime;
                                SkillCtr.CheakSKillOver(limittime, maxtime);
                                break;
                            case SkillID.Stealth:
                                break;
                            case SkillID.Boom:
                                break;
                            default:
                                break;
                        }
                    }

                    JumpSmarsh(i);
                    SkillDataCtr.playerStealth(PlayersData[i]);
                    animators[1].SetFloat("vspeed", Players[i].GetComponent<Rigidbody2D>().velocity.y);

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

    public List<Player> GetAppPlayers()
    {
        List<Player> players = new List<Player>();

        for(int i = 0; i < PlayersData.Count; i++)
        {
            if(i < DisPlayPlayerNums)
            {
                players.Add(PlayersData[i]);
            }
            else
            {
                return players;
            }
        }

        return players;
    }

    //�X�N���v�g�擾
    void GetBottonCtr()
    {
        MoveCtr = FindObjectOfType<MoveBottonCtr>();
        ShootCtr = FindObjectOfType<ShootBottonCtr>();
        SkillCtr = FindObjectOfType<SkillBottonCtr>();
        ChaUICtr = FindObjectOfType<CharacterUIController>();
        BgCtr = FindObjectOfType<BGCtr>();
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
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshSpeed, Players[ID].GetComponent<Rigidbody2D>().velocity.y + SkillDataCtr.JumpSmarshDir.y * SkillDataCtr.JumpSmarshSpeed * 0.5f, 0.0f);
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
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(Players[ID].GetComponent<Rigidbody2D>().velocity.x - SkillDataCtr.JumpSmarshDir.x * SkillDataCtr.JumpSmarshAngular * Time.deltaTime, Players[ID].GetComponent<Rigidbody2D>().velocity.y - SkillDataCtr.JumpSmarshDir.y * 0.5f * SkillDataCtr.JumpSmarshAngular * Time.deltaTime, 0.0f);
            }
            else
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -0.01f, 0.0f);
            }
            if (SkillDataCtr.JumpSmarsh())
            {
                Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -0.01f, 0.0f);
            }
        }
    }

    public void ChangePlayer()
    {
        //�f�[�^�������l�ɖ߂�
        Timer = 0.0f;

        for (int i = 0; i < Players.Count; i++)
        {
            if (i < DisPlayPlayerNums)
            {
                PlayersData[i].HP = PlayersData[i].MaxHP;
                PlayersData[i].IsJump = false;
                PlayersData[i].IsAlive = true;
                PlayersData[i].ShootTimer = 0.0f;
                PlayersData[i].ShotTimes = 0;
                PlayersData[i].ShootIntoCD = false;
                PlayersData[i].CanShoot = false;
                PlayersData[i].EnableMoveJump = true;
                PlayersData[i].EnableMoveJump2 = true;
                Players[i].GetComponentInChildren<LostLifeCtr>().LostLifeReset();
                Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
                if(i == ControlPlayerID)
                {
                    //�ۑ��f�[�^�̍X�V
                    PlayersData[i].SavedBehaviour = new PlayerBehaviourData(PlayersData[i].RecordBehaviour);
                }
                else
                {
                    for (int j = 0; j < PlayersData[i].SavedBehaviour.GetBehaviourData().Count; j++)
                    {
                        PlayersData[i].SavedBehaviour.GetBehaviourData()[j].Used = false;
                    }
                }

                //�L�^�f�[�^�̍폜
                PlayersData[i].RecordBehaviour.ClearData();

                Players[i].SetActive(true);
            }
            else
            {
                Players[i].SetActive(false);
            }
        }

        ChaUICtr.ChangeHP(PlayersData[ControlPlayerID].PlayerID);

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();
        ShootCtr.ShootCDTimer = 0.0f;

        SkillCtr.SkillKeyDown = 0;
        SkillCtr.SkillTimer = 0.0f;
        SkillCtr.SkillCDTimer = 0.0f;
        SkillCtr.SkillIntoCD = false;
        SkillDataCtr.EnableUseCut = true;
        SkillDataCtr.UseCut = false;
        SkillDataCtr.StealthReset();
        SkillDataCtr.BoomReset();
        ControlPlayerID++;
        ControlPlayerID %= Players.Count;

        ChaUICtr.ChangeUI(ControlPlayerID);
        BgCtr.Reset();
        //�V�����L�^��������
        StartBehaviourRecord = true;
    }

    public void PlayerWithoutLoop()
    {
        Timer = 0.0f;

        for (int i = 0; i < Players.Count; i++)
        {
            if (i < DisPlayPlayerNums)
            {
                PlayersData[i].HP = PlayersData[i].MaxHP;
                PlayersData[i].IsJump = false;
                PlayersData[i].IsAlive = true;
                PlayersData[i].ShootTimer = 0.0f;
                PlayersData[i].ShotTimes = 0;
                PlayersData[i].ShootIntoCD = false;
                PlayersData[i].CanShoot = false;
                PlayersData[i].EnableMoveJump = true;
                PlayersData[i].EnableMoveJump2 = true;
                Players[i].GetComponentInChildren<LostLifeCtr>().LostLifeReset();
                Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
                for (int j = 0; j < PlayersData[i].SavedBehaviour.GetBehaviourData().Count; j++)
                {
                    PlayersData[i].SavedBehaviour.GetBehaviourData()[j].Used = false;
                }
                PlayersData[i].RecordBehaviour.ClearData();

                Players[i].SetActive(true);
            }
            else
            {
                Players[i].SetActive(false);
            }
        }

        ChaUICtr.ChangeHP(PlayersData[ControlPlayerID].PlayerID);

        for (int i = 0; i < ShootCtr.m_BulletsList.Count; i++)
        {
            GameObject.Destroy(ShootCtr.m_BulletsList[i]);
        }

        ShootCtr.m_BulletsList.Clear();
        ShootCtr.ShootCDTimer = 0.0f;

        SkillCtr.SkillKeyDown = 0;
        SkillCtr.SkillTimer = 0.0f;
        SkillCtr.SkillCDTimer = 0.0f;
        SkillCtr.SkillIntoCD = false;
        SkillDataCtr.EnableUseCut = true;
        SkillDataCtr.UseCut = false;
        SkillDataCtr.StealthReset();
        SkillDataCtr.BoomReset();
        BgCtr.Reset();

        StartBehaviourRecord = true;
    }


    public float CheakSkillCD()
    {
        float rate = 0.0f;
        switch (PlayersData[ControlPlayerID].SkillIDs[0])
        {
            case SkillID.JUMPSMARSH:
                if (SkillDataCtr.UseJumpSmarsh)
                {
                    rate = 1.0f;
                }
                else
                {
                    rate = 0.0f;
                }
                break;
            case SkillID.Cut:
                rate = 1.0f - SkillCtr.SkillCDTimer / SkillCtr.SkillCD;
                if (!SkillDataCtr.UseCut)
                {
                    rate = 0;
                }
                break;
            case SkillID.Stealth:
                rate = 1.0f - SkillDataCtr.StealthCDTimer / SkillDataCtr.StealthCDTime;
                if (!SkillDataCtr.UseStealth && !SkillCtr.SkillIntoCD)
                {
                    rate = 0;
                }
                break;
            case SkillID.Boom:
                rate = 1.0f - SkillDataCtr.BoomCDTimer / SkillDataCtr.BoomCD;
                if (SkillDataCtr.EnableUseBoom && PlayersData[ControlPlayerID].CanShoot)
                {
                    rate = 0;
                }
                break;
            default:
                break;
        }
        return rate;
    }

    public float CheakShootCD()
    {
        float rate = 0.0f;

        rate = 1.0f - PlayersData[ControlPlayerID].ShootTimer / PlayersData[ControlPlayerID].ShootCD;
        if (PlayersData[ControlPlayerID].CanShoot && !PlayersData[ControlPlayerID].ShootIntoCD)
        {
            rate = 0;
        }

        return rate;
    }

    public bool BreakStealth(int ID)
    {

        if (PlayersData[ID].SkillIDs[0] == SkillID.Stealth && SkillDataCtr.UseStealth)
        {
            return true;
            //SkillDataCtr.UseStealth = false;
            //PlayersData[ID].EnableMoveJump2 = true;
            //if (ID == ControlPlayerID)
            //{
            //    SkillCtr.SkillIntoCD = true;
            //}
        }
        return false;
    }

    public void AddDisplayPlayer()
    {
        DisPlayPlayerNums = Mathf.Min(DisPlayPlayerNums + 1, Players.Count);
    }
}
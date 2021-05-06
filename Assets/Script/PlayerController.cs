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
        
        //������
        for (int i = 0; i<Players.Count;i++)
        {
            PlayersData.Add(Players[i].GetComponent<Player>());//�L�����N�^�[�f�[�^�擾
            PlayersData[i].PlayerID = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!StartFallBack)
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
                                break;
                            case 5://�V���[�g�����A���ڃV���[�g����
                                ShootCtr.ShootKeyDown(this, savedata[i].PlayerID, savedata[i].ShootDir);
                                break;
                            default:
                                break;
                        }
                        //�ۑ��f�[�^�̑Ή��L�[�ԍ���-1�ŁA���������f�[�^�𖳌�������
                        savedata[i].BottonID = -1;
                    }
                }
            }

            //�S�Ẵv���C���[�̓������X�V����
            for(int i =0; i<Players.Count;i++)
            {
                if(PlayersData[i].IsAlive)
                {
                    //�񑀍�Ώ�
                    if (i != ControlPlayerID)
                    {
                        Vector2 pos = Players[i].GetComponent<Transform>().position;
                        Players[i].GetComponent<Transform>().position = new Vector2(pos.x + PlayersData[i].IsMove * PlayersData[i].MoveSpeed * Time.deltaTime, pos.y);
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
            //�f�[�^�������l�ɖ߂�
            Timer = 0.0f;
            for (int i = 0; i < Players.Count; i++)
            {
                PlayersData[i].IsJump =false;
                PlayersData[i].IsAlive = true;
                Players[i].GetComponent<Transform>().position = PlayersData[i].StartPoStartPositon;
                Players[i].SetActive(true);
            }

            for(int i = 0; i<ShootCtr.m_BulletsList.Count;i++)
            {
                GameObject.Destroy(ShootCtr.m_BulletsList[i]);
            }

            ShootCtr.m_BulletsList.Clear();

            //����ΏەύX
            ControlPlayerID++;
            ControlPlayerID %= Players.Count;

            StartFallBack = false;

            //�ۑ��f�[�^�̍X�V
            SavedBehaviour = new PlayerBehaviourData(RecordBehaviour);

            //�L�^�f�[�^�̍폜
            RecordBehaviour.ClearData();

            //�V�����L�^��������
            StartBehaviourRecord = true;
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
        if(PlayersData[ID].JumpedTimes < 1)
        {
            Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector2(Players[ID].GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(2 * 9.81f * Players[ID].GetComponent<Rigidbody2D>().gravityScale * JumpFocre * Players[ID].GetComponent<Player>().JumpMass));
        }
        else
        {
            Players[ID].GetComponent<Rigidbody2D>().velocity = new Vector2(Players[ID].GetComponent<Rigidbody2D>().velocity.x, Mathf.Sqrt(9.81f * Players[ID].GetComponent<Rigidbody2D>().gravityScale * JumpFocre * Players[ID].GetComponent<Player>().JumpMass));
        }
        PlayersData[ID].JumpedTimes+=1;
        PlayersData[ID].IsJump = false;
    }

    public void ToNextIsDown(InputAction.CallbackContext obj)
    {
        IsDead = true;
    }

    public void SetScale(int ID,Vector3 _Scale)
    {
        PlayersData[ID].PlayersForward = _Scale;
        Players[ID].GetComponent<Transform>().localScale = _Scale;
    }
}

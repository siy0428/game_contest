using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //!!!!�@���E����̂́@U�@�L�[��

    //�v���C���[���X�g
    public List<GameObject> Players = new List<GameObject>();

    public List<Vector3> PlayersForward = new List<Vector3>();

    //�����삵�Ă���v���C���[�ԍ�
    public int ControlPlayerID = 0;

    //�s���L�^���[�h
    public bool StartBehaviourRecord = true;

    //�o�b�N�A�b�v���[�h(���g���Ă��Ȃ��A�����̕����t���O�Ƃ��āA�g���Ă�)
    public bool StartFallBack = false;

    //�o�b�N�A�b�v�̃X�s�[�h�{��(���g���Ă��Ȃ��j
    public float FallBackSpeed = 5.0f;

    //�ړ����x
    public float MoveSpeed = 5.0f;

    //�ړ��̃X�N���v�g�|�C���g
    private MoveBottonCtr MoveCtr;

    //�V���[�g�̃X�N���v�g�|�C���g
    private ShootBottonCtr ShootCtr;

    //�v���C���[�N���X����Ă��Ȃ��̂ŁA�K�v�̋@�\�����̂܂܁A���X�g�ɂ��������
    //�W�����v��ԃ��X�g
    public List<bool> IsJump = new List<bool>();

    //���̃��[�v���̃^�C�}�[
    public float Timer = 0.0f;

    //�ǂݍ��ݗp�f�[�^
    public PlayerBehaviourData SavedBehaviour = new PlayerBehaviourData();

    //�L�^�p�f�[�^
    public PlayerBehaviourData RecordBehaviour = new PlayerBehaviourData();

    //������ԃ��X�g
    public List<bool> IsAlive = new List<bool>();

    //�o�b�N�A�b�v�p�����ʒu���X�g
    public List<Vector2> StartPositon = new List<Vector2>();

    //�ړ���ԃ��X�g
    public List<int> IsMove = new List<int>();

    //�W�����v�Ɏg���������̗�
    public float JumpFocre = 30000.0f;

    //�o���b�g�̃C���X�^���X�T���v��
    public GameObject Bullet;

    //�o���b�g�̃X�s�[�h
    public float BulletSpeed = 150.0f;

    private bool IsDead = false;

    // Start is called before the first frame update
    public void Start()
    {
        //�X�N���v�g�擾
        GetBottonCtr();
        
        //������
        for (int i = 0; i<Players.Count;i++)
        {
            IsJump.Add(false);
            IsAlive.Add(true);
            IsMove.Add(0);
            StartPositon.Add(Players[i].GetComponent<Transform>().position);
        }

        PlayersForward.Add(new Vector3(1.0f, 1.0f, 0.0f));
        PlayersForward.Add(new Vector3(-1.0f, 1.0f, 0.0f));
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
                                IsMove[savedata[i].PlayerID] = -1;
                                PlayersForward[i] = new Vector3(-1.0f, 1.0f, 0.0f);
                                break;
                            case 1:
                                IsMove[savedata[i].PlayerID] = 0;
                                break;
                            case 2:
                                IsMove[savedata[i].PlayerID] = 1;
                                PlayersForward[i] = new Vector3(1.0f, 1.0f, 0.0f);
                                break;
                            case 3:
                                IsMove[savedata[i].PlayerID] = 0;
                                break;
                            case 4:
                                IsJump[savedata[i].PlayerID] = true;
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
                //�񑀍�Ώ�
                if(i!= ControlPlayerID)
                {
                    Vector2 pos = Players[i].GetComponent<Transform>().position;
                    Players[i].GetComponent<Transform>().position = new Vector2(pos.x + IsMove[i] * MoveSpeed * Time.deltaTime, pos.y);
                    Players[i].GetComponent<Transform>().localScale = PlayersForward[i];
                    if(IsJump[i])
                    {
                        Jump(i);
                    }

                }
                else//����Ώ�
                {
                    MoveCtr.MoveBottonUse(this);
                    ShootCtr.ShootKeyDown(this,i);
                    if (IsJump[i])
                    {
                        Jump(i);
                    }
                }
            }

            //���̃��[�v�ւ̃L�[
            
            if (IsDead)
            {
                IsAlive[ControlPlayerID] = false;
                IsDead = false;
            }

            //�^�C�}�[�݉�
            Timer += Time.deltaTime;

            //���[�v�I������
            if (IsAlive[ControlPlayerID])
            {
                //����S������
                for (int i = 0; i < IsAlive.Count; i++)
                {
                    if (i != ControlPlayerID)
                    {
                        if (IsAlive[i])
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
                IsJump[i] =false;
                IsAlive[i] = true;
                Players[i].GetComponent<Transform>().position =  StartPositon[i];
            }

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
        Players[ID].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpFocre));
        IsJump[ID] = false;
    }

    public void ToNextIsDown(InputAction.CallbackContext obj)
    {
        IsDead = true;
    }

    public void SetScale(int ID,Vector3 _Scale)
    {
        PlayersForward[ID] = _Scale;
        Players[ID].GetComponent<Transform>().localScale = _Scale;
    }
}

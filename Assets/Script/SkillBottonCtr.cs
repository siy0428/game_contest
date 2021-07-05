using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SkillBottonCtr : MonoBehaviour
{
    //スキル行動データ
    BehaviorData KeySkill = new BehaviorData();

    ShootBottonCtr Sbc; 

    public int SkillKeyDown = 0;//0　押していない　1押した　-1離した

    public float SkillTimer = 0.0f; //押した時間

    public float SkillCD = 3.0f;

    public float SkillCDTimer = 0.0f;

    public bool SkillIntoCD = false;

    PlayerController _PlayerCtr;

    CharacterUIController ChaUICtr;

    public void SkillBottonIsDown(InputAction.CallbackContext obj)
    {
        SkillKeyDown = 1;
        DownKeyRecord();
        if(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0] == SkillID.Cut)
        {
            _PlayerCtr.SkillDataCtr.EnableUseCut = false;
        }
        if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0] == SkillID.Stealth)
        {
            _PlayerCtr.SkillDataCtr.UseStealth = true;
            _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump2 = false;
            _PlayerCtr.SkillDataCtr.EnableUseStealth = false;
        }
        if(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0] == SkillID.Boom && _PlayerCtr.SkillDataCtr.EnableUseBoom && Sbc.GetCanShot(_PlayerCtr.ControlPlayerID))
        {
            Debug.Log("ASF");
            _PlayerCtr.SkillDataCtr.EnableUseBoom = false;
            Sbc.ShootKeyDown_Skill(_PlayerCtr, _PlayerCtr.ControlPlayerID, _PlayerCtr.SkillDataCtr.KumaBoomBulletObj, _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].ShootPos);
            SkillIntoCD = true;
        }
    }

    public void SkillBottonIsDowning(InputAction.CallbackContext obj)
    {
    }


    public void SkillBottonIsUp(InputAction.CallbackContext obj)
    {
        SkillKeyDown = -1;
        if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0] == SkillID.Cut)
        {
            _PlayerCtr.SkillDataCtr.UseCut = true;
        }

        if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0] == SkillID.Stealth)
        {
            if (_PlayerCtr.SkillDataCtr.UseStealth)
            {
                UpKeyRecord();
                SkillIntoCD = true;
                _PlayerCtr.SkillDataCtr.UseStealth = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sbc = FindObjectOfType<ShootBottonCtr>();
        _PlayerCtr = FindObjectOfType<PlayerController>();

        ChaUICtr = FindObjectOfType<CharacterUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SkillKeyDown == 1)
        {
            float t = Time.deltaTime;
            SkillTimer += t;
           
            if (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0] == SkillID.Stealth)
            {
                SkillIntoCD = _PlayerCtr.SkillDataCtr.Stealth(SkillTimer, t);
                if(SkillIntoCD)
                {
                    SkillKeyDown = 0;
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump2 = true;
                }
            }           
        }

        switch (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0])
        {
            case SkillID.JUMPSMARSH:
                if (SkillIntoCD && SkillKeyDown == 0)
                {
                    SkillCD = FindObjectOfType<SkillData>().GetSkillCDTime(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0]);

                    SkillCDTimer += Time.deltaTime;
                    if (SkillCDTimer >= SkillCD)
                    {
                        SkillIntoCD = false;
                        SkillCDTimer = 0.0f;
                    }
                }
                break;
            case SkillID.Cut:
                if (SkillIntoCD && SkillKeyDown == 0)
                {
                    SkillCD = FindObjectOfType<SkillData>().GetSkillCDTime(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0]);

                    SkillCDTimer += Time.deltaTime;
                    if (SkillCDTimer >= SkillCD)
                    {
                        SkillIntoCD = false;
                        SkillCDTimer = 0.0f;
                        _PlayerCtr.SkillDataCtr.EnableUseCut = true;
                        _PlayerCtr.SkillDataCtr.UseCut = false;
                    }
                }
                break;
            case SkillID.Stealth:
                SkillIntoCD = _PlayerCtr.SkillDataCtr.StealthCDFunc(SkillIntoCD);
                if (!_PlayerCtr.SkillDataCtr.UseStealth)
                {
                    _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].EnableMoveJump2 = true;
                }

                break;
            case SkillID.Boom:
                SkillIntoCD = _PlayerCtr.SkillDataCtr.BoomCDFunc(SkillIntoCD);
                break;
            default:
                break;
        }


        ChaUICtr.CDMaskUpdate(1);
    }

    public bool CheakSKillOver( float _LimitTime, float _MaxLimit = -1.0f)
    {
        bool ans = false;


        if (_LimitTime > 0)
        {
            //最大制限時間を超えた、また、制限時間を超えてかつスキルキーを離した         
            if(SkillTimer >= _MaxLimit || (SkillTimer >= _LimitTime && SkillKeyDown == -1))
            {
                ans = true;
            }        
        }


        if (ans)
        {
            UpKeyRecord();
            Sbc.ShootKeyDown_Skill(_PlayerCtr, _PlayerCtr.ControlPlayerID, _PlayerCtr.SkillDataCtr.CutBulletObj, new Vector2(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.x, _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.y));
            SkillTimer = 0.0f;
            SkillKeyDown = 0;
            SkillIntoCD = true;
        }
        else
        {
            //制限時間を超えて、最大制限時間を超えてない場合、Timerを累加し続いていく
            if (SkillKeyDown == -1 || SkillKeyDown == 1)
            {
                SkillTimer += Time.deltaTime;
            }
        }

        return ans;
    }
    
    private void DownKeyRecord()
    {
        Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
        switch (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0])
        {
            case SkillID.Cut:
                KeySkill.BottonID = 61;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.ShootDir = new Vector2(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.x, _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.y);
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                animators[0].SetBool("isKamae", true);
                animators[1].SetBool("isKamae", true);
                break;
            case SkillID.Stealth:
                KeySkill.BottonID = 62;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                break;
            case SkillID.Boom:
                KeySkill.BottonID = 63;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.ShootDir = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].ShootPos;
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                break;
            default:
                break;
        }
        _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeySkill);
    }

    private void UpKeyRecord()
    {
        Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
        switch (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0])
        {
            case SkillID.Cut:
                KeySkill.BottonID = 66;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.ShootDir = new Vector2(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.x, _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.y);
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                FindObjectOfType<SkillData>().UseCut = true;
                animators[0].SetBool("isKamae", false);
                animators[1].SetBool("isKamae", false);
                break;
            case SkillID.Stealth:
                KeySkill.BottonID = 67;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                break;
            case SkillID.Boom:
                KeySkill.BottonID = 68;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.ShootDir = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].ShootPos;
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;

                break;
            default:
                break;
        }
        _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].RecordBehaviour.AddBehaviour(KeySkill);
    }
}
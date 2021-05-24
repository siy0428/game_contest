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

    public void SkillBottonIsDown(InputAction.CallbackContext obj)
    {
        SkillKeyDown = 1;
        DownKeyRecord();
        _PlayerCtr.SkillDataCtr.EnableUseCut = false;
    }

    public void SkillBottonIsDowning(InputAction.CallbackContext obj)
    {
        if(SkillKeyDown == 1)
        {
            SkillTimer += Time.deltaTime;
        }
    }


    public void SkillBottonIsUp(InputAction.CallbackContext obj)
    {
        SkillKeyDown = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Sbc = FindObjectOfType<ShootBottonCtr>();
        _PlayerCtr = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SkillIntoCD && SkillKeyDown == 0)
        {
            SkillCDTimer += Time.deltaTime;
            if (SkillCDTimer >= SkillCD)
            {
                SkillIntoCD = false;
                SkillCDTimer = 0.0f;
                _PlayerCtr.SkillDataCtr.EnableUseCut = true;
            }
        }
    }

    public void UseSkill(int ID)
    {
        if (_PlayerCtr.StartBehaviourRecord)
        {
            if (ID == _PlayerCtr.ControlPlayerID)
            {
                SkillCD = _PlayerCtr.SkillDataCtr.GetSkillCDTime(_PlayerCtr.PlayersData[ID].SkillIDs[0]);
            }
        }
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
            if (SkillKeyDown == 1)
            {
                SkillTimer += Time.deltaTime;
            }
        }

        return ans;
    }
    
    private void DownKeyRecord()
    {
        switch (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0])
        {
            case SkillID.Cut:
                KeySkill.BottonID = 61;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.ShootDir = new Vector2(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.x, _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.y);
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                break;
            default:
                break;
        }

    }

    private void UpKeyRecord()
    {
        switch (_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].SkillIDs[0])
        {
            case SkillID.Cut:
                KeySkill.BottonID = 66;
                KeySkill.StartTime = _PlayerCtr.Timer;
                KeySkill.ShootDir = new Vector2(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.x, _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].transform.localScale.y);
                KeySkill.Used = false;
                KeySkill.PlayerID = _PlayerCtr.ControlPlayerID;
                break;
            default:
                break;
        }
    }
}
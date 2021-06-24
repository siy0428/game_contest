using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID
{
    NONE,
    JUMPSMARSH,
    Cut,
    Stealth
}


public class SkillData : MonoBehaviour
{
    //JumpSarsh
    public float JumpSmarshSpeed = 10.0f;
    public float JumpSmarshTime = 0.5f;
    public float JumpSmarshTimer = 0.0f;
    public Vector2 JumpSmarshDir = new Vector2();
    public bool UseJumpSmarsh = false;
    public float JumpSmarshAngular = 5.0f;

    //‹‡
    public float CutCD = 3.0f;
    public float CutLimitTime = 1.0f;
    public float MaxLimitTime = 3.0f;
    public bool UseCut = false;
    public Vector3 CutFixPosition;
    public bool EnableUseCut = true;
    public GameObject CutBulletObj;

    //g‰B‚·
    public bool UseStealth = false;
    public bool EnableUseStealth = true;
    public float StealthCDTimer = 0.0f;
    public float StealthCDBasicTime = 0.5f;
    public float StealthCDTime = 0.5f;
    public float StealthMaxLimitTime = 5.0f;
    public float StealthCDrate = 2.0f;
    public float StealthTime = 0.0f;
    
    public bool JumpSmarsh()
    {
        if(UseJumpSmarsh)
        {
            if(JumpSmarshTimer >= JumpSmarshTime)
            {
                UseJumpSmarsh = false;
                JumpSmarshTimer = 0.0f;
                JumpSmarshDir = new Vector2(0,0);
                return true;
            }

            JumpSmarshTimer += Time.deltaTime;         
        }
        return false;
    }

    public void playerStealth(Player _player)
    {
        if(_player.SkillIDs[0] == SkillID.Stealth)
        {
            SpriteRenderer[] sr = _player.GetComponentsInChildren<SpriteRenderer>();

            if(UseStealth)
            {
                for(int i = 0; i<2;i++)
                {
                    Color color = sr[i].color;
                    color = new Color(color.r, color.g, color.b, 100.0f/255.0f);
                    sr[i].color = color;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Color color = sr[i].color;
                    color = new Color(color.r, color.g, color.b, 1.0f);
                    sr[i].color = color;
                }
            }
        }
    }

    public bool Stealth(float time,float dt)
    {
        StealthTime = time;
        bool res_intocd = false;
        if(UseStealth)
        {
            if (StealthTime < StealthMaxLimitTime)
            {
                StealthCDTime += StealthCDrate * dt;
            }
            else
            {
                UseStealth = false;
                res_intocd = true;
            }
        }
        return res_intocd;
    }

    public bool StealthCDFunc(bool intocd)
    {
        bool res = intocd;
        if(res)
        {
            StealthCDTimer += Time.deltaTime;
            if(StealthCDTimer >= StealthCDTime)
            {
                StealthCDTimer = 0.0f;
                res = false;
                EnableUseStealth = true;
                StealthCDTime = StealthCDBasicTime;
            }
        }
        return res;
    }

    public void StealthReset()
    {
        StealthCDTime = StealthCDBasicTime;
        StealthCDTimer = 0.0f;
        UseStealth = false;
        EnableUseStealth = true;
        StealthTime = 0.0f;
    }

    //
    public float GetSkillCDTime(SkillID _skillID)
    {
        float value = 0.0f;
        switch (_skillID)
        {
            case SkillID.JUMPSMARSH:
                break;
            case SkillID.Cut:
                value = CutCD;
                break;
            case SkillID.Stealth:
                value = StealthCDTime;
                break;
            default:
                break;
        }

        return value;
    }
}
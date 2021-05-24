using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID
{
    NONE,
    JUMPSMARSH,
    Cut
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

    //public float 
    
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

    //
    public float GetSkillCDTime(SkillID _skillID)
    {
        float value = 0.0f;
        switch (_skillID)
        {
            case SkillID.NONE:
                break;
            case SkillID.JUMPSMARSH:
                break;
            case SkillID.Cut:
                value = CutCD;
                break;
            default:
                break;
        }

        return value;
    }
}
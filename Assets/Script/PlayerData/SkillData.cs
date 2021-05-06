using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID
{
    NONE,
    ATTACK,
    JUMPSMARSH
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
    public void JumpSmarsh()
    {
        if(UseJumpSmarsh)
        {
            if(JumpSmarshTimer >= JumpSmarshTime)
            {
                UseJumpSmarsh = false;
                JumpSmarshTimer = 0.0f;
                JumpSmarshDir = new Vector2(0,0);
                return;
            }

            JumpSmarshTimer += Time.deltaTime;         
        }
    }
    //
}

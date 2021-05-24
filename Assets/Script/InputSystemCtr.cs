using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemCtr : MonoBehaviour
{
    public GameObject m_InputObj;

    private PlayerInput m_Input;

    private InputAction m_Move;

    private InputAction m_Jump;

    private InputAction m_Shoot;

    //テスト用だけ、次のループへ
    //キーボードのUキー
    private InputAction m_ToNextLoop;

    private InputAction m_Skill;

    private MoveBottonCtr m_MoveCtr;

    private ShootBottonCtr m_ShootCtr;

    private PlayerController m_Player;

    private SkillBottonCtr m_SkillCtr;
    public void Awake()
    {
        m_Input = m_InputObj.transform.GetComponent<PlayerInput>();
        m_MoveCtr = FindObjectOfType<MoveBottonCtr>();
        m_ShootCtr = FindObjectOfType<ShootBottonCtr>();
        m_SkillCtr = FindObjectOfType<SkillBottonCtr>();
        m_Player = FindObjectOfType<PlayerController>();

        InputActionMap actionMap = m_Input.currentActionMap;

        m_Move = actionMap["Move"];

        m_Jump = actionMap["Jump"];
        m_Jump.started += m_MoveCtr.JumpBottonIsDown;

        m_Shoot = actionMap["Shoot"];
        m_Shoot.started += m_ShootCtr.ShootBottonIsDown;

        m_ToNextLoop = actionMap["ToNextLoop"];
        m_ToNextLoop.started += m_Player.ToNextIsDown;

        m_Skill = actionMap["Skill"];
        m_Skill.started += m_SkillCtr.SkillBottonIsDown;
        m_Skill.performed += m_SkillCtr.SkillBottonIsDowning;
        m_Skill.canceled += m_SkillCtr.SkillBottonIsUp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveCtr.LeftStickIsUse(m_Move);
    }
}

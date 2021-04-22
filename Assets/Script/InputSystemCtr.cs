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

    private MoveBottonCtr m_MoveCtr;

    private ShootBottonCtr m_ShootCtr;

    private Player m_Player;
    public void Awake()
    {
        m_Input = m_InputObj.transform.GetComponent<PlayerInput>();
        m_MoveCtr = FindObjectOfType<MoveBottonCtr>();
        m_ShootCtr = FindObjectOfType<ShootBottonCtr>();
        m_Player = FindObjectOfType<Player>();

        InputActionMap actionMap = m_Input.currentActionMap;

        m_Move = actionMap["Move"];

        m_Jump = actionMap["Jump"];
        m_Jump.started += m_MoveCtr.JumpBottonIsDown;

        m_Shoot = actionMap["Shoot"];
        m_Shoot.started += m_ShootCtr.ShootBottonIsDown;

        m_ToNextLoop = actionMap["ToNextLoop"];
        m_ToNextLoop.started += m_Player.ToNextIsDown;
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

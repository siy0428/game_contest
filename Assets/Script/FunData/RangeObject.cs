using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeObject : MonoBehaviour
{
    /// <summary>
    /// �G�f�B�^��ŕҏW�\�ȕϐ�
    /// </summary>
    [SerializeField, Range(0.0f, 360.0f)]
    private float m_Angle = 0.0f;
    [SerializeField]
    private float m_Radius = 0.0f;
    [SerializeField, Range(3, 32)]
    private int m_TriangleCount = 12;
    [SerializeField]
    private Color m_Color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    [SerializeField]
    private float m_MaxRotSpeed;

    /// <summary>
    /// �v���C�x�[�g�ϐ�
    /// </summary>
    private float m_RotSpeed = 1.0f;
    private float m_RotateAngle = 0.0f;
    private Vector3 m_Dir = Vector3.up;
    private List<GameObject> WallList = new List<GameObject>();
    private InputAction arrow;

    /// <summary>
    /// ���X�N���v�g����Q�Ɨp���\�b�h
    /// </summary>
    public float Angle { get { return m_Angle; } }
    public float Radius { get { return m_Radius; } }
    public int TriangleCount { get { return m_TriangleCount; } }
    public Color Color { get { return m_Color; } }
    public float RotateAngle { get { return m_RotateAngle; } }

    void Start()
    {
        PlayerInput _input = FindObjectOfType<PlayerInput>();
        InputActionMap actionMap = _input.currentActionMap;
        arrow = actionMap["Fun"];
    }

    // Update is called once per frame
    void Update()
    {
        var v = arrow.ReadValue<Vector2>();

        //���͂��������̎擾
        InputDir(v);

        //��]
        Rotation();

        //�����蔻��̃T�C�Y�ύX
        ColliderFlexible();
    }

    /// <summary>
    /// ���͂��������̎擾
    /// </summary>
    void InputDir(Vector2 input)
    {
        //�����
        if (input.y > 0.5f)
        {
            m_Dir = Vector3.up;
            m_RotSpeed = m_MaxRotSpeed;
        }
        //������
        else if (input.y < -0.5f)
        {
            m_Dir = Vector3.down;
            m_RotSpeed = m_MaxRotSpeed;
        }
        //�E����
        else if (input.x > 0.5f)
        {
            //�������������Ă�����
            if (m_Dir == Vector3.left)
            {
                m_RotateAngle = 270.0f;
            }
            else
            {
                m_RotSpeed = m_MaxRotSpeed;
            }
            m_Dir = Vector3.right;
        }
        //������
        else if (input.x < -0.5f)
        {
            //�E�����������Ă�����
            if (m_Dir == Vector3.right)
            {
                m_RotateAngle = 90.0f;
            }
            else
            {
                m_RotSpeed = m_MaxRotSpeed;
            }
            m_Dir = Vector3.left;
        }

        m_Dir.Normalize();
    }

    /// <summary>
    /// ��̉�]
    /// </summary>
    void Rotation()
    {
        //�����Â�]������
        if (Vector3.Cross(this.transform.up, m_Dir).z > 0.0f)
        {
            m_RotateAngle += m_RotSpeed;
        }
        else if (Vector3.Cross(this.transform.up, m_Dir).z <= 0.0f)
        {
            m_RotateAngle -= m_RotSpeed;
        }

        //��]���x�̌���
        if (Vector3.Dot(this.transform.up, m_Dir) > 0.9f)
        {
            m_RotSpeed *= 0.9f;
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, m_RotateAngle);
    }

    /// <summary>
    /// �����蔻��̃T�C�Y�ύX
    /// </summary>
    void ColliderFlexible()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        collider.size = new Vector2(m_Radius * 2, m_Radius * 2);
    }

    /// <summary>
    /// 2D�̓����蔻��
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}
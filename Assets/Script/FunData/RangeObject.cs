using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFunHit
{
    public GameObject enemy;
    public float distance;

    public void Reset()
    {
        enemy = null;
        distance = 0.0f;
    }
}

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
    private float m_MaxRotSpeed;
    [SerializeField]
    private Material m_Material;
    [SerializeField]
    private PlayerController m_PlayerController;
    [SerializeField]
    private GameObject m_Player;

    /// <summary>
    /// �v���C�x�[�g�ϐ�
    /// </summary>
    private float m_RotSpeed = 1.0f;
    private float m_RotateAngle = 0.0f;
    private Vector3 m_Dir = Vector3.up;
    private InputAction arrow;
    private EnemyFunHit enemyFunHit;

    /// <summary>
    /// ���X�N���v�g����Q�Ɨp���\�b�h
    /// </summary>
    public float Angle { get { return m_Angle; } }
    public float Radius { get { return m_Radius; } }
    public int TriangleCount { get { return m_TriangleCount; } }
    public float RotateAngle { get { return m_RotateAngle; } }
    public Vector3 Direction { get { return m_Dir; } }

    void Start()
    {
        PlayerInput _input = FindObjectOfType<PlayerInput>();
        InputActionMap actionMap = _input.currentActionMap;
        arrow = actionMap["Fun"];

        enemyFunHit = new EnemyFunHit();
    }

    // Update is called once per frame
    void Update()
    {
        //���͂��������̎擾
        InputDir(arrow.ReadValue<Vector2>());

        //��]
        Rotation();

        //��̍X�V����
        FunUpdate();
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
    /// ��_�쐬
    /// </summary>
    /// <param name="angle">�p�x</param>
    /// <param name="triangleCount">���_��</param>
    /// <returns></returns>
    private Vector3[] CreateFanVertices(float angle, int triangleCount)
    {
        //�G���[�ݒ�
        if (angle <= 0.0f)
        {
            throw new System.ArgumentException(string.Format("�p�x�����������I angle={0}", angle));
        }
        if (triangleCount <= 0)
        {
            throw new System.ArgumentException(string.Format("�������������I triangleCount={0}", triangleCount));
        }

        angle = Mathf.Min(angle, 360.0f);

        var vertices = new List<Vector3>(triangleCount + 2);

        // �n�_
        vertices.Add(Vector3.zero);

        // Mathf.Sin()��Mathf.Cos()�Ŏg�p����̂͊p�x�ł͂Ȃ����W�A���Ȃ̂ŕϊ����Ă����B
        float radian = angle * Mathf.Deg2Rad;
        float startRad = -radian / 2;
        float incRad = radian / triangleCount;

        for (int i = 0; i < triangleCount + 1; ++i)
        {
            float currentRad = startRad + (incRad * i);

            Vector3 vertex = new Vector3(Mathf.Sin(currentRad), Mathf.Cos(currentRad), 0.0f);
            vertices.Add(vertex);
        }

        return vertices.ToArray();
    }

    /// <summary>
    /// �O�p�`�̍쐬
    /// </summary>
    /// <param name="angle">�p�x</param>
    /// <param name="triangleCount">���_��</param>
    /// <returns></returns>
    private Mesh CreateFunMesh(float angle, int triangleCount)
    {
        var mesh = new Mesh();

        var vertices = CreateFanVertices(angle, triangleCount);

        var triangleIndexes = new List<int>(triangleCount * 3);

        for (int i = 0; i < triangleCount; ++i)
        {
            triangleIndexes.Add(0);
            triangleIndexes.Add(i + 1);
            triangleIndexes.Add(i + 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndexes.ToArray();

        mesh.RecalculateNormals();

        return mesh;
    }

    /// <summary>
    /// ��̍X�V����
    /// </summary>
    private void FunUpdate()
    {
        var renderer = GetComponent<MeshRenderer>();
        int id = m_PlayerController.ControlPlayerID;

        //���삵�Ă��Ȃ��v���C���[�̐�͕`�悵�Ȃ�
        if (m_PlayerController.Players[id].gameObject.name != m_Player.name)
        {
            renderer.material = m_Material;
            renderer.enabled = false;
            return;
        }

        //��̕`��
        renderer.material = m_Material;
        renderer.enabled = true;

        //��b�V���̕`��
        DrawFunMesh();

        //�G�̐���蔻��
        EnemyFunCollision();
    }

    /// <summary>
    /// ��`���b�V���̕`��
    /// </summary>
    private void DrawFunMesh()
    {
        if (Radius <= 0.0f)
        {
            return;
        }

        Transform transform = this.transform;
        Vector3 scale = Vector3.one * Radius;

        this.transform.localScale = scale;

        if (Angle > 0.0f)
        {
            Mesh funMesh = CreateFunMesh(Angle, TriangleCount);

            var meshFilter = GetComponent<MeshFilter>();

            meshFilter.mesh = funMesh;
        }

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = m_Material;
    }

    /// <summary>
    /// �G�Ɛ�̓����蔻��
    /// </summary>
    void EnemyFunCollision()
    {
        //��̒��ň�ԋ߂��G�̏��̃��Z�b�g
        enemyFunHit.Reset();

        //�G�l�~�[�̎擾
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Player");

        //��ɓ������Ă���G�̎擾
        foreach (GameObject enemy in enemys)
        {
            //�G�̐F�𔒂ɐݒ�
            enemy.GetComponent<Renderer>().material.color = Color.white;

            //�G�̖��O�Ƒ��삵�Ă���v���C���[�������������玟�̏���
            if (enemy.name == m_Player.name)
            {
                continue;
            }

            //�G�ƃv���C���[�̃x�N�g��
            Vector3 dir = enemy.transform.position - this.transform.position;

            CircleCollider2D rad = enemy.GetComponent<CircleCollider2D>();
            Vector2 enemyPos = enemy.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = RotateAngle + (90.0f - Angle / 2);
            float endDeg = startDeg + Angle;
            float radius = Radius;
            bool funHit = MathUtils.IsInsideOfSector(enemyPos, center, startDeg, endDeg, radius, rad.radius);

            //�v���C���[����G�ɐL�т�x�N�g��
            RaycastHit2D hit = Physics2D.Linecast(transform.position, enemyPos, 1);
            Debug.DrawLine(transform.position, enemyPos, Color.red, 1);

            //null�łȂ����
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    continue;
                }
            }

            //��ɓ������Ă���ꍇ
            if (funHit)
            {
                Vector3 dist = enemyPos - center;
                //��r�Ώۂ��܂��Ȃ��ꍇ�b��œG�̃I�u�W�F�N�g���i�[
                if (!enemyFunHit.enemy)
                {
                    enemyFunHit.enemy = enemy;
                    enemyFunHit.distance = dist.magnitude;
                }
                else
                {
                    //���ݔ�r���Ă���G�Ƃ̋����̕����Z���ꍇ
                    if (dist.magnitude < enemyFunHit.distance)
                    {
                        enemyFunHit.enemy = enemy;
                        enemyFunHit.distance = dist.magnitude;
                    }
                }
            }

            //��͈͓��̓G�͐ԐF
            if (enemyFunHit.enemy)
            {
                enemyFunHit.enemy.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
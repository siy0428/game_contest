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
    private float Angle = 0.0f;
    [SerializeField]
    private float Radius = 0.0f;
    [SerializeField, Range(3, 32)]
    private int TriangleCount = 12;
    [SerializeField]
    private float MaxRotSpeed;
    [SerializeField]
    private Material Material;
    [SerializeField]
    private GameObject Player;

    /// <summary>
    /// �v���C�x�[�g�ϐ�
    /// </summary>
    private PlayerController pc;
    private ShootBottonCtr sbc;
    private float RotSpeed = 1.0f;
    private InputAction arrow;
    private float FinishAngle;

    /// <summary>
    /// ���X�N���v�g����Q�Ɨp���\�b�h
    /// </summary>
    public float GetAngle { get { return Angle; } }
    public float GetRadius { get { return Radius; } }
    public int GetTriangleCount { get { return TriangleCount; } }
    public float GetRotateAngle { get; private set; }
    public Vector3 Direction { get; private set; }

    void Start()
    {
        PlayerInput _input = FindObjectOfType<PlayerInput>();
        InputActionMap actionMap = _input.currentActionMap;
        arrow = actionMap["Move"];
        pc = FindObjectOfType<PlayerController>();
        sbc = FindObjectOfType<ShootBottonCtr>();

        Direction = Vector3.up;
        FinishAngle = 0.0f;
        GetRotateAngle = FinishAngle;
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
        //�Ȃɂ����͂��Ă��Ȃ��ꍇ�������s��Ȃ�
        input.Normalize();

        if (input.magnitude < 1.0f)
        {
            return;
        }

        //�����
        if (input.y > 0.5f)
        {
            Direction = Vector3.up;
            RotSpeed = MaxRotSpeed;
        }
        //������
        else if (input.y < -0.5f)
        {
            Direction = Vector3.down;
            RotSpeed = MaxRotSpeed;
        }
        //�E����
        else if (input.x > 0.5f)
        {
            //�������������Ă�����
            if (Direction == Vector3.left)
            {
                GetRotateAngle = 270.0f;
            }
            else
            {
                RotSpeed = MaxRotSpeed;
            }
            Direction = Vector3.right;
        }
        //������
        else if (input.x < -0.5f)
        {
            //�E�����������Ă�����
            if (Direction == Vector3.right)
            {
                GetRotateAngle = 90.0f;
            }
            else
            {
                RotSpeed = MaxRotSpeed;
            }
            Direction = Vector3.left;
        }

        Direction.Normalize();
    }

    /// <summary>
    /// ��̉�]
    /// </summary>
    void Rotation()
    {
        //�O�ς�Z�������m�F���āA�ŒZ��]�����ŏ����Â�]������
        if (Vector3.Cross(this.transform.up, Direction).z > 0.0f)
        {
            GetRotateAngle += RotSpeed;
        }
        else if (Vector3.Cross(this.transform.up, Direction).z <= 0.0f)
        {
            GetRotateAngle -= RotSpeed;
        }

        //�ڕW�̊p�x�ɒB������p�x���Œ�
        var cross = Vector3.Cross(Direction, Vector3.up);
        cross.z = (cross.z < 0) ? 1 : -1;
        FinishAngle = Vector3.Angle(Direction, Vector3.up) * cross.z;
        FinishAngle = (FinishAngle + 360) % 360;
        if (FinishAngle - MaxRotSpeed <= GetRotateAngle && FinishAngle + MaxRotSpeed >= GetRotateAngle)
        {
            GetRotateAngle = FinishAngle;
        }

        //�p�x�̒���
        GetRotateAngle %= 360.0f;
        GetRotateAngle = (GetRotateAngle + 360) % 360;

        //��]���x�̌���
        if (Vector3.Dot(this.transform.up, Direction) > 0.9f)
        {
            RotSpeed *= 0.9f;
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GetRotateAngle);
    }

    /// <summary>
    /// �����ݎg�p���Ă��Ȃ�
    /// �����蔻��̃T�C�Y�ύX
    /// </summary>
    void ColliderFlexible()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        collider.size = new Vector2(Radius * 2, Radius * 2);
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
        int id = pc.ControlPlayerID;

        //���삵�Ă��Ȃ��v���C���[�̐�͕`�悵�Ȃ�
        if (pc.Players[id].gameObject.name != Player.name)
        {
            renderer.material = Material;
            renderer.enabled = false;
            return;
        }

        //��̕`��
        renderer.material = Material;
        renderer.enabled = true;

        //��b�V���̕`��
        DrawFunMesh();

        GameObject hit_obj = FunCollision();

        //��ɓG�������Ă����ꍇ
        if (hit_obj)
        {
            sbc.SetCanShot(true);
            if (hit_obj.tag == "Player")
            {
                sbc.SetShotPos(hit_obj.transform.position + new Vector3(0.0f, 0.25f, 0.0f));
            } 
            else
            {
                sbc.SetShotPos(hit_obj.transform.position);
            }
        }
        //��ɓG�����Ȃ��ꍇ
        else
        {
            sbc.SetCanShot(false);
        }
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
        renderer.material = Material;
    }

    /// <summary>
    /// �G�Ɛ�̓����蔻��
    /// </summary>
    /// <returns>���������G��GameObject��Ԃ�(�Ȃ��ꍇ��NULL)</returns>
    public GameObject FunCollision()
    {
        GameObject hit_enemy = null;

        //�G�l�~�[�̎擾
        List<GameObject> objects = new List<GameObject>();
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //�G�̊i�[
        foreach (var enemy in enemys)
        {
            objects.Add(enemy);
        }
        //�v���C���[�̊i�[
        foreach (var player in players)
        {
            objects.Add(player);
        }

        //��ɓ������Ă���G�̎擾
        foreach (var obj in objects)
        {

            //�G�̖��O�Ƒ��삵�Ă���v���C���[�������������玟�̏���
            if (obj.name == Player.name)
            {
                continue;
            }

            //�G�ƃv���C���[�̃x�N�g��
            Vector3 dir = obj.transform.position - this.transform.position;

            CircleCollider2D rad = obj.GetComponent<CircleCollider2D>();
            Vector2 playerPos = obj.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = GetRotateAngle + (90.0f - Angle / 2);
            float endDeg = startDeg + Angle;
            float radius = Radius;
            bool funHit = MathUtils.IsInsideOfSector(playerPos, center, startDeg, endDeg, radius, rad.radius);

            //�v���C���[����G�ɐL�т�x�N�g��
            RaycastHit2D hit = Physics2D.Linecast(transform.position, playerPos, 1);
            //Debug.DrawLine(transform.position, playerPos, Color.red, 1);

            //null�łȂ����
            if (hit)
            {
                //�G�ƃv���C���[�̊Ԃɒn�ʂ�����Δ�����s��Ȃ�
                if (hit.collider.gameObject.tag == "Ground")
                {
                    continue;
                }
            }

            //��ɓ������Ă���ꍇ
            if (funHit)
            {
                Vector3 dist = playerPos - center;
                //��r�Ώۂ��܂��Ȃ��ꍇ�b��œG�̃I�u�W�F�N�g���i�[
                if (!hit_enemy)
                {
                    hit_enemy = obj;
                }
                else
                {
                    var dist2 = (Vector2)hit_enemy.transform.position - center;
                    //���ݔ�r���Ă���G�Ƃ̋����̕����Z���ꍇ
                    if (dist.magnitude < dist2.magnitude)
                    {
                        hit_enemy = obj;
                    }
                }
            }
        }

        return hit_enemy;
    }
}
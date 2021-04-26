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
    /// エディタ上で編集可能な変数
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
    /// プライベート変数
    /// </summary>
    private float m_RotSpeed = 1.0f;
    private float m_RotateAngle = 0.0f;
    private Vector3 m_Dir = Vector3.up;
    private InputAction arrow;
    private EnemyFunHit enemyFunHit;

    /// <summary>
    /// 他スクリプトから参照用メソッド
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
        //入力した方向の取得
        InputDir(arrow.ReadValue<Vector2>());

        //回転
        Rotation();

        //扇の更新処理
        FunUpdate();
    }

    /// <summary>
    /// 入力した方向の取得
    /// </summary>
    void InputDir(Vector2 input)
    {
        //上方向
        if (input.y > 0.5f)
        {
            m_Dir = Vector3.up;
            m_RotSpeed = m_MaxRotSpeed;
        }
        //下方向
        else if (input.y < -0.5f)
        {
            m_Dir = Vector3.down;
            m_RotSpeed = m_MaxRotSpeed;
        }
        //右方向
        else if (input.x > 0.5f)
        {
            //左方向を向いていたら
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
        //左方向
        else if (input.x < -0.5f)
        {
            //右方向を向いていたら
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
    /// 扇の回転
    /// </summary>
    void Rotation()
    {
        //少しづつ回転させる
        if (Vector3.Cross(this.transform.up, m_Dir).z > 0.0f)
        {
            m_RotateAngle += m_RotSpeed;
        }
        else if (Vector3.Cross(this.transform.up, m_Dir).z <= 0.0f)
        {
            m_RotateAngle -= m_RotSpeed;
        }

        //回転速度の減衰
        if (Vector3.Dot(this.transform.up, m_Dir) > 0.9f)
        {
            m_RotSpeed *= 0.9f;
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, m_RotateAngle);
    }

    /// <summary>
    /// 当たり判定のサイズ変更
    /// </summary>
    void ColliderFlexible()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        collider.size = new Vector2(m_Radius * 2, m_Radius * 2);
    }

    /// <summary>
    /// 扇頂点作成
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="triangleCount">頂点数</param>
    /// <returns></returns>
    private Vector3[] CreateFanVertices(float angle, int triangleCount)
    {
        //エラー設定
        if (angle <= 0.0f)
        {
            throw new System.ArgumentException(string.Format("角度がおかしい！ angle={0}", angle));
        }
        if (triangleCount <= 0)
        {
            throw new System.ArgumentException(string.Format("数がおかしい！ triangleCount={0}", triangleCount));
        }

        angle = Mathf.Min(angle, 360.0f);

        var vertices = new List<Vector3>(triangleCount + 2);

        // 始点
        vertices.Add(Vector3.zero);

        // Mathf.Sin()とMathf.Cos()で使用するのは角度ではなくラジアンなので変換しておく。
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
    /// 三角形の作成
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="triangleCount">頂点数</param>
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
    /// 扇の更新処理
    /// </summary>
    private void FunUpdate()
    {
        var renderer = GetComponent<MeshRenderer>();
        int id = m_PlayerController.ControlPlayerID;

        //操作していないプレイヤーの扇は描画しない
        if (m_PlayerController.Players[id].gameObject.name != m_Player.name)
        {
            renderer.material = m_Material;
            renderer.enabled = false;
            return;
        }

        //扇の描画
        renderer.material = m_Material;
        renderer.enabled = true;

        //扇メッシュの描画
        DrawFunMesh();

        //敵の扇当たり判定
        EnemyFunCollision();
    }

    /// <summary>
    /// 扇形メッシュの描画
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
    /// 敵と扇の当たり判定
    /// </summary>
    void EnemyFunCollision()
    {
        //扇の中で一番近い敵の情報のリセット
        enemyFunHit.Reset();

        //エネミーの取得
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Player");

        //扇に当たっている敵の取得
        foreach (GameObject enemy in enemys)
        {
            //敵の色を白に設定
            enemy.GetComponent<Renderer>().material.color = Color.white;

            //敵の名前と操作しているプレイヤーが同じだったら次の処理
            if (enemy.name == m_Player.name)
            {
                continue;
            }

            //敵とプレイヤーのベクトル
            Vector3 dir = enemy.transform.position - this.transform.position;

            CircleCollider2D rad = enemy.GetComponent<CircleCollider2D>();
            Vector2 enemyPos = enemy.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = RotateAngle + (90.0f - Angle / 2);
            float endDeg = startDeg + Angle;
            float radius = Radius;
            bool funHit = MathUtils.IsInsideOfSector(enemyPos, center, startDeg, endDeg, radius, rad.radius);

            //プレイヤーから敵に伸びるベクトル
            RaycastHit2D hit = Physics2D.Linecast(transform.position, enemyPos, 1);
            Debug.DrawLine(transform.position, enemyPos, Color.red, 1);

            //nullでなければ
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    continue;
                }
            }

            //扇に当たっている場合
            if (funHit)
            {
                Vector3 dist = enemyPos - center;
                //比較対象がまだない場合暫定で敵のオブジェクトを格納
                if (!enemyFunHit.enemy)
                {
                    enemyFunHit.enemy = enemy;
                    enemyFunHit.distance = dist.magnitude;
                }
                else
                {
                    //現在比較している敵との距離の方が短い場合
                    if (dist.magnitude < enemyFunHit.distance)
                    {
                        enemyFunHit.enemy = enemy;
                        enemyFunHit.distance = dist.magnitude;
                    }
                }
            }

            //扇範囲内の敵は赤色
            if (enemyFunHit.enemy)
            {
                enemyFunHit.enemy.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
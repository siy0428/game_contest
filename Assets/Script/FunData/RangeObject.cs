using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeObject : MonoBehaviour
{
    /// <summary>
    /// エディタ上で編集可能な変数
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
    /// プライベート変数
    /// </summary>
    private PlayerController pc;
    private ShootBottonCtr sbc;
    private float RotSpeed = 1.0f;
    private InputAction arrow;
    private float FinishAngle;

    /// <summary>
    /// 他スクリプトから参照用メソッド
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
        //なにも入力していない場合処理を行わない
        input.Normalize();

        if (input.magnitude < 1.0f)
        {
            return;
        }

        //上方向
        if (input.y > 0.5f)
        {
            Direction = Vector3.up;
            RotSpeed = MaxRotSpeed;
        }
        //下方向
        else if (input.y < -0.5f)
        {
            Direction = Vector3.down;
            RotSpeed = MaxRotSpeed;
        }
        //右方向
        else if (input.x > 0.5f)
        {
            //左方向を向いていたら
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
        //左方向
        else if (input.x < -0.5f)
        {
            //右方向を向いていたら
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
    /// 扇の回転
    /// </summary>
    void Rotation()
    {
        //外積のZ成分を確認して、最短回転方向で少しづつ回転させる
        if (Vector3.Cross(this.transform.up, Direction).z > 0.0f)
        {
            GetRotateAngle += RotSpeed;
        }
        else if (Vector3.Cross(this.transform.up, Direction).z <= 0.0f)
        {
            GetRotateAngle -= RotSpeed;
        }

        //目標の角度に達したら角度を固定
        var cross = Vector3.Cross(Direction, Vector3.up);
        cross.z = (cross.z < 0) ? 1 : -1;
        FinishAngle = Vector3.Angle(Direction, Vector3.up) * cross.z;
        FinishAngle = (FinishAngle + 360) % 360;
        if (FinishAngle - MaxRotSpeed <= GetRotateAngle && FinishAngle + MaxRotSpeed >= GetRotateAngle)
        {
            GetRotateAngle = FinishAngle;
        }

        //角度の調整
        GetRotateAngle %= 360.0f;
        GetRotateAngle = (GetRotateAngle + 360) % 360;

        //回転速度の減衰
        if (Vector3.Dot(this.transform.up, Direction) > 0.9f)
        {
            RotSpeed *= 0.9f;
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, GetRotateAngle);
    }

    /// <summary>
    /// ※現在使用していない
    /// 当たり判定のサイズ変更
    /// </summary>
    void ColliderFlexible()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        collider.size = new Vector2(Radius * 2, Radius * 2);
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
        int id = pc.ControlPlayerID;

        //操作していないプレイヤーの扇は描画しない
        if (pc.Players[id].gameObject.name != Player.name)
        {
            renderer.material = Material;
            renderer.enabled = false;
            return;
        }

        //扇の描画
        renderer.material = Material;
        renderer.enabled = true;

        //扇メッシュの描画
        DrawFunMesh();

        GameObject hit_obj = FunCollision();

        //扇に敵が入っていた場合
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
        //扇に敵がいない場合
        else
        {
            sbc.SetCanShot(false);
        }
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
        renderer.material = Material;
    }

    /// <summary>
    /// 敵と扇の当たり判定
    /// </summary>
    /// <returns>当たった敵のGameObjectを返す(ない場合はNULL)</returns>
    public GameObject FunCollision()
    {
        GameObject hit_enemy = null;

        //エネミーの取得
        List<GameObject> objects = new List<GameObject>();
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //敵の格納
        foreach (var enemy in enemys)
        {
            objects.Add(enemy);
        }
        //プレイヤーの格納
        foreach (var player in players)
        {
            objects.Add(player);
        }

        //扇に当たっている敵の取得
        foreach (var obj in objects)
        {

            //敵の名前と操作しているプレイヤーが同じだったら次の処理
            if (obj.name == Player.name)
            {
                continue;
            }

            //敵とプレイヤーのベクトル
            Vector3 dir = obj.transform.position - this.transform.position;

            CircleCollider2D rad = obj.GetComponent<CircleCollider2D>();
            Vector2 playerPos = obj.transform.position;
            Vector2 center = this.transform.position;
            float startDeg = GetRotateAngle + (90.0f - Angle / 2);
            float endDeg = startDeg + Angle;
            float radius = Radius;
            bool funHit = MathUtils.IsInsideOfSector(playerPos, center, startDeg, endDeg, radius, rad.radius);

            //プレイヤーから敵に伸びるベクトル
            RaycastHit2D hit = Physics2D.Linecast(transform.position, playerPos, 1);
            //Debug.DrawLine(transform.position, playerPos, Color.red, 1);

            //nullでなければ
            if (hit)
            {
                //敵とプレイヤーの間に地面があれば判定を行わない
                if (hit.collider.gameObject.tag == "Ground")
                {
                    continue;
                }
            }

            //扇に当たっている場合
            if (funHit)
            {
                Vector3 dist = playerPos - center;
                //比較対象がまだない場合暫定で敵のオブジェクトを格納
                if (!hit_enemy)
                {
                    hit_enemy = obj;
                }
                else
                {
                    var dist2 = (Vector2)hit_enemy.transform.position - center;
                    //現在比較している敵との距離の方が短い場合
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
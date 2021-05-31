using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootKeeper : MonoBehaviour
{

    public bool doShoot = false;

    public ShootBottonCtr shootbottonctr;

    private int ShootOriginID = -1;

    private Vector3 TargetPos;

    private Player shootOrigin;

    private bool isShot = false;

    public void SetParama(Vector3 _TargetPos, int ID)
    {
        TargetPos = _TargetPos;
        ShootOriginID = ID;
    }

    // Start is called before the first frame update
    void Start()
    {
        shootOrigin = transform.parent.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!doShoot && isShot)
        {
            isShot = false;
        }
        if (doShoot && !isShot)
        {
            // 弾（ゲームオブジェクト）の生成
            GameObject clone = Instantiate(shootOrigin.Bullet, shootOrigin.GetComponent<Transform>().position + shootOrigin.GetComponent<Player>().ShootFixPostion, Quaternion.identity);
            clone.GetComponent<BulletData>().SetTarget(TargetPos);    //弾の方向
            clone.GetComponent<BulletData>().SetShootPosition(new Vector3(clone.transform.position.x, clone.transform.position.y, 1.0f));
            clone.GetComponent<Collision>().PlayerID = ShootOriginID;
            shootbottonctr.m_BulletsList.Add(clone);

            isShot = true;
        }
    }
}

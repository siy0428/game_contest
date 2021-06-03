using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBottonCtr : MonoBehaviour
{
    //シュート行動データ
    BehaviorData KeyShoot = new BehaviorData();

    public float ShootCD = 0.5f;

    public float ShootCDTimer = 0.0f;

    int KeyShootDown = 0;

    public List<GameObject> m_BulletsList = new List<GameObject>();

    CharacterUIController ChaUICtr;


    public void ShootBottonIsDown(InputAction.CallbackContext obj)
    {
        if(KeyShootDown == 0)
        {
            KeyShootDown = 1;
        }
    }

    private Vector3 ShotPos;

    public void SetShotPos(Vector3 pos)
    {
        ShotPos = pos;
    }

    private bool CanShot;

    public void SetCanShot(bool can)
    {
        CanShot = can;
    }

    public bool GetCanShot()
    {
        return CanShot;
    }

    //IDは動作の主
    public void ShootKeyDown(PlayerController _PlayerCtr, int ID, Vector2 _ShootDir = new Vector2())
    {
        if (KeyShootDown > 0 && ID == _PlayerCtr.ControlPlayerID)//操作対象の処理
        {
            //射撃可能な場合
            if (CanShot)
            {
                Animator[] animators = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].GetComponentsInChildren<Animator>();
                animators[0].SetTrigger("doAttack");

                //操作キャラクターのシュートの間隔を取得する
                ShootCD = _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].ShootCD;

                // 弾（ゲームオブジェクト）の生成
                //GameObject clone = Instantiate(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position + _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion, Quaternion.identity);
                //clone.GetComponent<BulletData>().SetTarget(ShotPos);    //弾の方向
                //clone.GetComponent<BulletData>().SetShootPosition(clone.transform.position);


                // 向きの生成（Z成分の除去と正規化）
                Vector3 shotForward = new Vector3(_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].PlayersForward.x, 0.0f, 0.0f);

                //データ記録生成
                KeyShoot.PlayerID = _PlayerCtr.ControlPlayerID;

                KeyShoot.BottonID = 5;

                KeyShoot.StartTime = _PlayerCtr.Timer;

                KeyShoot.ShootDir = ShotPos;

                //記録に追加
                _PlayerCtr.RecordBehaviour.AddBehaviour(KeyShoot);

                KeyShootDown = -1;

                _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].gameObject.GetComponentInChildren<ShootKeeper>().SetParama(ShotPos, ID);

                //clone.GetComponent<Collision>().PlayerID = ID;
                //m_BulletsList.Add(clone);
            }
            else
            {
                KeyShootDown = 0;
            }
        }
        else if (ID != _PlayerCtr.ControlPlayerID)//非操作対象の処理
        {
            // 弾（ゲームオブジェクト）の生成
            //GameObject clone = Instantiate(_PlayerCtr.PlayersData[ID].Bullet, _PlayerCtr.Players[ID].GetComponent<Transform>().position + _PlayerCtr.Players[ID].GetComponent<Player>().ShootFixPostion, Quaternion.identity);

            //直接方向を取得
            // Vector3 shotForward = _ShootDir;
            // clone.GetComponent<BulletData>().SetTarget(_ShootDir);
            // clone.GetComponent<BulletData>().SetShootPosition(clone.transform.position);
            // 弾に速度を与える
            //clone.GetComponent<Rigidbody2D>().velocity = shotForward * _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].BulletSpeed;

            //clone.GetComponent<Collision>().PlayerID = ID;
            //m_BulletsList.Add(clone);

            Animator[] animators = _PlayerCtr.PlayersData[ID].GetComponentsInChildren<Animator>();
            animators[0].SetTrigger("doAttack");

            //_PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].gameObject.GetComponentInChildren<ShootKeeper>().SetParama(ShotPos, ID);
            _PlayerCtr.PlayersData[_PlayerCtr.ControlPlayerID].gameObject.GetComponentInChildren<ShootKeeper>().SetParama(_ShootDir, ID);
        }
    }

    public void ShootKeyDown_Skill(PlayerController _PlayerCtr, int ID, GameObject _SkillBulletObj, Vector2 _ShootDir = new Vector2())
    {
        GameObject clone = Instantiate(_SkillBulletObj, _PlayerCtr.Players[ID].GetComponent<Transform>().position, Quaternion.identity);
        clone.GetComponent<BulletData>().SetTarget(_ShootDir);    //弾の方向
        clone.GetComponent<BulletData>().SetShootPosition(clone.transform.position);
        if (_ShootDir.x > 0)
        {
            clone.transform.localScale = new Vector3(-clone.transform.localScale.x, clone.transform.localScale.y, 0);
            clone.transform.position = new Vector3(clone.transform.position.x + _PlayerCtr.SkillDataCtr.CutFixPosition.x, clone.transform.position.y - _PlayerCtr.SkillDataCtr.CutFixPosition.y,0);
        }
        else
        {
            clone.transform.position -= _PlayerCtr.SkillDataCtr.CutFixPosition;
        }
        clone.GetComponent<Collision>().PlayerID = ID;
        m_BulletsList.Add(clone);
    }
        
    private void Start()
    {
        ShotPos = new Vector3(0.0f, 0.0f, 0.0f);
        CanShot = false;

        ChaUICtr = FindObjectOfType<CharacterUIController>();

    }

    private void Update()
    {
        if (KeyShootDown < 0)
        {
            ShootCDTimer += Time.deltaTime;
        }

        if (ShootCDTimer >= ShootCD)
        {
            KeyShootDown = 0;
            ShootCDTimer = 0.0f;
        }
        ChaUICtr.CDMaskUpdate(0);
    }
}

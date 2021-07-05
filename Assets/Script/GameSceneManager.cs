using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    private PlayerController pc;
    private bool change;

    [SerializeField]
    private float FadeTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        pc = FindObjectOfType<PlayerController>();
        change = false;
    }

    // Update is called once per frame
    void Update()
    {
        //シーンの遷移
        ChangeScene();
    }

    //シーンの切り替え
    void ChangeScene()
    {
        if (change)
        {
            return;
        }

        //現在操作しているプレイヤーのID
        int player_id = pc.ControlPlayerID;

        //操作しているプレイヤーが死んだ場合は次のゲームオーバーシーンへ遷移
        if(!pc.PlayersData[player_id].IsAlive2)
        {
            GameOver();
            return;
        }

        //他のプレイヤーを全て倒した場合シーン遷移
        foreach (var player in pc.PlayersData)
        {
            //操作しているプレイヤーを参照していた場合は次のループ
            if (player_id == player.PlayerID)
            {
                continue;
            }

            //生きているプレイヤーがいればシーンを遷移しない
            if (player.IsAlive2)
            {
                return;
            }
        }
        //シーンの遷移
        Result();
    }

    /// <summary>
    /// リザルト用シーン遷移
    /// </summary>
    public void Result()
    {
        change = true;

        if (SceneManager.GetActiveScene().name == "beta")
            FadeManager.Instance.LoadScene("ResultScene", FadeTime);
    }

    /// <summary>
    /// ゲームオーバー用シーン遷移
    /// </summary>
    public void GameOver()
    {
        change = true;

        if (SceneManager.GetActiveScene().name == "beta")
            FadeManager.Instance.LoadScene("ResultScene", FadeTime);
    }
}

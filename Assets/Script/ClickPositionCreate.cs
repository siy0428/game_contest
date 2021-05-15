using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


//参考:https://enjoy-freely.com/%E3%80%90unity%E3%80%91%E3%83%87%E3%83%BC%E3%82%BF%E4%BF%9D%E5%AD%98%E3%82%BB%E3%83%BC%E3%83%96%E3%81%A7%E3%81%8D%E3%82%8B%E3%82%88%E3%81%86%E3%81%AB%E3%81%97%E3%82%88%E3%81%86%EF%BC%81/

public class ClickPositionCreate : MonoBehaviour
{
    // 生成したいPrefab
    public GameObject Tilemap;
    // クリックした位置座標
    private Vector3 clickPosition;
    // Use this for initialization
    void Start()
    {
        string[] saveStringList = new string[3];

        for (int i = 0; i < saveStringList.Length; i++)
        {
            //セーブデータがる場合は、"Save_Data"Keyが引き出される
            //セーブデータがない場合は、"No Data"Keyが引き出される
            string saveString = PlayerPrefs.GetString("Save_Data" + i, "No Data");
            //端末に入ってるデータを読み込む
            Debug.Log(saveString);
        }
        for (int i = 0; i < saveStringList.Length; i++)
        {
            //データを設定する
            PlayerPrefs.SetString("Save_Data" + i, "Have Data" + i);
            //設定したデータを保存
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // マウス入力で左クリック（0）を離した瞬間
        if (Input.GetMouseButtonUp(1))
        {
            // ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
            // Vector3でマウスがクリックした位置座標を取得する
            clickPosition = Input.mousePosition;
            // Z軸修正
            clickPosition.z = 10f;
            // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
            // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
            Instantiate(Tilemap, Camera.main.ScreenToWorldPoint(clickPosition), Tilemap.transform.rotation);
        }
    }
}

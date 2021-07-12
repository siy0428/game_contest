using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreOutput : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score", 0).ToString();
        }
        else
        {
            Debug.Log("スコアが取得できませんでした");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

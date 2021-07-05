using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddScoreCtr : MonoBehaviour
{
    public int AddScoreValue = 0;

    public float countScore = 0;

    public int Score = 0;

    public float LimitTime = 0.5f;

    public float LimitTime2 = 0.5f;

    public float Timer = 0.0f;

    public Vector2 movedistance;

    public Vector2 StartPos;

    public Vector2 currentPos;

    public GameObject canvasObj;
    // Start is called before the first frame update
    void Awake()
    {
        canvasObj = FindObjectOfType<Canvas>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        Timer += dt;
        if (Timer >= LimitTime + LimitTime2)
        {
            Score = 0;
            currentPos = StartPos + movedistance;
            Destroy(gameObject);
        }
        else if( Timer >= LimitTime)
        {
            currentPos = StartPos + movedistance;
            countScore -= AddScoreValue * (dt / LimitTime);
            Score = (int)countScore;
        }
        else
        {
            currentPos += movedistance * (dt / LimitTime);
        }

        GetComponent<TextMeshProUGUI>().text = Score.ToString();
        GetComponent<RectTransform>().localPosition = currentPos;
    }

    public void SetStartPos(Vector3 _Pos)
    {
        Debug.Log(_Pos);
        Vector2 newpos = Vector2.zero;
        GetComponent<RectTransform>().SetParent(canvasObj.transform);
        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, _Pos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasObj.GetComponent<RectTransform>(), screenPos, Camera.main,out newpos);
        StartPos = newpos;
        currentPos = StartPos;
        GetComponent<RectTransform>().localScale = new Vector3(5,5,1);
        Debug.Log(newpos);
    }

    public void SetAddScore(int _Value)
    {
        AddScoreValue = _Value;
        countScore = AddScoreValue;
        Score = (int)countScore;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInZoom : MonoBehaviour
{
    private Camera CutInCamera;

    [SerializeField]
    private int[] StageTime;
    [SerializeField]
    private float[] StageSize;

    private int StageCount;
    private float _Time;

    void Start()
    {
        CutInCamera = GetComponent<Camera>();
        StageCount = 0;
        _Time = 0.0f;
    }

    void Update()
    {
        var finish = Zoom();

        if (finish)
        {
            StartCoroutine(Stop());

            if (StageCount >= 3)
            {
                //ƒJƒƒ‰‚ğƒƒCƒ“ƒJƒƒ‰‚ÉØ‚è‘Ö‚¦
                CutInManager.instance.isActive = false;
                //Time.timeScale = 1.0f;
            }
        }
    }

    private bool Zoom()
    {
        CutInCamera.orthographicSize = Mathf.Max(StageSize[StageCount], CutInCamera.orthographicSize - 0.01f);

        return CutInCamera.orthographicSize <= StageSize[StageCount];
    }

    private IEnumerator Stop()
    {
        while (_Time < StageTime[StageCount])
        {
            _Time += Time.unscaledDeltaTime;
            yield return null;
        }
        StageCount++;
        _Time = 0.0f;
    }
}

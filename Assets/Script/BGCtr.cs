using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCtr : MonoBehaviour
{
    public Transform[] bgs;
    public float parallaxScaleX;
    public float parallaxScaleY;
    public float parallaxReductionFactorX;
    public float parallaxReductionFactorY;
    public float smoothing;

    public Vector3 CamStartPos;

    private Transform Cam;
    private Vector3 prevCamePos;

    private Vector3[] bgsStartPos;
    private void Awake()
    {
        Cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        prevCamePos = CamStartPos;
        bgsStartPos = new Vector3[bgs.Length];
        for (int i = 0; i < bgs.Length; i++)
        {
            bgsStartPos[i] = bgs[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float parallaxX = (prevCamePos.x - Cam.position.x) * parallaxScaleX;
        float parallaxY = (prevCamePos.y - Cam.position.y) * parallaxScaleY;
        for (int i = 0; i < bgs.Length; i++)
        {
            float bgTargetPosX = bgs[i].position.x + parallaxX * (i * parallaxReductionFactorX + 1);

            float bgTargetPosY = bgs[i].position.y + parallaxY * (i * parallaxReductionFactorY + 1);

            Vector3 bgTargetPos = new Vector3(bgTargetPosX, bgTargetPosY, bgs[i].position.z);

            bgs[i].position = Vector3.Lerp(bgs[i].position, bgTargetPos,smoothing * Time.deltaTime);
        }

        prevCamePos = Cam.position;
    }

    public void Reset()
    {
        for (int i = 0; i < bgs.Length; i++)
        {
            bgs[i].position = bgsStartPos[i];
        }
    }
}

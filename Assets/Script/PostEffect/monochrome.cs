using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monochrome : MonoBehaviour
{
    [SerializeField]
    private Material monoTone;
    [SerializeField]
    private Material default_material;

    private TimeBodyManager tbm;

    void Start()
    {
        tbm = FindObjectOfType<TimeBodyManager>();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Material material = null;

        //逆再生中だったらモノクロのエフェクトをかける
        material = (tbm.GetIsUse()) ? monoTone : default_material;

        Graphics.Blit(src, dest, material);
    }
}

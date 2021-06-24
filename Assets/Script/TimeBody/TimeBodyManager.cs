using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeBodyManager : MonoBehaviour
{
    [SerializeField]
    private int magnification;

    private bool is_use;

    // Start is called before the first frame update
    void Start()
    {
        is_use = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 逆再生する速度の倍率の取得
    /// </summary>
    /// <returns></returns>
    public int GetMagniflication()
    {
        return magnification;
    }

    /// <summary>
    /// 逆再生使用フラグの設定
    /// </summary>
    /// <param name="use">設定フラグ</param>
    public void SetIsUse(bool use)
    {
        is_use = use;
    }

    /// <summary>
    /// 逆再生使用フラグの取得
    /// </summary>
    /// <returns>現在逆再生中かどうかのフラグ</returns>
    public bool GetIsUse()
    {
        return is_use;
    }
}

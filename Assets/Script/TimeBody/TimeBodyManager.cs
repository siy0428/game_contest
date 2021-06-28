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
    /// �t�Đ����鑬�x�̔{���̎擾
    /// </summary>
    /// <returns></returns>
    public int GetMagniflication()
    {
        return magnification;
    }

    /// <summary>
    /// �t�Đ��g�p�t���O�̐ݒ�
    /// </summary>
    /// <param name="use">�ݒ�t���O</param>
    public void SetIsUse(bool use)
    {
        is_use = use;
    }

    /// <summary>
    /// �t�Đ��g�p�t���O�̎擾
    /// </summary>
    /// <returns>���݋t�Đ������ǂ����̃t���O</returns>
    public bool GetIsUse()
    {
        return is_use;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInManager : MonoBehaviour
{
    public static CutInManager instance;

    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject CutInCamera;

    [HideInInspector]
    public bool isActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        //StartCutIn();
    }

    // Update is called once per frame
    void Update()
    {
        //カットイン中のカメラ切り替え
        ChangeCamera(!isActive);
    }

    private void ChangeCamera(bool main)
    {
        MainCamera.SetActive(main);
        CutInCamera.SetActive(!main);
    }

    void StartCutIn()
    {
        isActive = true;

        Time.timeScale = 0.0f;
    }
}

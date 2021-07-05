using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInManager : MonoBehaviour
{
    public static CutInManager instance;

    private float _time = 0.0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(_time < 3.0f )
        //{
        //    Time.timeScale = 0.0f;
        //}
        //else if(_time > 5.0f)
        //{
        //    Time.timeScale = 1.0f;
        //    _time = 0.0f;
        //}

        //_time += Time.deltaTime;

        //Debug.Log(_time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostLifeCtr : MonoBehaviour
{
    public List<GameObject> HPPerb;
    public List<GameObject> LostHPObjs;
    public int TotalLostHP = 0;
    public float IconDistance = 0.44f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLostLife(int value)
    {
        TotalLostHP += value;
        int k = TotalLostHP / 2;
        if (LostHPObjs.Count > 0)
        {
            for(int q = LostHPObjs.Count - 1; q >= 0 ; q--)
            {
                GameObject a = LostHPObjs[q];
                LostHPObjs.Remove(a);
                Destroy(a);
            }            
        }
        for (int i = 0; i < k; i++)
        {
            GameObject clone = Instantiate(HPPerb[0], Vector3.zero, Quaternion.identity);
            LostHPObjs.Add(clone);
        }

        if(TotalLostHP % 2 == 1)
        {
            GameObject clone = Instantiate(HPPerb[1], Vector3.zero, Quaternion.identity);
            LostHPObjs.Add(clone);
        }

        for (int j = 0; j < LostHPObjs.Count; j++)
        {
            LostHPObjs[j].transform.SetParent(transform);
            float scale = LostHPObjs[j].transform.localScale.x;
            LostHPObjs[j].transform.localPosition = new Vector3((-(LostHPObjs.Count - 1) * IconDistance / 2 + j * IconDistance)* scale, 0, 0);
        }
    }

    public void LostLifeReset()
    {
        if (LostHPObjs.Count > 0)
        {
            for (int q = LostHPObjs.Count - 1; q >= 0; q--)
            {
                GameObject a = LostHPObjs[q];
                LostHPObjs.Remove(a);
                Destroy(a);
            }
        }
        TotalLostHP = 0;
    }
}

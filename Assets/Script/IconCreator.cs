using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCreator : MonoBehaviour
{
    public GameObject IconImageObj;
    public Vector3 StartPostion;
    public int WidthNum;
    public int HeightNum;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i<HeightNum;i++)
        {
            for(int j = 0; j<WidthNum;j++)
            {
                GameObject icon = Instantiate(IconImageObj);
                icon.transform.SetParent(transform, false);
                icon.transform.localPosition = StartPostion + new Vector3(j * 3, -i * 3, 0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    public List<GameObject> CreatableObjs;
    public List<Vector3> CrateableObjPos;
    public List<CreatableObject> SelectableObjList;
    public List<CreatableObject> CreatedObjs;

    public int SelectableMaxNum = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int RandomObj()
    {
        int a = (int)Random.Range(0, 10000);
        a %= CreatableObjs.Count;
        return a;
    }

    public void CreateSelectableList()
    {
        foreach(CreatableObject a in SelectableObjList)
        {
            SelectableObjList.Remove(a);
            Destroy(a.Obj);
        }

        for (int i = 0; i < SelectableMaxNum; i++)
        {
            CreatableObject newobj = new CreatableObject();
            newobj.ID = RandomObj();
            newobj.Obj = Instantiate(CreatableObjs[newobj.ID], CrateableObjPos[i],Quaternion.identity);
            newobj.pos = CrateableObjPos[i];
        }
    }
}

public class CreatableObject
{
    public int ID;
    public GameObject Obj;
    public bool IsUsed;
    public Vector3 pos;
    public CreatableObject()
    {
        ID = -1;
        IsUsed = false;
        pos = new Vector3();
    }
}

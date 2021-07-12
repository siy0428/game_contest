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

    public int SelectedID = 0;

    [SerializeField]
    private bool IsSelected = false;

    [SerializeField]
    private bool CreateModeOn = false;

    [SerializeField]
    private bool IsCreated = false;

    private CreatableObject SelectedObj;
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
        if(SelectableObjList.Count > 0)
        {
            for (int i = SelectableObjList.Count - 1; i > -1; i--)
            {
                CreatableObject a = SelectableObjList[i];
                SelectableObjList.Remove(a);
                if (i != SelectedID)
                {
                    Destroy(a.Obj);
                }
            }
        }

        for (int i = 0; i < SelectableMaxNum; i++)
        {
            CreatableObject newobj = new CreatableObject();
            newobj.ID = RandomObj();
            newobj.Obj = Instantiate(CreatableObjs[newobj.ID], CrateableObjPos[i],Quaternion.identity);
            newobj.pos = CrateableObjPos[i];
            SelectableObjList.Add(newobj);
        }
    }

    public void ChangeIntoCreateMode(bool _Value = true)
    {
        CreateModeOn = _Value;
    }

    public void SelectItem()
    {
        IsSelected = true;
        SelectedObj = SelectableObjList[SelectedID];
    }

    public void CreateObj()
    {
        IsCreated = true;
        CreatedObjs.Add(SelectedObj);
    }

    public void CreateReset(bool _CreateModeOn,bool _IsToNext)
    {
        if(_CreateModeOn)
        {
            ChangeIntoCreateMode();
            IsCreated = false;
            IsSelected = false;
            if (!_IsToNext)
            {
                CreatableObject a = CreatedObjs[CreatedObjs.Count - 1];
                CreatedObjs.Remove(a);
                Destroy(a.Obj);

                for (int i = 0; i < SelectableMaxNum; i++)
                {
                    SelectableObjList[i].pos = CrateableObjPos[i];
                }
            }
            else
            {
                CreateSelectableList();
            }
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

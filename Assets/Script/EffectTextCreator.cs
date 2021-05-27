using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTextCreator : MonoBehaviour
{
    public RuntimeAnimatorController theAnimator;

    public string EffectTextStr = "ET";

    public bool DoCreateText = false;

    private bool DoneCreate = false;


    public void create_effect_text()
    {
        GameObject TextObj, ParentObj;

        float pos_x = gameObject.GetComponentInParent<Transform>().position.x + Random.insideUnitCircle.x * 0.5f;
        float pos_y = gameObject.GetComponentInParent<Transform>().position.y + 0.5f + Random.insideUnitCircle.y * 0.5f;

        ParentObj = new GameObject(EffectTextStr);
        ParentObj.AddComponent<DestroyTrigger>();

        TextObj = new GameObject("EffectTextObj");
        TextObj.transform.SetParent(ParentObj.transform);
        TextObj.AddComponent<SpriteRenderer>();
        TextObj.AddComponent<Animator>();
        TextObj.GetComponent<Animator>().runtimeAnimatorController = theAnimator;
        TextObj.AddComponent<DestroyTrigger>();

        ParentObj.transform.position = new Vector3(pos_x, pos_y, 2.0f);
        DoneCreate = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (DoneCreate && !DoCreateText)
        {
            DoneCreate = false;
        }
        if (DoCreateText && !DoneCreate)
        {
            create_effect_text();
        }
    }
}

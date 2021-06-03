using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    //キャラクターアイコン
    public GameObject AvatarObj;

    //普通の攻撃アイコン
    public GameObject AttackIconObj;
    
    //普通の攻撃CD用マスク
    public GameObject AttackIconCDMaskObj;

    //スキルアイコン
    public GameObject SkillIconObj;

    //スキルCD用マスク
    public GameObject SkillIconCDMaskObj;

    //スキルのボタンイメージ
    public GameObject SkillBottonImObj;

    //ヘルスポイント
    public List<GameObject> HealthPointObj;

    public List<Sprite> HealthPointIconsList;

    private PlayerController PlayerCtr;

    private ShootBottonCtr ShootCtr;

    private SkillBottonCtr SkillCtr;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCtr = FindObjectOfType<PlayerController>();

        ShootCtr = FindObjectOfType<ShootBottonCtr>();

        SkillCtr = FindObjectOfType<SkillBottonCtr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeUI(int ID)
    {
        Player py = PlayerCtr.PlayersData[ID];

        AvatarObj.GetComponent<Image>().sprite = py.AvatarObj;

        AttackIconObj.GetComponent<Image>().sprite = py.AttackIconObj;

        SkillIconObj.GetComponent<Image>().sprite = py.SkillIconObj;

        SkillBottonImObj.GetComponent<Image>().sprite = py.SkillBottonIm;

        AttackIconCDMaskObj.GetComponent<Image>().fillAmount = 0;

        SkillIconCDMaskObj.GetComponent<Image>().fillAmount = 0;

        if (py.MaxHP % 2 == 0)
        {
            for (int i = 0; i < HealthPointObj.Count; i++)
            {
                if( i < py.MaxHP / 2)
                {
                    HealthPointObj[i].SetActive(true);
                    if(i==0)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[0];
                    }
                    else
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[3];
                    }
                }
                else
                {
                    HealthPointObj[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < HealthPointObj.Count; i++)
            {
                if (i < (py.MaxHP + 1)/ 2)
                {
                    HealthPointObj[i].SetActive(true);
                    if (i == 0)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[0];
                    }
                    else if(i!= (int)py.MaxHP / 2)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[3];
                    }
                    else
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[4];
                    }
                }
                else
                {
                    HealthPointObj[i].SetActive(false);
                }
            }

        }

    }

    public void CDMaskUpdate(int _AB)
    {
        if(_AB == 0)
        {
            AttackIconCDMaskObj.GetComponent<Image>().fillAmount = PlayerCtr.CheakShootCD();
        }

        if(_AB == 1)
        {
            SkillIconCDMaskObj.GetComponent<Image>().fillAmount = PlayerCtr.CheakSkillCD();
        }
    }

    public void ChangeHP(int ID)
    {
        Player py = PlayerCtr.PlayersData[ID];
        int limit = ((int)py.MaxHP + 1) / 2;

        if (py.HP % 2 == 0)
        {          
            for (int i = 0; i < limit; i++)
            {
                if (i < py.HP / 2 && py.HP > 0)
                {
                    if (i == 0)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[0];
                    }
                    else
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[3];
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[2];
                    }
                    else
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[5];
                    }

                }
            }

        }
        else
        {
            for (int i = 0; i < limit; i++)
            {
                if (i < ((int)py.HP + 1) / 2)
                {
                    if (i == 0)
                    {
                        if(py.HP > 1)
                        {
                            HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[0];
                        }
                        else
                        {
                            HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[1];
                        }
                    }
                    else if (i != (int)py.HP / 2)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[3];
                    }
                    else
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[4];
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[2];
                    }
                    else
                    {
                        HealthPointObj[i].GetComponent<Image>().sprite = HealthPointIconsList[5];
                    }
                }
            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    //�L�����N�^�[�A�C�R��
    public GameObject AvatarObj;

    //���ʂ̍U���A�C�R��
    public GameObject AttackIconObj;
    
    //���ʂ̍U��CD�p�}�X�N
    public GameObject AttackIconCDMaskObj;

    //���j�b�g�X�L���A�C�R��
    public GameObject SkillIconObj;

    //���j�b�g�X�L��CD�p�}�X�N
    public GameObject SkillIconCDMaskObj;


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

        AttackIconCDMaskObj.GetComponent<Image>().fillAmount = 0;

        SkillIconObj.GetComponent<Image>().fillAmount = 0;
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
}

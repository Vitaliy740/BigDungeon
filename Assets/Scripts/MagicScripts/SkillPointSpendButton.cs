using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
public class SkillPointSpendButton : MonoBehaviour, IPointerClickHandler
{
    public SkillPanel panel;
    public string skillname;
    public int skillcout=0;
    public int maxskillLevel;
    public int previosneededpoints;
    public GameObject previoskill;
    public Text CoutSkillLevel;
    private GameObject skillDescriptionbase;
    public List<GameObject> SkillDescription;
    public bool isActiveSkill=true;
    public List<GameObject> buttons;
    private GameObject SkillButtonPlace;
    public UnityEvent adding;
    // Start is called before the first frame update
    private void Start()
    {
        //CoutSkillLevel = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        SkillButtonPlace = GameObject.Find("MagicButtonPlace");
        buttons = new List<GameObject>();
        for (int i = 0; i < SkillButtonPlace.transform.childCount; i++)
        {
            buttons.Add(SkillButtonPlace.transform.GetChild(i).gameObject);
        }
        skillDescriptionbase = GameObject.Find("Description Base");
        SkillDescription = new List<GameObject>();
        for (int i = 0; i < skillDescriptionbase.transform.childCount; i++)
        {
            SkillDescription.Add(skillDescriptionbase.transform.GetChild(i).gameObject);
        }
        CoutSkillLevel.text = skillcout.ToString() + "/" + maxskillLevel.ToString();
    }
    public void Spend()
    {
        panel.ChangeSkillCout(-1);
        skillcout++;
        CoutSkillLevel.text = skillcout.ToString()+"/"+maxskillLevel.ToString();
        panel.AddPossibility();
    }
    public void LoadSkillLevel(Save.SkillSaveData skill)
    {
        skillcout = skill.Skilllevel;
        CoutSkillLevel.text = skillcout.ToString() + "/" + maxskillLevel.ToString();
        if(skillcout<1 && isActiveSkill)
        {
            for (int i = 0; i < SkillButtonPlace.transform.childCount; i++)
            {
                if (buttons[i].name == skillname)
                    buttons[i].SetActive(false);
            }
        } 

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < skillDescriptionbase.transform.childCount; i++)
        {
            if (SkillDescription[i].name == skillname)
                SkillDescription[i].SetActive(true);
            else SkillDescription[i].SetActive(false);
        }
        if (panel.addpossibility && previoskill.GetComponent<SkillPointSpendButton>().skillcout >= previosneededpoints && skillcout<maxskillLevel)
        {
            Spend();
            if (adding!=null)
            {
                adding.Invoke();
                
            }
        }
        if (isActiveSkill && skillcout > 0) 
            for(int i=0;i< SkillButtonPlace.transform.childCount; i++)
            {
                if (buttons[i].name == skillname)
                    buttons[i].SetActive(true);
                else buttons[i].SetActive(false);
            }

    }
}

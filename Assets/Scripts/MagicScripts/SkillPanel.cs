using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    private Text coutskillviev;
    public int coutskill=0;
    [HideInInspector]
    public Button spendpoint;
    public GameObject CancellButton;
    public bool addpossibility=true;
    public void Start()
    {
        spendpoint = gameObject.transform.GetChild(4).GetComponent<Button>();
        spendpoint.interactable = false;
        coutskillviev = gameObject.transform.GetChild(2).GetComponent<Text>();
        ChangeSkillCout(0);
        AddPossibility();
    }
    public void ChangeSkillCout(int n)
    {
        coutskill += n;
        coutskillviev.text = coutskill.ToString();
    }
    public void AddPossibility()
    {
        if (coutskill < 1)
        {
            CancellButton.gameObject.SetActive(false);
            spendpoint.gameObject.SetActive(true);
            addpossibility = false;
            spendpoint.interactable = false;
        }
        else spendpoint.interactable = true;
    }
    public void StopAdding()
    {
        spendpoint.interactable = true;
        addpossibility = false;
    }
    public void AddSkillPoint()
    {
        ChangeSkillCout(1);
        spendpoint.interactable = true;

    }
    public void SpendingSkillPoint()
    {
        addpossibility = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expirience : MonoBehaviour
{
    public float CurrentExp=0;
    private Image ExpBar;
    private Vector3 ExpScale;
    private Vector3 originalScale;
    public GameObject ExperienceBar;
    public int Level=0;
    public int neededExperience = 75;
    private int fullexp=0;
    public Text CurLvl;
    public GameObject MArea;
    public GameObject skillpanel;
    // Start is called before the first frame update
    void Start()
    {
        skillpanel.GetComponent<SkillPanel>().Start();
        ExpBar = ExperienceBar.GetComponent<Image>();
        ExpScale = ExperienceBar.transform.localScale;
        originalScale = ExpScale;
        UpdateExperience(0);
        CurLvl.text = Level.ToString();
    }
    public void UpdateExperience(float exp)
    {
        if (Level == 30) 
        {
            Debug.Log(fullexp);
            ExpBar.transform.localScale = originalScale;
            return; 
        }
        if (CurrentExp + exp < neededExperience)
        {
            CurrentExp = CurrentExp + exp;
            ExpScale.x = (CurrentExp / neededExperience) * originalScale.x;
            ExpBar.transform.localScale = ExpScale;
        }
        else
        {
            fullexp += neededExperience;
            Level++;
            skillpanel.GetComponent<SkillPanel>().AddSkillPoint();
            CurrentExp = (CurrentExp + exp)- neededExperience;
            neededExperience += 25;
            ExpScale.x = (CurrentExp / neededExperience) * originalScale.x;
            ExpBar.transform.localScale = ExpScale;
        }
        CurLvl.text = Level.ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mastery : MonoBehaviour
{
    public GameObject skillb;
    private int skilllevel;
    // Start is called before the first frame update
    public float CheckMastery()
    {
        skilllevel = skillb.GetComponent<SkillPointSpendButton>().skillcout;
        return (skilllevel * 5f)/100f;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rage : MonoBehaviour
{
    public Button rageb;
    private bool ragepressed;
    public SkillPointSpendButton sb;
    private HealthBar hb;
    private ManaBar mb;
    private float curtime=0;
    public float originalHealth;
    // Start is called before the first frame update
    void Start()
    {
        hb = GetComponent<HealthBar>();
        mb = GetComponent<ManaBar>();
        originalHealth = hb.maxhealth;
    }
    public float Ragecouter()
    {
        if (sb.skillcout > 0)
            return (sb.skillcout * 10f) / 100f;
        else
            return 0f;
    }
    // Update is called once per frame
    void Update()
    {
        rageb.onClick.AddListener(delegate { ragepressed = true; });
        if(ragepressed&& curtime < 5f&&mb.mana>=49)
        {
            rageb.interactable = false;
            hb.health = hb.originalHealth - (10f * sb.skillcout/100f)*hb.originalHealth;
            hb.maxhealth = hb.originalHealth - (10f * sb.skillcout / 100f) * hb.originalHealth;
            curtime += Time.deltaTime;
            mb.mana -= 49;
            mb.UpdateManaBar();
            hb.UpdateHealthBar();
            ragepressed = false;
        }
        if(rageb.interactable == false)
            curtime += Time.deltaTime;
        if (curtime >= 5f)
        {
            rageb.interactable = true;
            hb.maxhealth = hb.originalHealth;
            hb.UpdateHealthBar();
            curtime = 0f;

        }
    }
}

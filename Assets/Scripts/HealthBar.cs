using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public float health = 100f;
    public float maxhealth = 100f;
    public float damageAmount;
    public float originalHealth;
    public ArmorSlot armor;
    private Image healthBar;
    private Vector3 healthScale;
    private Vector3 originalslale;
    public GameManager G;
    public GameObject healthbar;
    private Text cHealth, mHealth;
    public SkillPointSpendButton sb;
    public SkillPointSpendButton shield;
    private float time = 0;
    private float cooldown = 80f;
    private bool startcooldown=false;
    public bool shiledisactive=false;
    public GameObject visualshield;
    // Use this for initialization
    void Start()
    {
        
        healthBar.material.color = Color.green;
        cHealth = healthbar.transform.parent.GetChild(2).GetComponent<Text>();
        mHealth = healthbar.transform.parent.GetChild(1).GetComponent<Text>();
        originalslale = healthScale;
        originalHealth = maxhealth;
        UpdateHealthBar();
    }
    private void Awake()
    {
        healthBar = healthbar.GetComponent<Image>();
        healthScale = healthbar.transform.localScale;
    }
    public void MoreHealth()
    {
        maxhealth += 30;
        originalHealth = maxhealth;
        health = maxhealth;
        healthScale = originalslale;
        UpdateHealthBar();
    }
    public void AddHealth(int Value)
    {
        health += Value;
        if (health > maxhealth) health = maxhealth;
        UpdateHealthBar();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.childCount>0) 
        {
            if (col.transform.GetChild(2).tag == "Enemy Veapon" && col.gameObject.GetComponent<EnemyAI>().isAttacking)
                Damaging(col.transform.GetChild(2).gameObject.GetComponent<Item>().Damege);
        }
        if (col.gameObject.tag == "Enemy Veapon" && col.transform.parent.GetComponent<EnemyAI>().isAttacking)
            Damaging(col.gameObject.GetComponent<Item>().Damege);
    }
    private void Damaging(float damage)
    {
        damageAmount = damage - armor.Armor.Armor;
        if (damageAmount < 1) damageAmount = 1;
        if (health-damageAmount > 0f)
        {
            UpdateHealthBar();
            health -= damageAmount;
        }
        else
        {
            if (shield.skillcout > 0 && time < 8f && cooldown == 80f)
            {
                shiledisactive = true;
                health = 1;
                UpdateHealthBar();
                visualshield.SetActive(true);
            }
            else
            {
                health = 0;
                UpdateHealthBar();
                G.ShowLosePanel();
            }
        }
    }
    public void UpdateHealthBar()
    {
        healthScale.x = (health / originalHealth) * originalslale.x;
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = healthScale;
        cHealth.text = health.ToString();
        mHealth.text = maxhealth.ToString();
    }
    private void Update()
    {
        if (shiledisactive)
        {
            time += Time.deltaTime;
        }
        if (time > 8f)
        {
            shiledisactive = false;
            startcooldown = true;
            visualshield.SetActive(false);
            time = 0;
        }
        if (startcooldown)
        {
            cooldown -= Time.deltaTime;
            Debug.Log(cooldown);
        }
        if (cooldown <= 0f)
        {
            startcooldown = false;
            cooldown = 80f;
            
        }
        
    }

}

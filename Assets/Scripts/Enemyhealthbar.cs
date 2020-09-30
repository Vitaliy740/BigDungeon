using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemyhealthbar : MonoBehaviour {
    
    // Use this for initialization
    public float enemyhealth = 50f;
    private float maxhealth;
    public float enemydamageAmount = 10f;
    private Vector3 healthScale;
    private Vector3 originalskale;
    public GameObject healthbar;
    public GameObject DataBase;
    public GameObject personaldrop=null;
    public GameObject Level;
    public int maxDrop;
    public int minDrop;
    private GameObject playerWeapon;
    [SerializeField]
    private float Experience;
    [SerializeField]
    private int armor;
    [SerializeField]
    private string resistance;
    [SerializeField]
    private string vulnerability;
    private bool fired=false;
    private bool frozen=false;
    private float effecttime = 0f;
    private float normalspeed;
    private string pweapondt;
    private int tik;
    // Use this for initialization
    void Awake()
    {
        healthScale = healthbar.transform.localScale;
        originalskale= healthbar.transform.localScale;
        playerWeapon = GameObject.Find("HandWeapon");
        normalspeed = GetComponent<EnemyAI>().speed;
        maxhealth = enemyhealth;
    }
    //private void Start()
    //{
    //    Debug.Log(playerWeapon.gameObject.GetComponent<DamageCounter>().FindDamage());
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player Veapon" && playerWeapon.transform.parent.gameObject.GetComponent<Control>().attack)
        {
            if (enemyhealth > 0f)
            {
                Debug.Log(playerWeapon.gameObject.GetComponent<DamageCounter>().FindDamage(resistance, vulnerability, armor));
                pweapondt = playerWeapon.GetComponentInChildren<Item>().DamageType;
                enemyhealth -= playerWeapon.gameObject.GetComponent<DamageCounter>().FindDamage(resistance,vulnerability,armor);
                if (pweapondt!= resistance)
                {
                    if (pweapondt == vulnerability)
                        if (pweapondt == "Fire")
                        {
                            fired = true;
                            effecttime = 0f;
                        }
                        else
                        {
                            frozen = true;
                            effecttime = 0f;
                        }
                    else if (pweapondt == "Fire" && Random.Range(1, 3) == 1)
                        fired = true;
                    else if (pweapondt == "Frost" && Random.Range(1, 3) == 1)
                        frozen = true;
                }
                UpdateHealthBar();
            }
            else 
            {
                enemyhealth = 0;
                UpdateHealthBar();
                Death();
                return;
            }
        }
    } 
    void Death()
    {
        GameObject objd = GameObject.Find("Игрок");
        if(objd.GetComponent<HealthBar>().shiledisactive)
             objd.GetComponent<HealthBar>().AddHealth(50);
        if (objd.GetComponent<SuperSpin>().spinning)
        {
            objd.GetComponent<SuperSpin>().AddSpinTime();
            objd.GetComponent<HealthBar>().AddHealth(20);
        }
        if (personaldrop)
        {
            if (Random.Range(1, 5) == 1)
            {
                int a = Random.Range(0, personaldrop.transform.childCount);
                GameObject pobj = Instantiate(personaldrop.transform.GetChild(a).gameObject);
                pobj.transform.position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
                pobj.transform.localScale = DataBase.transform.GetChild(a).transform.localScale;
            }
        }
        int c = Random.Range(minDrop, maxDrop+1);
        for (int i = 0; i <c; i++)
        {
            int a = Random.Range(0, DataBase.transform.childCount);
            GameObject obj = Instantiate(DataBase.transform.GetChild(a).gameObject);
            obj.GetComponent<Item>().price = 0;
            if (obj.GetComponent<Item>().isWeapon)
            {
                int elemental = Random.Range(1, 30);
                if (elemental == 1)
                {
                    obj.GetComponent<Item>().elementalDamage = Random.Range(1 + Level.GetComponent<Expirience>().Level, 4 * (Level.GetComponent<Expirience>().Level + 1)+1);
                    obj.GetComponent<Item>().DamageType = "Fire";
                    obj.GetComponent<Item>().price += obj.GetComponent<Item>().elementalDamage * 5;
                }
                if (elemental == 2)
                {
                    obj.GetComponent<Item>().elementalDamage = Random.Range(1+ Level.GetComponent<Expirience>().Level, 4* (Level.GetComponent<Expirience>().Level + 1)+1);
                    obj.GetComponent<Item>().DamageType = "Frost";
                    obj.GetComponent<Item>().price += obj.GetComponent<Item>().elementalDamage * 5;
                }
                obj.GetComponent<Item>().Damege = Random.Range(5+Level.GetComponent<Expirience>().Level, 5 *(Level.GetComponent<Expirience>().Level+1)+1);
            }
            if (obj.GetComponent<Item>().isArmor)
            {
                obj.GetComponent<Item>().Armor = Random.Range(10 + Level.GetComponent<Expirience>().Level, 10 * (Level.GetComponent<Expirience>().Level + 1)+1);
            }
            obj.transform.position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
            obj.transform.localScale = DataBase.transform.GetChild(a).transform.localScale;
            obj.GetComponent<Item>().price+=Random.Range(25*(Level.GetComponent<Expirience>().Level+1),30* (Level.GetComponent<Expirience>().Level + 1));
            if (obj.GetComponent<Item>().id == 13) obj.GetComponent<Item>().price *= 2;
            if (obj.GetComponent<Item>().id == 3 || obj.GetComponent<Item>().id == 6) obj.GetComponent<Item>().price = 25;
        }
        transform.position = new Vector3(-437.35f, -40f, 0.85f);
        frozen = false;
        fired = false;
        effecttime = 0;
        enemyhealth = 10f;
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        Level.GetComponent<Expirience>().UpdateExperience(Experience);
    }
    public void UpdateHealthBar()
    {
        healthScale.x = (enemyhealth / maxhealth) * originalskale.x;
        healthbar.transform.localScale = healthScale;
    }
    private void Update()
    {
        if (enemyhealth < 0f) Death();
            if (frozen)
            if( effecttime < 5f)
            {
                GetComponent<EnemyAI>().speed = normalspeed / 2f;
                if (tik < (int)effecttime)
                {
                    if (pweapondt == vulnerability)
                    {
                        enemyhealth -= playerWeapon.GetComponentInChildren<Item>().elementalDamage;
                    }
                    else
                        enemyhealth -= playerWeapon.GetComponentInChildren<Item>().elementalDamage / 2f;
                    UpdateHealthBar();
                    tik = (int)effecttime;
                }
                transform.GetChild(3).gameObject.SetActive(true);
                effecttime += Time.deltaTime;
            }
            else
            {
                GetComponent<EnemyAI>().speed = normalspeed;
                effecttime =0f;
                tik = 0;
                frozen = false;
                transform.GetChild(3).gameObject.SetActive(false);
            }
        if(fired)
            if( effecttime < 5f)
            {
                if (tik < (int)effecttime)
                {
                    if (pweapondt == vulnerability)
                    {
                        enemyhealth -= playerWeapon.GetComponentInChildren<Item>().elementalDamage;
                    }
                    else
                        enemyhealth -= playerWeapon.GetComponentInChildren<Item>().elementalDamage / 2f;
                    UpdateHealthBar();
                    tik = (int)effecttime;
                }
                transform.GetChild(4).gameObject.SetActive(true);
                effecttime += Time.deltaTime;
            }
            else
            {
                transform.GetChild(4).gameObject.SetActive(false);
                tik = 0;
                effecttime = 0f;
                fired = false;
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDrop : MonoBehaviour
{
    public GameObject DataBase;
    public int maxDrop;
    public GameObject Level;
    public string Rare;
    public int minDrop;
    public GameObject wall;

    public void Drop()
    {
        int c = Random.Range(minDrop, maxDrop + 1);
        Debug.Log(c);
        for (int i = 0; i < c; i++)
        {
            int a = Random.Range(0, DataBase.transform.childCount);
            GameObject obj = Instantiate(DataBase.transform.GetChild(a).gameObject);
            if (obj.GetComponent<Item>().isWeapon)
            {
                int elemental = Random.Range(1, 30);
                if (elemental == 1)
                {
                    obj.GetComponent<Item>().elementalDamage = Random.Range(4 + Level.GetComponent<Expirience>().Level, 6 * (Level.GetComponent<Expirience>().Level + 1) + 1);
                    obj.GetComponent<Item>().DamageType = "Fire";
                }
                if (elemental == 2)
                {
                    obj.GetComponent<Item>().elementalDamage = Random.Range(4 + Level.GetComponent<Expirience>().Level, 6 * (Level.GetComponent<Expirience>().Level + 1) + 1);
                    obj.GetComponent<Item>().DamageType = "Frost";
                }
                obj.GetComponent<Item>().Damege = Random.Range(5 + Level.GetComponent<Expirience>().Level, 5 * (Level.GetComponent<Expirience>().Level + 1) + 1);
            }
            if (obj.GetComponent<Item>().isArmor)
            {
                obj.GetComponent<Item>().Armor = Random.Range(12 + Level.GetComponent<Expirience>().Level, 13 * (Level.GetComponent<Expirience>().Level + 1) + 1);
            }
            obj.transform.position = new Vector3(transform.GetChild(1).position.x + Random.Range(-0.5f, 0.5f), transform.GetChild(1).position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
            obj.transform.localScale = DataBase.transform.GetChild(a).transform.localScale;
        }
        if (wall) Destroy(wall);
        transform.GetChild(0).gameObject.SetActive(true);
    }

}

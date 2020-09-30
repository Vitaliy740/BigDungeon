using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    public GameObject buycell;
    public Expirience exb;
    public int minItem;
    public int maxItem;
    public GameObject DataBase;
    public SellPanel sel;
    
    // Start is called before the first frame update
    void Start()
    {
        var hpotion= Instantiate(buycell, transform.position, Quaternion.identity);
        hpotion.GetComponent<BuyItem>().buyebleItem = DataBase.transform.GetChild(0).GetComponent<Item>();
        hpotion.transform.parent = transform;
        hpotion.GetComponent<BuyItem>().buyebleItem.price = 50;
        var mpotion = Instantiate(buycell, transform.position, Quaternion.identity);
        mpotion.GetComponent<BuyItem>().buyebleItem = DataBase.transform.GetChild(1).GetComponent<Item>();
        mpotion.transform.parent = transform;
        mpotion.GetComponent<BuyItem>().buyebleItem.price = 50;
        int c = Random.Range(minItem, maxItem);
        Vector2 size = Vector2.zero;
        for (int i = 0; i <= c; i++)
        {
            
            int a = Random.Range(3, DataBase.transform.childCount);
            var newItem = Instantiate(buycell,transform.position, Quaternion.identity);
            newItem.transform.parent = transform;
            newItem.GetComponent<BuyItem>().buyebleItem = DataBase.transform.GetChild(a).GetComponent<Item>();
            if (newItem.GetComponent<BuyItem>().buyebleItem.isWeapon)
            {
                newItem.GetComponent<BuyItem>().buyebleItem.Damege = Random.Range(5 + exb.Level, 5 * (exb.Level + 1) + 1);
                int elemental = Random.Range(1, 10);
                if (elemental == 1)
                {
                    newItem.GetComponent<BuyItem>().buyebleItem.DamageType = "Fire";
                    newItem.GetComponent<BuyItem>().buyebleItem.elementalDamage = Random.Range(1 + exb.Level, 4 * (exb.Level + 1) + 1);
                }
                if (elemental == 2)
                {
                    newItem.GetComponent<BuyItem>().buyebleItem.DamageType = "Frost";
                    newItem.GetComponent<BuyItem>().buyebleItem.elementalDamage = Random.Range(1 + exb.Level, 4 * (exb.Level + 1) + 1);
                }
            }
            if (newItem.GetComponent<BuyItem>().buyebleItem.isArmor) 
                newItem.GetComponent<BuyItem>().buyebleItem.Armor= Random.Range(10 + exb.Level, 10 * (exb.Level + 1) + 1);
            newItem.GetComponent<BuyItem>().buyebleItem.price=2*Random.Range(5 * (exb.Level + 1), 15 * (exb.Level + 1));
            
            size += new Vector2(0, buycell.GetComponent<RectTransform>().rect.height);
        }
        GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        GetComponent<RectTransform>().sizeDelta = size + GetComponent<RectTransform>().sizeDelta + new Vector2(0, 2*buycell.GetComponent<RectTransform>().rect.height);
    }

    public void AddSelledItem(Item item)
    {
        GetComponent<RectTransform>().sizeDelta += new Vector2(0, buycell.GetComponent<RectTransform>().rect.height);
        var newItem = Instantiate(buycell, transform.position, Quaternion.identity);
        newItem.transform.parent = transform;
        newItem.GetComponent<BuyItem>().buyebleItem = item;
    }

}

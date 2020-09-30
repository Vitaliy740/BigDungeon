using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    private Text nameItem;
    public Item selledItem;

    public Sprite fire;
    public Sprite frost;
    public Sprite dmg;
    public Sprite arm;
    private Image icon;
    public int NumItem;
    public int countItem;
    public Text price;

    private void Start()
    {
        icon = transform.GetChild(2).GetComponent<Image>();
        nameItem = transform.GetChild(1).GetComponent<Text>();
        nameItem.text = selledItem.nameItem;
        icon.sprite = selledItem.icon;
        if (countItem > 1) icon.transform.GetChild(0).GetComponent<Text>().text = countItem.ToString();
        if (selledItem.isWeapon)
        {
            nameItem.transform.GetChild(0).gameObject.SetActive(true);
            nameItem.transform.GetChild(0).GetComponent<Image>().sprite = dmg;
            nameItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = selledItem.Damege.ToString();
            if (selledItem.DamageType == "Fire")
            {
                nameItem.transform.GetChild(1).gameObject.SetActive(true);
                nameItem.transform.GetChild(1).GetComponent<Image>().sprite = fire;
                nameItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = selledItem.elementalDamage.ToString();
            }
            else if (selledItem.DamageType == "Frost")
            {
                nameItem.transform.GetChild(1).gameObject.SetActive(true);
                nameItem.transform.GetChild(1).GetComponent<Image>().sprite = frost;
                nameItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = selledItem.elementalDamage.ToString();
            }
        }
        if (selledItem.isArmor)
        {
            nameItem.transform.GetChild(0).gameObject.SetActive(true);
            nameItem.transform.GetChild(0).GetComponent<Image>().sprite = arm;
            nameItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = selledItem.Armor.ToString();
        }
        price.text += selledItem.price.ToString();
    }
    public void Selling()
    {
        Inventory.instanceInventory.ChangeGold(selledItem.price);
        Inventory.instanceInventory.RemoveItem(NumItem);
        countItem--;
        if (selledItem.id != 3 && selledItem.id != 6) transform.parent.GetComponent<SellPanel>().buy.AddSelledItem(selledItem);
        if (countItem > 0) icon.transform.GetChild(0).GetComponent<Text>().text = countItem.ToString();
        if (countItem<1)
            Destroy(gameObject);
        transform.parent.GetComponent<SellPanel>().UpdateBar();
    }

}


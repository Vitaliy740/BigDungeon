using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    private Text nameItem;
    public Item buyebleItem;
    public Sprite fire;
    public Sprite frost;
    public Sprite dmg;
    public Sprite arm;
    private Image icon;
    public Text price;

    private void Start()
    {
        UpdateBar();
    }
    public void UpdateBar()
    {
        icon = transform.GetChild(2).GetComponent<Image>();
        nameItem = transform.GetChild(1).GetComponent<Text>();

        nameItem.text = buyebleItem.nameItem;
        icon.sprite = buyebleItem.icon;
        if (buyebleItem.isWeapon)
        {
            nameItem.transform.GetChild(0).gameObject.SetActive(true);
            nameItem.transform.GetChild(0).GetComponent<Image>().sprite = dmg;
            nameItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = buyebleItem.Damege.ToString();
            if (buyebleItem.DamageType == "Fire")
            {
                nameItem.transform.GetChild(1).gameObject.SetActive(true);
                nameItem.transform.GetChild(1).GetComponent<Image>().sprite = fire;
                nameItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = buyebleItem.elementalDamage.ToString();
                buyebleItem.price += buyebleItem.elementalDamage*20;
            }
            else if (buyebleItem.DamageType == "Frost")
            {
                nameItem.transform.GetChild(1).gameObject.SetActive(true);
                nameItem.transform.GetChild(1).GetComponent<Image>().sprite = frost;
                nameItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = buyebleItem.elementalDamage.ToString();
                buyebleItem.price += buyebleItem.elementalDamage * 20;
            }
        }
        if (buyebleItem.isArmor)
        {
            nameItem.transform.GetChild(0).gameObject.SetActive(true);
            nameItem.transform.GetChild(0).GetComponent<Image>().sprite = arm;
            nameItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = buyebleItem.Armor.ToString();
        }
        price.text += buyebleItem.price.ToString();

    }
    public void Buying()
    {
        if (Inventory.instanceInventory.goldc < buyebleItem.price)
        {
            return;
        }
        Inventory.instanceInventory.ChangeGold(-buyebleItem.price);
        buyebleItem.price /= 2;
        Inventory.instanceInventory.AddItem(buyebleItem);
        transform.parent.GetComponent<BuyPanel>().sel.UpdateBar();
        if (buyebleItem.id != 3 && buyebleItem.id != 6)
        {
            transform.parent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, GetComponent<RectTransform>().rect.height);
            Destroy(gameObject);
        }
    }

}

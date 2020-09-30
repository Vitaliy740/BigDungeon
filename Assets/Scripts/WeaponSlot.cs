using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour
{
    public Item weapon;



    public void DisplayItem()
    {
        Item currentItem = weapon;

        Image icon = transform.GetChild(0).GetComponent<Image>();

        if (currentItem.id != 0)
        {
            icon.enabled = true;
            Sprite itemIcon = currentItem.icon;
            icon.sprite = itemIcon;

        }
        else
        {
            icon.enabled = false;
            icon.sprite = null;
        }
    }


    void Start()
    {
        weapon = GameObject.Find("HandWeapon").transform.GetChild(0).GetComponent<Item>();
        DisplayItem();
    }

}

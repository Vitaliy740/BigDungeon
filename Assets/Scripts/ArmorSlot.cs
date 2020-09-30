using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlot : MonoBehaviour
{
    public Item Armor;
    // Start is called before the first frame update

    public void DisplayItem()
    {
        Item currentItem = Armor;

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
        DisplayItem();
    }


}

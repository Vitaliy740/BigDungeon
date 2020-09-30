using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject isDragingObject;
    private Vector3 normalSize;
    void Start()
    {
        normalSize= GameObject.Find("NormalSize").transform.localScale;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject dragPrefab = Inventory.instanceInventory.dragPrefab;
        isDragingObject = gameObject;
        
        
        dragPrefab.transform.GetChild(0).GetComponent<Text>().enabled = true;
        


            dragPrefab.transform.GetChild(0).GetComponent<Text>().enabled = false;

            if (isDragingObject.GetComponent<CurrentItem>())
            {
                dragPrefab.GetComponent<Image>().sprite = GetComponent<CurrentItem>().currentInventoryItem.icon;
            }

            if (isDragingObject.GetComponent<CurrentFavouriteItem>())
            {
                dragPrefab.GetComponent<Image>().sprite = GetComponent<CurrentFavouriteItem>().currentFavouriteItem.icon;
            }


        if (dragPrefab.GetComponent<Image>().sprite != null)
            {
                dragPrefab.SetActive(true);
                dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                isDragingObject = null;
            }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Inventory.instanceInventory.dragPrefab.transform.position = eventData.position ;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject && eventData.pointerCurrentRaycast.gameObject.name == "WP" && isDragingObject.GetComponent<CurrentItem>().currentInventoryItem.isWeapon)
        {

            Item wp = Inventory.instanceInventory.weaponSlot.weapon;
            Inventory.instanceInventory.weaponSlot.weapon = isDragingObject.GetComponent<CurrentItem>().currentInventoryItem;
            Inventory.instanceInventory.weaponSlot.DisplayItem();
            GameObject.Find("Игрок").GetComponent<Control>().ChangeWeapon();

            for (int i = 0; i < Inventory.instanceInventory.database.transform.childCount; i++)
            {

                Item item = Inventory.instanceInventory.database.transform.GetChild(i).GetComponent<Item>();

                if (item.id == isDragingObject.GetComponent<CurrentItem>().currentInventoryItem.id)
                {
                    var preWeapon = GameObject.Find("HandWeapon").transform.GetChild(0);
                    var newWeapon = Instantiate(Inventory.instanceInventory.database.transform.GetChild(i), preWeapon.transform.position, Quaternion.identity);
                    newWeapon.transform.rotation = preWeapon.transform.rotation;
                    newWeapon.transform.parent = preWeapon.parent;
                    Destroy(GameObject.Find("HandWeapon").transform.GetChild(0).gameObject);
                    newWeapon.tag = "Player Veapon";

                    for (int j = 0; i < Inventory.instanceInventory.item.Count; j++)
                        if (Inventory.instanceInventory.item[j].id == newWeapon.GetComponent<Item>().id)
                        {
                            newWeapon.GetComponent<Item>().Damege = Inventory.instanceInventory.item[j].Damege;
                            newWeapon.GetComponent<Item>().DamageType = Inventory.instanceInventory.item[j].DamageType;
                            newWeapon.GetComponent<Item>().elementalDamage = Inventory.instanceInventory.item[j].elementalDamage;
                            break;
                        }
                    Vector3 f;
                    if (preWeapon.localScale.sqrMagnitude < newWeapon.localScale.sqrMagnitude)
                    {
                        f = new Vector3(normalSize.x + newWeapon.localScale.x, normalSize.y + newWeapon.localScale.y);
                    }
                    else
                    {
                        f = new Vector3(normalSize.x - newWeapon.localScale.x, normalSize.y - newWeapon.localScale.y);
                    }

                    newWeapon.localScale += f;
                    break;
                }
            }
            Inventory.instanceInventory.RemoveItem(isDragingObject.GetComponent<CurrentItem>().ItemNum);
            Inventory.instanceInventory.EmptySlot();
            Inventory.instanceInventory.AddItem(wp);

            wp = null;
        }
        else if (eventData.pointerCurrentRaycast.gameObject && eventData.pointerCurrentRaycast.gameObject.name == "AR" && isDragingObject.GetComponent<CurrentItem>().currentInventoryItem.isArmor)
        {
            Item ar = Inventory.instanceInventory.armorSlot.Armor;
            Inventory.instanceInventory.armorSlot.Armor = isDragingObject.GetComponent<CurrentItem>().currentInventoryItem;
            Inventory.instanceInventory.armorSlot.DisplayItem();

            for (int i = 0; i < Inventory.instanceInventory.database.transform.childCount; i++)
            {

                Item item = Inventory.instanceInventory.database.transform.GetChild(i).GetComponent<Item>();
                Debug.Log(item.id);
                if (item.id == isDragingObject.GetComponent<CurrentItem>().currentInventoryItem.id)
                {

                    for (int j = 0; j < Inventory.instanceInventory.item.Count; j++)
                        if (Inventory.instanceInventory.item[j].id == Inventory.instanceInventory.armorSlot.Armor.id)
                        {
                            Inventory.instanceInventory.armorSlot.Armor.Armor = Inventory.instanceInventory.item[j].Armor;
                            break;
                        }
                    break;
                }
            }
            Inventory.instanceInventory.RemoveItem(isDragingObject.GetComponent<CurrentItem>().ItemNum);
            Inventory.instanceInventory.EmptySlot();
            Inventory.instanceInventory.AddItem(ar);

        }
        else if (!EventSystem.current.IsPointerOverGameObject() && isDragingObject.GetComponent<CurrentItem>())
            {

            Inventory.instanceInventory.DropedItem(isDragingObject.GetComponent<CurrentItem>().currentInventoryItem.id);
            Inventory.instanceInventory.RemoveItem(isDragingObject.GetComponent<CurrentItem>().ItemNum);


            }

        isDragingObject = null;
        GameObject dragPrefab = Inventory.instanceInventory.dragPrefab;
        dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
        dragPrefab.GetComponent<Image>().sprite = null;
        dragPrefab.SetActive(false);
    }
    public Item dragedItem(Item item)
    {
        for (int i = 0; i < Inventory.instanceInventory.database.transform.childCount; i++)
        {
            Item temp = Inventory.instanceInventory.database.transform.GetChild(i).GetComponent<Item>();
            if (temp.id == item.id)
            {
                return temp;
            }
        }
        return null;
    }

    public void AddItem(Item item)
    {
        item.countItem++;
    }
}

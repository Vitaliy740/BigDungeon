using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    private int itemNum;
    private Text descriptionItem;
    void Start()
    {
        descriptionItem = transform.parent.parent.GetChild(5).transform.GetChild(2).GetComponent<Text>();
    }
    public Item currentInventoryItem
    {
        get { return Inventory.instanceInventory.item[ItemNum]; }
        set { Inventory.instanceInventory.item[ItemNum] = value; }
    }

    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        descriptionItem.transform.parent.GetChild(3).gameObject.SetActive(false);
        descriptionItem.transform.parent.GetChild(4).gameObject.SetActive(false);
        descriptionItem.transform.parent.GetChild(6).gameObject.SetActive(false);
        descriptionItem.transform.parent.GetChild(7).gameObject.SetActive(false);
        descriptionItem.transform.parent.GetChild(8).GetComponent<Text>().text = "";
        descriptionItem.transform.parent.GetChild(5).GetComponent<Text>().text = "";
        descriptionItem.transform.GetChild(0).GetComponent<Text>().text = currentInventoryItem.nameItem;
        descriptionItem.text = currentInventoryItem.descriptionItem;
            if (currentInventoryItem.OnItemUse != null)
            {
                currentInventoryItem.OnItemUse.Invoke();
            }
            if (currentInventoryItem.isRemovable)
            {
                Inventory.instanceInventory.RemoveItem(ItemNum);
            }

        if (currentInventoryItem.isWeapon)
        {
            descriptionItem.transform.parent.GetChild(3).gameObject.SetActive(true);
            descriptionItem.transform.parent.GetChild(5).GetComponent<Text>().text = currentInventoryItem.Damege.ToString();
            if (currentInventoryItem.DamageType == "Fire")
            {
                descriptionItem.transform.parent.GetChild(6).gameObject.SetActive(true);
                descriptionItem.transform.parent.GetChild(8).GetComponent<Text>().text= currentInventoryItem.elementalDamage.ToString();
            }
            if (currentInventoryItem.DamageType == "Frost")
            {
                descriptionItem.transform.parent.GetChild(7).gameObject.SetActive(true);
                descriptionItem.transform.parent.GetChild(8).GetComponent<Text>().text = currentInventoryItem.elementalDamage.ToString();
            }
        }
        if (currentInventoryItem.isArmor)
        {
            descriptionItem.transform.parent.GetChild(4).gameObject.SetActive(true);
            descriptionItem.transform.parent.GetChild(5).GetComponent<Text>().text = currentInventoryItem.Armor.ToString();
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = Drag.isDragingObject;

        if (dragedObject == null)
        {
            return;
        }

        CurrentItem dragedCurrentItem = dragedObject.GetComponent<CurrentItem>();
        CurrentFavouriteItem dragedFavouriteItem = dragedObject.GetComponent<CurrentFavouriteItem>();
        WeaponSlot draggedWeapon = dragedObject.GetComponent<WeaponSlot>();
        Drag drag = Drag.isDragingObject.GetComponent<Drag>();


            if (dragedObject.GetComponent<CurrentItem>())
            {
                if (dragedCurrentItem.currentInventoryItem.id == GetComponent<CurrentItem>().currentInventoryItem.id)
                {
                    if (dragedCurrentItem.currentInventoryItem.isStackable)
                    {
                        int count = dragedCurrentItem.currentInventoryItem.countItem;
                        GetComponent<CurrentItem>().currentInventoryItem.countItem += count;
                        dragedCurrentItem.currentInventoryItem = Inventory.instanceInventory.EmptySlot();
                    }
                }
                else
                {
                    Item currentItem = GetComponent<CurrentItem>().currentInventoryItem;
                    GetComponent<CurrentItem>().currentInventoryItem = dragedCurrentItem.currentInventoryItem;
                    dragedCurrentItem.currentInventoryItem = currentItem;
                }
                
            }
        if (dragedObject.GetComponent<CurrentFavouriteItem>())
            {
                if (dragedFavouriteItem.currentFavouriteItem.id == GetComponent<CurrentItem>().currentInventoryItem.id)
                {
                    if (dragedFavouriteItem.currentFavouriteItem.isStackable)
                    {
                        int count = dragedFavouriteItem.currentFavouriteItem.countItem;
                        GetComponent<CurrentItem>().currentInventoryItem.countItem += count;
                        dragedFavouriteItem.currentFavouriteItem = Inventory.instanceInventory.EmptySlot();
                    }
                }
                else
                {
                    Item currentItem = GetComponent<CurrentItem>().currentInventoryItem;
                    GetComponent<CurrentItem>().currentInventoryItem = dragedFavouriteItem.currentFavouriteItem;
                    dragedFavouriteItem.currentFavouriteItem = currentItem;
                }
            }

           

            Inventory.instanceInventory.DisplayItems();
            
            if(Inventory.instanceInventory.favourite)
                Inventory.instanceInventory.favourite.DisplayItems();
        Inventory.instanceInventory.weaponSlot.DisplayItem();


        //if (eventData.button == PointerEventData.InputButton.Right)
        //{
        //    if (GetComponent<CurrentItem>().currentInventoryItem.id == Inventory.instanceInventory.EmptySlotID())
        //    {
        //        if (dragedCurrentItem)
        //        {
        //            Item dragItem = dragedCurrentItem.currentInventoryItem.getCopy();
        //            dragItem.countItem = 1;
        //            GetComponent<CurrentItem>().currentInventoryItem = dragItem;
        //            Inventory.instanceInventory.RemoveItem(dragedCurrentItem.ItemNum);
        //            return;
        //        }

        //        if (dragedFavouriteItem)
        //        {
        //            Item dragItem = dragedFavouriteItem.currentFavouriteItem.getCopy();
        //            dragItem.countItem = 1;
        //            GetComponent<CurrentItem>().currentInventoryItem = dragItem;
        //            Inventory.instanceInventory.favourite.RemoveItem(dragedFavouriteItem.ItemNum);
        //            Inventory.instanceInventory.DisplayItems();
        //            return;
        //        }
        //        if (dragedStorageItem)
        //        {
        //            Item dragItem = dragedStorageItem.currentStorageItem.getCopy();
        //            dragItem.countItem = 1;
        //            GetComponent<CurrentItem>().currentInventoryItem = dragItem;
        //            Storage.RemoveItem(dragedStorageItem.itemNum);
        //            Inventory.instanceInventory.DisplayItems();
        //            return;
        //        }
        //    }

        //    if (dragedCurrentItem)
        //    {
        //        if (GetComponent<CurrentItem>().currentInventoryItem.id == dragedCurrentItem.currentInventoryItem.id)
        //        {
        //            if (dragedCurrentItem.currentInventoryItem.isStackable)
        //            {
        //                drag.AddItem(GetComponent<CurrentItem>().currentInventoryItem);
        //                Inventory.instanceInventory.RemoveItem(dragedCurrentItem.ItemNum);
        //                return;
        //            }
        //        }
        //    }

        //    if (dragedFavouriteItem)
        //    {
        //        if (GetComponent<CurrentItem>().currentInventoryItem.id == dragedFavouriteItem.currentFavouriteItem.id)
        //        {
        //            if (dragedFavouriteItem.currentFavouriteItem.isStackable)
        //            {
        //                drag.AddItem(GetComponent<CurrentItem>().currentInventoryItem);
        //                Inventory.instanceInventory.favourite.RemoveItem(dragedFavouriteItem.ItemNum);
        //                Inventory.instanceInventory.DisplayItems();
        //                return;
        //            }
        //        }
        //    }

        //    if (dragedStorageItem)
        //    {
        //        if (GetComponent<CurrentItem>().currentInventoryItem.id == dragedStorageItem.currentStorageItem.id)
        //        {
        //            if (dragedStorageItem.currentStorageItem.isStackable)
        //            {
        //                drag.AddItem(GetComponent<CurrentItem>().currentInventoryItem);
        //                Storage.RemoveItem(dragedStorageItem.itemNum);
        //                Inventory.instanceInventory.DisplayItems();
        //                return;
        //            }
        //        }
        //    }
        //}
    }

}









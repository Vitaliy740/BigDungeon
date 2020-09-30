using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CurrentFavouriteItem : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private int itemNum;

    public Item currentFavouriteItem
    {
        get { return Inventory.instanceInventory.favourite.favouriteItem[ItemNum]; }
        set { Inventory.instanceInventory.favourite.favouriteItem[ItemNum] = value; }
    }

    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }



    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = Drag.isDragingObject;

        if (dragedObject == null)
        {
            return;
        }

        Drag drag = Drag.isDragingObject.GetComponent<Drag>();

        CurrentFavouriteItem dragedFavouriteItem = dragedObject.GetComponent<CurrentFavouriteItem>();
        CurrentItem dragedCurrentItem = dragedObject.GetComponent<CurrentItem>();
       

        if (dragedObject.GetComponent<CurrentFavouriteItem>())
            {
                if (dragedFavouriteItem.currentFavouriteItem.id == GetComponent<CurrentFavouriteItem>().currentFavouriteItem.id)
                {
                    if (dragedFavouriteItem.currentFavouriteItem.isStackable)
                    {
                        int count = dragedFavouriteItem.currentFavouriteItem.countItem;
                        GetComponent<CurrentFavouriteItem>().currentFavouriteItem.countItem += count;
                        dragedFavouriteItem.currentFavouriteItem = Inventory.instanceInventory.EmptySlot();
                    }
                }
                else
                {
                    Item currentItem = GetComponent<CurrentFavouriteItem>().currentFavouriteItem;
                    GetComponent<CurrentFavouriteItem>().currentFavouriteItem = dragedFavouriteItem.currentFavouriteItem;
                    dragedFavouriteItem.currentFavouriteItem = currentItem;
                } 
            }

            if (dragedObject.GetComponent<CurrentItem>())
            {
                if (dragedCurrentItem.currentInventoryItem.id == GetComponent<CurrentFavouriteItem>().currentFavouriteItem.id)
                {
                    if (dragedCurrentItem.currentInventoryItem.isStackable)
                    {
                        int count = dragedCurrentItem.currentInventoryItem.countItem;
                        GetComponent<CurrentFavouriteItem>().currentFavouriteItem.countItem += count;
                        dragedCurrentItem.currentInventoryItem = Inventory.instanceInventory.EmptySlot();
                    }
                }
                else
                {
                    Item currentFavouriteItem = GetComponent<CurrentFavouriteItem>().currentFavouriteItem;
                    GetComponent<CurrentFavouriteItem>().currentFavouriteItem = dragedCurrentItem.currentInventoryItem;
                    dragedCurrentItem.currentInventoryItem = currentFavouriteItem;
                }
            }


        //if (dragedObject.GetComponent<CyrrentWeapon>())
        //{
        //    if (draggedWeapopn.currentWeapon.id == GetComponent<CyrrentWeapon>().currentWeapon.id)
        //    {
        //        if (draggedWeapopn.currentWeapon.isStackable)
        //        {
        //            int count = draggedWeapopn.currentWeapon.countItem;
        //            GetComponent<CyrrentWeapon>().currentWeapon.countItem += count;
        //            draggedWeapopn.currentWeapon = Inventory.instanceInventory.EmptySlot();
        //        }
        //    }
        //    else
        //    {
        //        Item currentWeapon = GetComponent<CyrrentWeapon>().currentWeapon;
        //        GetComponent<CyrrentWeapon>().currentWeapon = draggedWeapopn.currentWeapon;
        //        draggedWeapopn.currentWeapon = currentWeapon;
        //    }

        //}
        Inventory.instanceInventory.DisplayItems();
            
            Inventory.instanceInventory.favourite.DisplayItems();
        


            if (GetComponent<CurrentFavouriteItem>().currentFavouriteItem.id == Inventory.instanceInventory.EmptySlotID())
            {
                if (dragedCurrentItem)
                {
                    Item dragItem = dragedCurrentItem.currentInventoryItem.getCopy();
                    dragItem.countItem = 1;
                    GetComponent<CurrentFavouriteItem>().currentFavouriteItem = dragItem;
                    Inventory.instanceInventory.RemoveItem(dragedCurrentItem.ItemNum);
                    Inventory.instanceInventory.favourite.DisplayItems();
                Inventory.instanceInventory.weaponSlot.DisplayItem();
                return;
                }
                if (dragedFavouriteItem)
                {
                    Item dragItem = dragedFavouriteItem.currentFavouriteItem.getCopy();
                    dragItem.countItem = 1;
                    GetComponent<CurrentFavouriteItem>().currentFavouriteItem = dragItem;
                    Inventory.instanceInventory.favourite.RemoveItem(dragedFavouriteItem.ItemNum);
                Inventory.instanceInventory.weaponSlot.DisplayItem();
                return;
                }


    Inventory.instanceInventory.favourite.DisplayItems();
           
        }
           
            if (dragedCurrentItem)
            {
                if (GetComponent<CurrentFavouriteItem>().currentFavouriteItem.id == dragedCurrentItem.currentInventoryItem.id)
                {
                    if (dragedCurrentItem.currentInventoryItem.isStackable)
                    {
                        drag.AddItem(GetComponent<CurrentFavouriteItem>().currentFavouriteItem);
                        Inventory.instanceInventory.RemoveItem(dragedCurrentItem.ItemNum);
                        Inventory.instanceInventory.favourite.DisplayItems();
                    Inventory.instanceInventory.weaponSlot.DisplayItem();
                    return;
                    }
                }
            }



            if (dragedFavouriteItem)
            {
                if (GetComponent<CurrentFavouriteItem>().currentFavouriteItem.id == dragedFavouriteItem.currentFavouriteItem.id)
                {
                    if (dragedFavouriteItem.currentFavouriteItem.isStackable)
                    {
                        drag.AddItem(GetComponent<CurrentFavouriteItem>().currentFavouriteItem);
                        Inventory.instanceInventory.favourite.RemoveItem(dragedFavouriteItem.ItemNum);
                    Inventory.instanceInventory.weaponSlot.DisplayItem();
                    return;
                    }
                }
            }


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentFavouriteItem.OnItemUse != null)
        {
            currentFavouriteItem.OnItemUse.Invoke();
        }
        if (currentFavouriteItem.isRemovable)
        {
            Inventory.instanceInventory.favourite.RemoveItem(ItemNum);
        }
    }
}

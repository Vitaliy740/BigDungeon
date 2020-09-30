using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel : MonoBehaviour
{
    public GameObject sellcell;
    public BuyPanel buy;
    public Text gold;
    void Start()
    {
        UpdateBar();
    }
    public void UpdateBar()
    {
        Vector2 size = Vector2.zero;
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
        for (int i = 0; i < Inventory.instanceInventory.item.Count; i++)
        {
            if (Inventory.instanceInventory.item[i].id != 0)
            {
                var newItem = Instantiate(sellcell, transform.position, Quaternion.identity);
                newItem.transform.parent = transform;
                newItem.GetComponent<SellItem>().selledItem = Inventory.instanceInventory.item[i];
                newItem.GetComponent<SellItem>().NumItem = i;
                newItem.GetComponent<SellItem>().countItem = Inventory.instanceInventory.item[i].countItem;
                size += new Vector2(0,  sellcell.GetComponent<RectTransform>().rect.height);
            }
        }
        GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        GetComponent<RectTransform>().sizeDelta = size + GetComponent<RectTransform>().sizeDelta;
        gold.text = Inventory.instanceInventory.goldc.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

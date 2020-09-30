using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instanceInventory;

    public GameObject database;
    public Button TakeButton;
    public GameObject TakingArea;
    public bool buttonClicked = false;
    [HideInInspector]
    public List<Item> item;
    [HideInInspector]
    public int lastid=-1;
    public GameObject inventoryPanel;
    public bool addposibility=true;
    [SerializeField]
    private GameObject cellContainer;
    [Space(10)]
    

    [Space(10)]
    public GameObject dragPrefab;
    [Space(10)]
    public Favourite favourite;
    public WeaponSlot weaponSlot;
    public ArmorSlot armorSlot;

    public Text gold;
    public int goldc=0;
    [Space(10)]
    public Transform dropPlace;

    void Awake()
    {
        instanceInventory = this;
    }

    // Use this for initialization
    void Start()
    {
        InitInventory();
        DisplayItems();
        NumItems();

        RenameCells();
        RenameIcons();
        HidePanel();
    }

    void InitInventory()
    {
        item = new List<Item>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            item.Add(EmptySlot());
        }
    }

    public Item EmptySlot()
    {
        return new Item();
    }

    public void AddUnStackableItem(Item currentItem)
    {
        if (currentItem.id == 13)
        {
            ChangeGold(currentItem.price);
            return;
        }
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == EmptySlotID())
            {
                item[i] = currentItem;
                item[i].countItem = 1;
                int kol = 0;
                //Destroy(currentItem.gameObject);
                for (int j = 0; j < item.Count; j++)
                    if (item[j].id == EmptySlotID())
                    {
                        kol ++;
                    }
                if (kol < 1) addposibility = false;
                DisplayItems();

                break;
            }
        }
    }
    public void ChangeGold(int amount)
    {
        goldc += amount;
        gold.text = goldc.ToString();
    }
    public int EmptySlotID()
    {
        return 0;
    }

    void AddStackableItem(Item currentItem)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == currentItem.id)
            {
                item[i].countItem++;
                DisplayItems();
                //Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnStackableItem(currentItem);
    }

    public void AddItem(Item currentItem)
    {
        if (currentItem.isStackable)
        {
            AddStackableItem(currentItem);
        }
        else
            AddUnStackableItem(currentItem);        
    }

    void NumItems()
    {
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            cell.GetComponent<CurrentItem>().ItemNum = i;
        }
    }

    void RenameCells()
    {
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            cell.name = "Cell " + i.ToString();
        }
    }

    void RenameIcons()
    {
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            icon.name = "Icon " + i.ToString();
            addposibility = true;
        }
    }
   
    void Update()
    {
        TakeButton.onClick.AddListener(TakeItem);
        if (buttonClicked)
        {
            buttonClicked = false;
            if (TakingArea.GetComponent<TakingButton>().Item != null)
            {
            AddItem(TakingArea.GetComponent<TakingButton>().Item);
            Destroy(TakingArea.GetComponent<TakingButton>().Item.gameObject);
            }

        }
    }

    void TakeItem()
    {
        buttonClicked = true;
    }

    void CheckEmptyItem(Item item)
    {
        if (item.id == EmptySlotID())
        {
            return;
        }
    }


    void HidePanel()
    {
        inventoryPanel.SetActive(false);
        weaponSlot.gameObject.SetActive(false);
        armorSlot.gameObject.SetActive(false);
        //if(GameObject.Find("GameMenues"))
        //    GameObject.Find("GameMenues").SetActive(false);
    }

    public void DisplayItems()
    {   
        
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            Item currentItem = item[i];
            Transform cell = cellContainer.transform.GetChild(i);

            Image icon = cell.transform.GetChild(0).GetComponent<Image>();
            Text count = icon.transform.GetChild(0).GetComponent<Text>();

            if (currentItem.id != EmptySlotID())
            {
                icon.enabled = true;
                Sprite itemIcon = currentItem.icon;
                icon.sprite = itemIcon;
                count.text = null;

                if (currentItem.isStackable)
                {
                    if (currentItem.countItem > 1)
                    {
                        count.text = currentItem.countItem.ToString();
                    }
                    else
                    {
                        count.text = null;
                    }
                }
            }
            else
            {
                icon.enabled = false;
                icon.sprite = null;
                count.text = null;
            }
        }
    }

    public void RemoveItem(int numItem)
    {
        if (item[numItem].countItem > 1)
        {
            item[numItem].countItem--;
        }
        else
        {
            item[numItem] = EmptySlot();
        }
        addposibility = true;
        DisplayItems();
    }

    public void DropedItem(int id)
    {
        for (int i = 0; i < database.transform.childCount; i++)
        {
            Item itemm = database.transform.GetChild(i).GetComponent<Item>();
            if (itemm.id == id)
            {
                GameObject obj = Instantiate(itemm.gameObject);
                if (obj.GetComponent<Item>().isWeapon)
                    for (int j = 0; i < item.Count; j++)
                        if (item[j].id == obj.GetComponent<Item>().id)
                        {
                            obj.GetComponent<Item>().Damege = item[j].Damege;
                            obj.GetComponent<Item>().DamageType = item[j].DamageType;
                            obj.GetComponent<Item>().elementalDamage = item[j].elementalDamage;
                            break;
                        }

                if (obj.GetComponent<Item>().isArmor)
                    for (int j = 0; i < item.Count; j++)
                        if (item[j].id == obj.GetComponent<Item>().id)
                        {
                            obj.GetComponent<Item>().Armor = item[j].Armor;
                            break;
                        }
                obj.transform.position = new Vector3(dropPlace.position.x, dropPlace.position.y, dropPlace.position.z);
            }
        
    }
    }
    public bool isExistItem(int id)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemID(int id)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == id)
            {
                if (item[i].countItem > 1)
                {
                    item[i].countItem--;
                }
                else
                {
                    item[i] = EmptySlot();
                }
                DisplayItems();
            }
        }
    }
}

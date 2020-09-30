using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakingButton : MonoBehaviour {
    public GameObject takeButton;
    public Image Standartimg;
    public Text itemName;
    public Button takingButton;
    public Text Damage;
    public Text ElementalDamage;
    public Text armor;
    public Item Item;
    public Sprite fire;
    public Sprite freeze;
    private bool pressed=false;
    private GameObject chest=null;
    public Text gold;
    private GameObject npc = null;
    private bool dialog = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.gameObject.tag!="Player Veapon" && collision.gameObject.tag != "Enemy Veapon"&&Inventory.instanceInventory.addposibility)
        {
            itemName.transform.GetChild(0).GetComponent<Text>().text = "Подобрать:";
            itemName.color = Color.black;
            Damage.gameObject.SetActive(false);
            takeButton.SetActive(true);
            armor.gameObject.SetActive(false);
            Standartimg.gameObject.SetActive(false);
            ElementalDamage.text = "";
            string dopinf = "";
            if (collision.gameObject.GetComponent<Item>().isWeapon)
            {
                Damage.text = collision.gameObject.GetComponent<Item>().Damege.ToString();
                Damage.gameObject.SetActive(true);
                if (collision.gameObject.GetComponent<Item>().DamageType == "Fire")
                {
                    itemName.color = Color.blue;
                    Standartimg.sprite = fire;
                    Standartimg.gameObject.SetActive(true);
                    ElementalDamage.text = collision.gameObject.GetComponent<Item>().elementalDamage.ToString();
                    ElementalDamage.gameObject.SetActive(true);
                    dopinf = " огня";
                }
                if (collision.gameObject.GetComponent<Item>().DamageType == "Frost")
                {
                    itemName.color = Color.blue;
                    Standartimg.sprite = freeze;
                    Standartimg.gameObject.SetActive(true);
                    ElementalDamage.text = collision.gameObject.GetComponent<Item>().elementalDamage.ToString();
                    ElementalDamage.gameObject.SetActive(true);
                    dopinf = " льда";
                }
                gold.gameObject.SetActive(false);
            }
            if (collision.gameObject.GetComponent<Item>().isArmor)
            {
                gold.gameObject.SetActive(false);
                armor.text = collision.gameObject.GetComponent<Item>().Armor.ToString();
                armor.gameObject.SetActive(true);
            }
            if (collision.GetComponent<Item>().id == 13)
            {
                gold.gameObject.SetActive(true);
                gold.text = collision.GetComponent<Item>().price.ToString();
            }
            Item = collision.GetComponent<Item>();
            itemName.text = collision.GetComponent<Item>().nameItem +dopinf;
        }
        else if (collision.gameObject.layer == 12)
        {
            itemName.transform.GetChild(0).GetComponent<Text>().text = "Поговорить:";
            itemName.text = collision.GetComponent<DialogTrigger>().dialog.name;
            itemName.color = Color.black;
            takeButton.SetActive(true);
            takingButton.onClick.AddListener(delegate { dialog=true; });
            npc= collision.gameObject;
        }
        if (collision.gameObject.layer==11 && !collision.transform.GetChild(0).gameObject.activeSelf)
        {
            itemName.transform.GetChild(0).GetComponent<Text>().text = "Открыть:";
            itemName.text = collision.GetComponent<ChestDrop>().Rare+"Сундук";
            Damage.gameObject.SetActive(false);
            armor.gameObject.SetActive(false);
            takeButton.SetActive(true);
            Standartimg.gameObject.SetActive(false);
            ElementalDamage.text = "";
            takingButton.onClick.AddListener(delegate { pressed = true; });
            chest = collision.gameObject;
        }
    }

    private void Update()
    {
        if (pressed)
        {
            pressed = false;
            chest.GetComponent<ChestDrop>().Drop();
            takeButton.SetActive(false);

        }
        if (dialog)
        {
            dialog = false;
            npc.GetComponent<DialogTrigger>().TriggerDialog();
            takeButton.SetActive(false);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
        chest = null;
        Item = null;
        gold.text = "";
        Damage.gameObject.SetActive(false);
        armor.gameObject.SetActive(false);
        gold.gameObject.SetActive(false);
        Standartimg.gameObject.SetActive(false);
        takeButton.SetActive(false);
    }
}

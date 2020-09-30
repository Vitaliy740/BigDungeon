using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Item : MonoBehaviour {
    public string nameItem;
    [Range(1,999)]
    public int id;
    [HideInInspector]
    public int countItem = 1;
    public bool isStackable;
    [TextArea(5,100)]
    public string descriptionItem;
    public Sprite icon;
    public int price;
    public bool isRemovable;
    public bool isDroped;
    public bool isWeapon;
    public bool isArmor;
    public int Damege;
    public string DamageType;
    public int elementalDamage;
    public string ArmorType;
    public int Armor;

    public UnityEvent OnItemUse;

    public Item getCopy()
    {
        return (Item)MemberwiseClone();
    }
}

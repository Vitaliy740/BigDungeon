using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.Events;
[Serializable]
public class SaveLoad : MonoBehaviour
{
    string filepath;
    private List<GameObject> enemyes = new List<GameObject>();
    private GameObject[] enemy;
    private List<GameObject> skills = new List<GameObject>();
    private GameObject[] items;
    public GameObject skill;
    public GameObject player;
    public Vector3 normalSize;
    public GameObject GameMenues;
    private Expirience exp;
    
    // Start is called before the first frame update
    private void Start()
    {
        
        exp = FindObjectOfType<Expirience>();
        normalSize = GameObject.Find("NormalSize").transform.localScale;
        filepath = Application.persistentDataPath +"/save.gamesave";
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 6; i < skill.transform.childCount-5; i++)
        {
            skills.Add(skill.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < FindObjectsOfType<EnemyAI>().Length; i++)
        {
            enemyes.Add(enemy[i]);
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filepath, FileMode.Create);
        Save save = new Save();
        save.SaveEnemyes(enemyes);
        save.SaveSkills(skills);
        save.Ppos.x = player.transform.position.x;
        save.Ppos.y = player.transform.position.y;
        save.Ppos.z = player.transform.position.z;
        save.gold = Inventory.instanceInventory.goldc;
        save.Phealth = player.GetComponent<HealthBar>().health;
        save.MaxPhealth = player.GetComponent<HealthBar>().maxhealth;
        save.Pmana = player.GetComponent<ManaBar>().mana;
        save.MaxPmana = player.GetComponent<ManaBar>().maxmana;
        save.curExp = exp.CurrentExp;
        save.curNexp = exp.neededExperience;
        save.curLevel = exp.Level;
        save.lvlcout = skill.GetComponent<SkillPanel>().coutskill;
        bf.Serialize(fs, save);
        fs.Close();
        for (int i = 0; i < Inventory.instanceInventory.item.Count; i++)
        {
            Item it = new Item();
            it = Inventory.instanceInventory.item[i];
            SavedItem json = new SavedItem();
            json = SavingItem(json, it);
            string item = JsonUtility.ToJson(json);
            string fipath = Application.persistentDataPath  + "/" + "invitem" + (i + 1).ToString()+".txt";
            File.WriteAllText(fipath, item);
        }

       for(int i = 0; i < Inventory.instanceInventory.favourite.favouriteItem.Count; i++)
        {
            Item it = new Item();
            it = Inventory.instanceInventory.favourite.favouriteItem[i];
            SavedItem json = new SavedItem();
            json = SavingItem(json, it);
            string favItem = JsonUtility.ToJson(json);
            string favipath= Application.persistentDataPath + "/" + "favitem" + (i + 1).ToString() + ".txt";
            File.WriteAllText(favipath, favItem);
        }

        SavedItem wp = new SavedItem();
        wp = SavingItem(wp, Inventory.instanceInventory.weaponSlot.weapon);
        string itemwp = JsonUtility.ToJson(wp);
        string wppath = Application.persistentDataPath  + "/" + "wpslot";
        File.WriteAllText(wppath, itemwp);
        
        SavedItem ar = new SavedItem();
        ar = SavingItem(ar, Inventory.instanceInventory.armorSlot.Armor);
        
        string itemar = JsonUtility.ToJson(ar);
        string arpath = Application.persistentDataPath  + "/" + "arslot";
        File.WriteAllText(arpath, itemar);
    }
    private SavedItem SavingItem(SavedItem json,Item it)
    {
        json.nameItem = it.nameItem;
        json.id = it.id;
        json.countItem = it.countItem;
        json.isStackable = it.isStackable;
        json.descriptionItem = it.descriptionItem;
        json.icon = it.icon;
        json.price = it.price;
        json.isRemovable = it.isRemovable;
        json.isDroped = it.isDroped;
        json.isWeapon = it.isWeapon;
        json.isArmor = it.isArmor;
        json.Damege = it.Damege;
        json.DamageType = it.DamageType;
        json.elementalDamage = it.elementalDamage;
        json.Armor = it.Armor;
        json.OnItemUse = it.OnItemUse;
        return json;
    }
    private Item LoadingItem(Item json, SavedItem it)
    {
        json.nameItem = it.nameItem;
        json.id = it.id;
        json.countItem = it.countItem;
        json.isStackable = it.isStackable;
        json.descriptionItem = it.descriptionItem;
        json.icon = it.icon;
        json.price = it.price;
        json.isRemovable = it.isRemovable;
        json.isDroped = it.isDroped;
        json.isWeapon = it.isWeapon;
        json.isArmor = it.isArmor;
        json.Damege = it.Damege;
        json.DamageType = it.DamageType;
        json.elementalDamage = it.elementalDamage;
        json.Armor = it.Armor;
        json.OnItemUse = it.OnItemUse;
        return json;
    }
    // Update is called once per frame
    public void Load()
    {
        Debug.Log(!File.Exists(filepath));
        if (!File.Exists(filepath))
            return;
        for (int j = 0; j < Inventory.instanceInventory.item.Count; j++)
            Inventory.instanceInventory.item[j] = Inventory.instanceInventory.EmptySlot();
        for (int j=0;j<Inventory.instanceInventory.item.Count;j++)
        {
            string fipath = Application.persistentDataPath + "/" + "invitem" + (j+1).ToString() + ".txt";
            string item = File.ReadAllText(fipath);
            SavedItem it = new SavedItem();
            it = JsonUtility.FromJson<SavedItem>(item);
            Item json = new Item();
            json = LoadingItem(json, it);
            for (int l = 0; l < it.countItem; l++)
            {
                Inventory.instanceInventory.AddItem(json);
            }
        }

        for (int j = 0; j < Inventory.instanceInventory.favourite.favouriteItem.Count; j++)
            Inventory.instanceInventory.favourite.favouriteItem[j] = Inventory.instanceInventory.EmptySlot();
        for (int j = 0; j < Inventory.instanceInventory.favourite.favouriteItem.Count; j++)
        {
            string fipath = Application.persistentDataPath  + "/" + "favitem" + (j + 1).ToString() + ".txt";
            string item = File.ReadAllText(fipath);
            SavedItem it = new SavedItem();
            it = JsonUtility.FromJson<SavedItem>(item);
            Item json = new Item();
            json = LoadingItem(json, it);
            for(int l = 0; l < it.countItem; l++)
            {
                Inventory.instanceInventory.favourite.AddItem(json);
            }
        }

        string wppath = Application.persistentDataPath + "/" + "wpslot";
        string weapon= File.ReadAllText(wppath);
        SavedItem weap = new SavedItem();
        weap = JsonUtility.FromJson<SavedItem>(weapon);
        Item wp = new Item();
        wp = LoadingItem(wp, weap);
        for (int j = 0; j < Inventory.instanceInventory.database.transform.childCount; j++)
        {

            Item item = Inventory.instanceInventory.database.transform.GetChild(j).GetComponent<Item>();

            if (item.id == wp.id)
            {
                var preWeapon = GameObject.Find("HandWeapon").transform.GetChild(0);
                var newWeapon = Instantiate(Inventory.instanceInventory.database.transform.GetChild(j), preWeapon.transform.position, Quaternion.identity);
                newWeapon.transform.rotation = preWeapon.transform.rotation;
                newWeapon.transform.parent = preWeapon.parent;
                Destroy(GameObject.Find("HandWeapon").transform.GetChild(0).gameObject);
                newWeapon.tag = "Player Veapon";
                newWeapon.GetComponent<Item>().Damege = wp.Damege;
                newWeapon.GetComponent<Item>().DamageType =wp.DamageType;
                newWeapon.GetComponent<Item>().elementalDamage = wp.elementalDamage;
                Inventory.instanceInventory.weaponSlot.weapon = newWeapon.GetComponent<Item>();
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
                Inventory.instanceInventory.weaponSlot.DisplayItem();
                break;
            }
        }

        string arpath = Application.persistentDataPath+ "/" + "arslot";
        string armor = File.ReadAllText(arpath);
        SavedItem arm = new SavedItem();
        arm = JsonUtility.FromJson<SavedItem>(armor);
        Item ar = new Item();
        ar = LoadingItem(ar, arm);
        Inventory.instanceInventory.armorSlot.Armor.icon = ar.icon;
        Inventory.instanceInventory.armorSlot.DisplayItem();
        Inventory.instanceInventory.armorSlot.Armor.Armor = ar.Armor;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filepath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        fs.Close();
        Inventory.instanceInventory.goldc = 0;
        Inventory.instanceInventory.ChangeGold(save.gold);
        player.GetComponent<HealthBar>().health = save.Phealth;
        player.GetComponent<HealthBar>().maxhealth = save.MaxPhealth;
        player.GetComponent<HealthBar>().UpdateHealthBar();
        player.GetComponent<ManaBar>().mana = save.Pmana;
        player.GetComponent<ManaBar>().maxmana = save.MaxPmana;
        player.GetComponent<ManaBar>().UpdateManaBar();
        exp.Level = save.curLevel;
        exp.CurrentExp = save.curExp;
        exp.neededExperience = save.curNexp;
        exp.UpdateExperience(0);
        skill.GetComponent<SkillPanel>().coutskill = save.lvlcout;
        skill.GetComponent<SkillPanel>().ChangeSkillCout(0);
        skill.GetComponent<SkillPanel>().AddPossibility();
        int i = 0;
        foreach(var enemy in save.EnemiesData)
        {
            enemyes[i].GetComponent<EnemyAI>().LoadData(enemy);
            i++;
        }
        int d = 0;
        foreach (var skill in save.skilldata)
        {
            skills[d].GetComponent<SkillPointSpendButton>().LoadSkillLevel(skill);
            d++;
        }
        
        player.transform.position = new Vector3(save.Ppos.x, save.Ppos.y, save.Ppos.z);
    }
}

[Serializable]
public class SavedItem
{
    public string nameItem;
    public int id;
    public int countItem;
    public bool isStackable;
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
}
[Serializable]
public class SaveSkills
{
    public int skilllevel;

}
[System.Serializable]
public class Save {
    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;
        public Vec3(float x ,float y,float  z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    [System.Serializable]
    public struct EnemySaveData
    {
        public float Health;
        public Vec3 Position, Direction;

        public EnemySaveData(Vec3 pos, Vec3 dir, float hp)
        {
            Position = pos;
            Direction = dir;
            Health = hp;
        }
    }
    public Vec3 Ppos;
    public int gold;
    public float Phealth;
    public float MaxPhealth;
    public float Pmana;
    public float MaxPmana;
    public float curExp;
    public int curNexp;
    public int curLevel;
    public int lvlcout;
    public List<EnemySaveData> EnemiesData = new List<EnemySaveData>();

    [System.Serializable]
    public struct SkillSaveData
    {
        public int Skilllevel;
        public SkillSaveData(int skill)
        {
            Skilllevel = skill;
        }
    }
    public List<SkillSaveData> skilldata = new List<SkillSaveData>();
    public void SaveSkills(List<GameObject> skillButtons)
    {
        foreach (var go in skillButtons)
        {
            int skill = go.GetComponent<SkillPointSpendButton>().skillcout;
            skilldata.Add(new SkillSaveData(skill));
        }
    }
    public void SaveEnemyes(List<GameObject> enemyes)
    {
        foreach(var go in enemyes)
        {
            Vec3 pos = new Vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            Vec3 dir = new Vec3(go.transform.rotation.x, go.transform.rotation.y, go.transform.rotation.z);
            float hp = go.GetComponent<Enemyhealthbar>().enemyhealth;
            EnemiesData.Add(new EnemySaveData(pos, dir,hp));
        }
    }
}

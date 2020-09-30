using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    private float Damage;
    private Item Weapon;
    public Mastery Mas;
    public Spin spin;
    public Rage rage;
    public Dash dash;
    // Start is called before the first frame update
    void Start()
    {
        FindWeapon();
    }

    public void FindWeapon()
    {
        Weapon = gameObject.transform.GetChild(0).GetComponent<Item>();
    }
    public float FindDamage(string resistance, string vulnerability, int armor)
    {
        Damage = Weapon.Damege + Mas.CheckMastery() * Weapon.Damege + rage.Ragecouter() * Weapon.Damege + spin.CoutDmg() * Weapon.Damege+dash.FindDmg()*Weapon.Damege;

        if (GetComponentInParent<HealthBar>().shiledisactive)
        {
            Damage = Damage + Weapon.Damege * 0.5f;
        }

        if (Weapon.DamageType != null)
        {
            if (resistance != Weapon.DamageType)
            {
                Damage += Weapon.elementalDamage;
            }
            else if (vulnerability == Weapon.DamageType)
            {
                Damage += 2* Weapon.elementalDamage;
            }
        }
        Damage -= armor;
        if (Damage < 1) Damage = 1;
        //Debug.Log("Получившийся урон:  " + Damage);
        return Damage;
    }

}

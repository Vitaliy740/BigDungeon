
using UnityEngine;
public class Control : MonoBehaviour {

    public float speed;
    private float originalspeed;
    public Animator anim;
    public Joystick joystick;
    public Joystick JoySign;
    public Transform _camera;
    public float attackTime;
    [HideInInspector]
    public Vector3 signVector;
    public Vector3 moveVector;
    public SkillPointSpendButton sb;
    public bool attack=false;

    private void Start()
    {
        originalspeed = speed;
        ChangeWeapon();
    }
    public void AddSpeed()
    {
        speed = originalspeed + 0.4f * sb.skillcout;
    }
    public void ChangeWeapon()
    {
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Item>().Damege = Inventory.instanceInventory.weaponSlot.weapon.Damege;
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Item>().DamageType = Inventory.instanceInventory.weaponSlot.weapon.DamageType;
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Item>().elementalDamage= Inventory.instanceInventory.weaponSlot.weapon.elementalDamage;
        GameObject.Find("HandWeapon").GetComponent<DamageCounter>().FindWeapon();
    }
    void FixedUpdate() {

         moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);
         signVector = (Vector3.right * JoySign.Horizontal + Vector3.up * JoySign.Vertical);
        _camera.position= new Vector3(transform.position.x, transform.position.y);
        if (moveVector != Vector3.zero)
        {
            transform.Translate(moveVector * speed * Time.deltaTime, Space.World);
            
        }
        if (signVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, signVector);
            anim.SetTrigger("Attack");
            attack = true;
            GameObject.FindGameObjectWithTag("Player Veapon").GetComponent<BoxCollider2D>().enabled=true;
        }
        else
        {
            attack = false;
            GameObject.FindGameObjectWithTag("Player Veapon").GetComponent<BoxCollider2D>().enabled = false;
        }


    }
}
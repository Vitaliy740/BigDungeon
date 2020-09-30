using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;
    public Button dbutton;
    private bool bpressed = false;
    public int dashcooldown;
    public bool dashing = false;
    private Vector3 dashDir;
    private int dashC;
    private int dashDuration = 25;
    private float dashSpeed=20f;
    private GameObject hitbox;
    public SkillPointSpendButton sb;
    public ManaBar mb;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = transform.GetChild(3).gameObject;
        hitbox.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
    }
    void MakeDash(Vector2 dir)
    {
        if (dashcooldown > 0) { Debug.Log("Невозможно");bpressed = false; return; }
        float pen = 1f;
        //if ((dir.x > 0.5f || dir.x < -0.5f) && (dir.y > 0.5f || dir.y < -0.5f))
        //    pen = 0.8f;
        rb.angularVelocity = 0f;
        dashing = true;
        dashDir = pen * dir;
        dashC = 0;
        hitbox.SetActive(true);
        //dashcooldown = 200;
        mb.mana -= 25f;
        mb.UpdateManaBar();
        bpressed = false;

    }
    public float FindDmg()
    {
        if (sb.skillcout < 1) return 0;
        if (dashing) return ((sb.skillcout * 5f) / 100f);
        return 0;
    }
    void StopDash()
    {
        dashDir = Vector2.zero;
        dashing = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        hitbox.SetActive(false);
    }
    void Update()
    {
        if (dashing) 
        {
            GetComponent<Control>().attack = true;
            return; 
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        } 
        dbutton.onClick.AddListener(delegate { bpressed = true; });
        if (bpressed&& mb.mana>=25)
        {
            if(GetComponent<Control>().signVector!=Vector3.zero)
                MakeDash((transform.GetChild(2).transform.position - transform.position).normalized);
        }
    }
    private void FixedUpdate()
    {
        if (dashcooldown > 0) dashcooldown--;
        if (dashing)
        {
            Debug.Log("Dashing");
            rb.velocity = dashDir * dashSpeed;
            dashC++;
            if (dashC > dashDuration)
            {
                StopDash();
                rb.velocity = Vector2.zero;
            }
        }
    }
}

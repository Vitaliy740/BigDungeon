using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    public Button spinbutton;
    public bool buttonispressed;
    private float spintime=0f;
    private GameObject spinvisualisation;
    public SkillPointSpendButton sb;
    private ManaBar mb;
    // Start is called before the first frame update
    void Start()
    {
        mb = GetComponent<ManaBar>();
        spinvisualisation = transform.GetChild(4).gameObject;
        spinvisualisation.SetActive(false);
    }
    public float CoutDmg()
    {
        int skilllevel = sb.skillcout;
        if (skilllevel > 0)
            return (skilllevel * 10f) / 100f;
        else
            return 0f;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        spinbutton.onClick.AddListener(delegate { if (mb.mana >= 45) { buttonispressed = true; spinvisualisation.SetActive(true); } });
        if (buttonispressed)
        {
            spintime += Time.fixedDeltaTime;
            Quaternion rotationZ = Quaternion.AngleAxis(36, Vector3.forward);
            transform.rotation *= rotationZ;
            spinvisualisation.transform.rotation *= rotationZ;
            GetComponent<Control>().attack = true;
        }
        if (spintime > 0.3f) 
        { 
            buttonispressed = false;
            spinvisualisation.SetActive(false); 
            spintime = 0f;
            mb.mana -= 45;
            mb.UpdateManaBar();
        }
    }
}

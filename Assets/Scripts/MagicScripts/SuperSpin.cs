using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SuperSpin : MonoBehaviour
{
    public Button supspinbutton;
    public bool spinning;
    private GameObject supspinvisualisation;
    public SkillPointSpendButton sb;
    private ManaBar mb;
    // Start is called before the first frame update
    void Start()
    {
        mb = GetComponent<ManaBar>();
        supspinvisualisation = transform.GetChild(4).gameObject;
        supspinvisualisation.SetActive(false);
    }
    public void AddSpinTime()
    {
        mb.AddMana(20);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        supspinbutton.onClick.AddListener(delegate { if (!spinning) { spinning = true; supspinvisualisation.SetActive(true); } else { spinning = false; supspinvisualisation.SetActive(false); }  });
        if (spinning && mb.mana > 0) 
        {
            mb.mana -= 10*Time.fixedDeltaTime;
            mb.UpdateManaBar();
            GetComponent<Control>().attack = true;
            Quaternion rotationZ = Quaternion.AngleAxis(36, Vector3.forward);
            transform.rotation *= rotationZ;
            supspinvisualisation.transform.rotation *= rotationZ;
        }
        if (mb.mana <= 0)
        {
            spinning = false;
            supspinvisualisation.SetActive(false);
            mb.mana = 0;
            mb.UpdateManaBar();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Image manaBar;
    public float mana = 100f;
    public float maxmana = 100f;
    public float originalMana;
    private Vector3 manaScale;
    private Vector3 originalscale;
    public GameObject manabar;
    private Text cMana, mMana;

    // Start is called before the first frame update
    public void MoreMana()
    {
        maxmana += 20;
        originalMana = maxmana;
        mana = maxmana;
        manaScale = originalscale;
        UpdateManaBar();
    }
    public void AddMana(int Value)
    {
        mana += Value;
        if (mana > maxmana) mana = maxmana;
        UpdateManaBar();
    }
    void Start()
    {
        manaBar = manabar.GetComponent<Image>();
        manaScale = manabar.transform.localScale;
        cMana = manabar.transform.parent.GetChild(5).GetComponent<Text>();
        mMana = manabar.transform.parent.GetChild(6).GetComponent<Text>();
        originalscale = manaScale;
        originalMana = maxmana;
        UpdateManaBar();
    }
    public void UpdateManaBar()
    {
        manaScale.x = (mana / originalMana) * originalscale.x;
        manaBar.transform.localScale = manaScale;
        cMana.text = ((int)mana).ToString();
        mMana.text = maxmana.ToString();
    }

}

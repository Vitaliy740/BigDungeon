using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicArea : MonoBehaviour, IPointerDownHandler {
    // Use this for initialization
    public GameObject explosion;
    public Button MButton;
    public Button CoutSkillButton;
    public Button PlaceSkill;
    public int skillID = 0;
    public void Start()
    {
        Debug.Log("sdad");
        MButton.gameObject.SetActive(false);
        CheckSkillPower();
        gameObject.SetActive(false);
    }
    public void CheckSkillPower()
    {
        Debug.Log("Its work");
        if (CoutSkillButton.GetComponent<SkillPointSpendButton>().skillcout < 1) {
            PlaceSkill.interactable = false;
        }
        else PlaceSkill.interactable = true;
    }
    public void MagicStart()
    {
        gameObject.SetActive(true);
        skillID = 0;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
        gameObject.SetActive(false);
        
        GameObject obj = Instantiate(explosion);
        obj.SetActive(true);
        obj.transform.position= Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 1));
    }
}

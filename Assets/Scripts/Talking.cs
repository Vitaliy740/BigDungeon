using UnityEngine;

public class Talking : MonoBehaviour {
    public GameObject Talkingg;

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "Player")
            Talkingg.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.tag == "Player")
            Talkingg.SetActive(false);
    }
}

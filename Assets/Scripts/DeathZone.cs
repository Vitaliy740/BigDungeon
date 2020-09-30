using UnityEngine;

public class DeathZone : MonoBehaviour {

    
    public GameManager d;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            d.ShowLosePanel();

            Destroy(collision.gameObject);
                }
    }
}

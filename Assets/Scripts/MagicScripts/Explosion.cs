using UnityEngine;

public class Explosion : MonoBehaviour {
    private float atime = 0;

    void Update()
    {
        if (gameObject.activeSelf == true)
        {              
            atime = atime + Time.deltaTime;
            if (atime > 2f)
                {
                atime = 0;
                gameObject.SetActive(false);
              
                }
         }   
    }
}

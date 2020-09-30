using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {
    public void play(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
        Time.timeScale = 1f;
            
    }

}

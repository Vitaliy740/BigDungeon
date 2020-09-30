using UnityEngine;
using UnityEngine.UI;
public class Exit : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            exit();
        });
    }
    private void exit()
    {
        Application.Quit();
    }
}


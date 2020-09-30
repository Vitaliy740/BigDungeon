using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public GameObject dpanel;
    public void TriggerDialog()
    {
        dpanel.SetActive(true);
        FindObjectOfType<DialogManager>().StartDialog(dialog);

    }
}

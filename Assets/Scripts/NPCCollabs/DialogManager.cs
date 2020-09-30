using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public Animator animator;
    private Queue<string> sentences;
    public GameObject sellbutton;
    public GameObject savebutton;
    public Text continueb;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("IsOpen",true);
        sellbutton.SetActive(false);
        savebutton.SetActive(false);
        continueb.text = "Продолжить";
        nameText.text = dialog.name;
        sentences.Clear();
        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndOfDialog();
            return;
        }
        if (sentences.Count == 1)
        {
            sellbutton.SetActive(true);
            savebutton.SetActive(true);
            continueb.text = "Уйти";
        }
            
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    void EndOfDialog()
    {
        animator.SetBool("IsOpen", false);
    }
}

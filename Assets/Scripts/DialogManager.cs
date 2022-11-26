using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private bool isTalking = false;

    public Text nameText;
    public Text dialogText;

    public Animator animator;

    private Queue<string> sentences;

    // Créér un unique DialogManager accéssible de partout
    public static DialogManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DialogManager dans la scène");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        isTalking = true;

        animator.SetBool("isOpen", true);

        nameText.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // arrete les autres coroutine de ce script
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            // yield return new WaitForSeconds(0.01f); // ou yield return null; pour skip un frame
            yield return null;
        }
    }

    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
        isTalking = false;
    }

    public bool GetIsTalking()
    {
        return isTalking;
    }
}

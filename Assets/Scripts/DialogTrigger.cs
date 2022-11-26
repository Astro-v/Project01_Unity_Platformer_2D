using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    private bool isInRange = false;

    private Text interactUI;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !DialogManager.instance.GetIsTalking())
        {
            TriggerDialog();
        }
        else if (isInRange && Input.GetKeyDown(KeyCode.E) && DialogManager.instance.GetIsTalking())
        {
            DialogManager.instance.DisplayNextSentence();
        }
        
        if (interactUI.enabled && DialogManager.instance.GetIsTalking())
        {
            interactUI.enabled = false;
        }
        else if (!interactUI.enabled && !DialogManager.instance.GetIsTalking() && isInRange)
        {
            interactUI.enabled = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
            DialogManager.instance.EndDialog();
        }
    }

    public void TriggerDialog()
    {
        DialogManager.instance.StartDialog(dialog);
    }
}

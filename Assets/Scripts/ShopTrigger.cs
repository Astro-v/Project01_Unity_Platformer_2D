using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    private bool isInRange = false;

    private Text interactUI;

    public string npcName;
    public Item[] itemsToSell;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !ShopManager.instance.GetIsShopping())
        {
            ShopManager.instance.OpenShop(itemsToSell, npcName);
        }
        else if (isInRange && Input.GetKeyDown(KeyCode.E) && ShopManager.instance.GetIsShopping())
        {
            ShopManager.instance.CloseShop();
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
            ShopManager.instance.CloseShop();
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private bool isShopping = false;

    public Text npcNameText;

    public Animator animator;

    public GameObject sellButtonPrefab;

    public Transform sellButtonsParent;

    // Cr��r un unique ShopManager acc�ssible de partout
    public static ShopManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopManager dans la sc�ne");
            return;
        }

        instance = this;
    }

    public void CloseShop()
    {
        animator.SetBool("isOpen", false);
        isShopping = false;
    }

    public void OpenShop(Item[] items, string npcName)
    {
        npcNameText.text = npcName;
        animator.SetBool("isOpen", true);
        UpdateItemsToSell(items);
        isShopping = true;
    }

    private void UpdateItemsToSell(Item[] items)
    {
        // Supprime les potentiels boutons pr�sent dans le parent
        for (int i = 0; i < sellButtonsParent.childCount; i++)
        {
            Destroy(sellButtonsParent.GetChild(i).gameObject);
        }

        // Instancie un bouton pour chaque item � vendre et le configure
        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(sellButtonPrefab, sellButtonsParent);
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].image;
            buttonScript.itemPrice.text = items[i].price.ToString();

            buttonScript.item = items[i];

            // Ajout dynamique du "On Click"
            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); });
        }
    }

    public bool GetIsShopping()
    {
        return isShopping;
    }
}

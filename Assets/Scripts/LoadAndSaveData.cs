using UnityEngine;
using System.Linq; // Pour le Select et Single / permet d'utiliser des commandes SQR request linq

// Normalement utilis� les JSON et/ou les XML et/ou fichier.txt + cryptage (voir video unity fr) et/ou utilis� des assets pr�vus pour ca 
// Ici les player pref sont utile pour enregistr� les pr�f�rences utilisateur mais comme les donn�s sauvegard� sont l�gere on utilise ca.

public class LoadAndSaveData : MonoBehaviour
{
    // Cr��r un unique LoadAndSaveData acc�ssible de partout
    public static LoadAndSaveData instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Start()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();

        // int currentHealth = PlayerPrefs.GetInt("playerHealth", PlayerHealth.instance.maxHealth);
        // PlayerHealth.instance.currentHealth = currentHealth;
        // PlayerHealth.instance.healthBar.SetHealth(currentHealth);

        // Chargement
        string[] itemsSave = PlayerPrefs.GetString("inventoryItems", "").Split(',');

        for (int i = 0; i < itemsSave.Length; i++)
        {
            if (itemsSave[i] != "")
            {
                // Ajoute l'item � l'inventaire
                int id = int.Parse(itemsSave[i]);
                Item currentItem = ItemsDatabase.instance.allItems.Single(x => x.id == id);
                Inventory.instance.content.Add(currentItem);
            }
        }

        Inventory.instance.UpdateInventoryUI();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }
        // PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.currentHealth);

        // Sauvegarde
        string itemsInInventory = string.Join(",", Inventory.instance.content.Select(x => x.id));
        // Debug.Log("Les items sont : " + itemsInInventory);
        PlayerPrefs.SetString("inventoryItems", itemsInInventory);

    }
}

using UnityEngine;
using System.Linq; // Pour le Select et Single / permet d'utiliser des commandes SQR request linq

// Normalement utilisé les JSON et/ou les XML et/ou fichier.txt + cryptage (voir video unity fr) et/ou utilisé des assets prévus pour ca 
// Ici les player pref sont utile pour enregistré les préférences utilisateur mais comme les donnés sauvegardé sont légere on utilise ca.

public class LoadAndSaveData : MonoBehaviour
{
    // Créér un unique LoadAndSaveData accéssible de partout
    public static LoadAndSaveData instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
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
                // Ajoute l'item à l'inventaire
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

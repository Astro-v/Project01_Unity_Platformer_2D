using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public Item[] allItems;

    // Créér un unique ItemsDatabase accéssible de partout
    public static ItemsDatabase instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ItemsDatabase dans la scène");
            return;
        }

        instance = this;
    }
}

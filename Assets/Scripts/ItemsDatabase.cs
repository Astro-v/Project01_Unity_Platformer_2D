using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public Item[] allItems;

    // Cr��r un unique ItemsDatabase acc�ssible de partout
    public static ItemsDatabase instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ItemsDatabase dans la sc�ne");
            return;
        }

        instance = this;
    }
}

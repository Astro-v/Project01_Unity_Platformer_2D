using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int coinsPickedUpInThisSceneCount;
    public Vector3 respawnPoint;
    public int levelToUnlock;

    // Créér un unique CurrentSceneManager accéssible de partout
    public static CurrentSceneManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scène");
            return;
        }

        instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}

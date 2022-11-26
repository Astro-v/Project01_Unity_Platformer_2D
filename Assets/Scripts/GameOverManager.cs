using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    // Créér un unique GameOverManager accéssible de partout
    public static GameOverManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        // Recommencer le niveau
        // Recharge la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Replace le joueur au spawn
        // Réactive les mouvement du joueur et lui rendre sa vie
        PlayerHealth.instance.Respawn();
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        // Retour au menu principal
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        // Fermer le jeu
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false; // static pour y avoir acces de partout
    private bool settingsIsOppened = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    private void Start()
    {
        pauseMenuUI.SetActive(gameIsPaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused && !settingsIsOppened)
            {
                Resume();
            }
            else if (gameIsPaused && settingsIsOppened)
            {
                settingsWindow.SetActive(false);
                settingsIsOppened = false;
            }
            else
            {
                Paused();
            }
        }
    }

    private void Paused()
    {
        // stoper les mouvement du joueur
        PlayerMovement.instance.enabled = false;
        // activer notre menu pause / l'afficher
        pauseMenuUI.SetActive(true);
        settingsWindow.SetActive(false);
        // arrêter le temps
        Time.timeScale = 0;
        // changer le statut du jeu
        gameIsPaused = true;
        settingsIsOppened = false;

    }

    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        settingsWindow.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        settingsIsOppened = false;

    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
        settingsIsOppened = true;
    }

    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
        settingsIsOppened = false;
    }
}

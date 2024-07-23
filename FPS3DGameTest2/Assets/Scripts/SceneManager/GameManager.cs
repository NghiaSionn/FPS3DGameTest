using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string playAgainScene;
    public GameObject pauseMenuUI;
    public GameObject optionPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (optionPanel.activeSelf)
                {
                    ShowPauseMenu();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // Tiếp tục game
        AudioListener.pause = false; // Tiếp tục âm thanh
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        optionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; // Dừng game
        AudioListener.pause = true; // Dừng âm thanh
        isPaused = true;
    }

    public void ShowPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ShowOptionPanel()
    {
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(playAgainScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

   
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused=false;

     
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
                
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartingScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

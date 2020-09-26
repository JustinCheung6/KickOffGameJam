using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneScript : MonoBehaviour
{
    private bool gamePaused = false;
    [SerializeField] private GameObject pauseMenu = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
        }

        if (gamePaused)
        {
            pauseGame();
        }
        else
        {
            resumeGame();
        }
        
    }

    public void quitGame()
    {
        Application.Quit();
    }
    public void loadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
        gamePaused = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        if(pauseMenu != null)
            pauseMenu.SetActive(false);
        gamePaused = false;
    }

}

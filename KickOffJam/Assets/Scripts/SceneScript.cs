using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneScript : MonoBehaviour
{
    private bool gamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Escape"))
        {
            gamePaused = true;
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
        gameObject.SetActive(true);
        gamePaused = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        gamePaused = false;
    }

}

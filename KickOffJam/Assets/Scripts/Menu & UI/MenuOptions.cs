using UnityEngine;
using UnityEngine.SceneManagement;

//Different methods used for Scene transitions
public class MenuOptions : MonoBehaviour
{
    protected void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    protected void LoadScene(int level)
    {
        Debug.Log($"Transitioning to Scene: {level}");
        SceneManager.LoadScene(level);
    }

    #region Method used Exclusively by Unity Buttons
    public void QuitGame_Btn()
    {
        QuitGame();
    }
    public void GoToGameScene_Btn()
    {
        LoadScene(1);
    }
    public void GoToTitleScene_Btn()
    {
        LoadScene(0);
    }
    #endregion
}

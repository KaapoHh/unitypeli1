using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void QuitGame()
    {
        Debug.Log("Peli suljetaan...");
        Application.Quit();
    }
}
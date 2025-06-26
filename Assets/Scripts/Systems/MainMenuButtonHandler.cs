using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public static void QuitDesktop()
    {
        Application.Quit();
    }
}

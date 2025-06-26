using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonHandler : MonoBehaviour
{
    private InputHandler _inputHandler;

    void Start()
    {
        _inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
    }
    public void Resume()
    {
        _inputHandler.DisableIsInMenu();
    }

    public void Option()
    {

    }

    public void QuitMenu()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitDesktop()
    {
        DOTween.KillAll();
        Application.Quit();
    }
}

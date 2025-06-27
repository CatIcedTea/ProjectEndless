using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonHandler : MonoBehaviour
{
    private InputHandler _inputHandler;
    private GameObject _optionsMenu;

    private AudioManager _audioManager;


    void Awake()
    {
        _optionsMenu = GameObject.FindGameObjectWithTag("Options");
    }

    void Start()
    {
        _inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Resume()
    {
        _audioManager.PlayAudio(_audioManager.menuCancel);
        _inputHandler.DisableIsInMenu();
    }

    public void Option()
    {
        _audioManager.PlayAudio(_audioManager.menuAccept);
        _optionsMenu.SetActive(true);
    }

    public void QuitMenu()
    {
        _audioManager.PlayAudio(_audioManager.menuCancel);
        DOTween.KillAll();
        StopAllCoroutines();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitDesktop()
    {
        _audioManager.PlayAudio(_audioManager.menuCancel);
        DOTween.KillAll();
        StopAllCoroutines();
        Application.Quit();
    }
}

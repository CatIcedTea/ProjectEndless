using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void StartGame()
    {
        _audioManager.PlayAudio(_audioManager.menuAccept);
        SceneManager.LoadScene("GameLevel");
    }

    public void QuitDesktop()
    {
        _audioManager.PlayAudio(_audioManager.menuCancel);
        Application.Quit();
    }
}

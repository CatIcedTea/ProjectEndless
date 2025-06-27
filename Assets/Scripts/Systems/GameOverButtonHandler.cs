using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    private AudioManager _audioManager;
    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void Retry()
    {
        _audioManager.PlayAudio(_audioManager.menuAccept);
        DOTween.KillAll();
        StopAllCoroutines();
        SceneManager.LoadScene("GameLevel");
    }

    public void QuitToMenu()
    {
        _audioManager.PlayAudio(_audioManager.menuCancel);
        DOTween.KillAll();
        StopAllCoroutines();
        SceneManager.LoadScene("MainMenu");
    }
}

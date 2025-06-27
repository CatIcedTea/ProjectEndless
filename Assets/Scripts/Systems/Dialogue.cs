using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _typewriterLength;
    [SerializeField] private float _typewriterPunctuationLength;
    [SerializeField] private float _nextLineTimer;

    private DialogueText _dialogueText;
    private RectTransform _boxRect;

    private bool flipRotate = false;

    private string[] _currentText;
    private int _lineIndex;
    private int _characterCounter;
    private bool _isInDialogue;
    private bool _isInWinState = false;

    private readonly char[] _punctuationArr = new char[] { '.', ',', '!', '?' };

    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _dialogueText = GetComponent<DialogueText>();
        _boxRect = GetComponent<RectTransform>();
        _text.maxVisibleCharacters = 0;
        _characterCounter = 0;

        _boxRect.transform.localScale = Vector3.zero;

        _currentText = _dialogueText.GetText(0);
        _text.text = _currentText[0];
        WriteText();
    }

    public void WriteText()
    {
        _isInDialogue = true;
        _boxRect.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
        _audioManager.PlayAudio(_audioManager.voiceId);
        IdleWobble();
        StartCoroutine(WriteNextCharacter());
    }

    public void WriteText(string[] textArr)
    {
        _boxRect.transform.localScale = Vector3.zero;
        StopAllCoroutines();

        _lineIndex = 0;
        _currentText = textArr;
        _text.text = _currentText[0];
        _text.maxVisibleCharacters = 0;
        _characterCounter = 0;

        WriteText();
    }

    private IEnumerator WriteNextCharacter()
    {
        _text.maxVisibleCharacters++;
        _characterCounter++;
        _audioManager.PlayAudio(_audioManager.menuHover);

        if (_text.text[_characterCounter - 1].Equals('<'))
        {
            while (!_text.text[_characterCounter - 1].Equals('>'))
            {
                _characterCounter++;
            }
        }

        if (_punctuationArr.Contains(_text.text[_characterCounter - 1]))
        {
            yield return new WaitForSeconds(_typewriterPunctuationLength);
        }
        else
            yield return new WaitForSeconds(_typewriterLength);

        if (_characterCounter < _text.text.Length)
            StartCoroutine(WriteNextCharacter());
        else
        {
            yield return new WaitForSeconds(_nextLineTimer);

            if (_lineIndex < _currentText.Length - 1)
            {
                _text.maxVisibleCharacters = 0;
                _characterCounter = 0;
                _lineIndex++;
                _text.text = _currentText[_lineIndex];
                StartCoroutine(WriteNextCharacter());
                _boxRect.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => _boxRect.transform.DOScale(Vector3.one, 0.25f));
                _audioManager.PlayAudio(_audioManager.voiceId);
            }
            else
            {
                if (_isInWinState)
                {
                    _boxRect.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => SendToMainMenu());
                }
                else
                    _boxRect.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => _isInDialogue = false);
            }
        }
    }

    private void IdleWobble()
    {
        if (!_isInDialogue)
            return;

        if (!flipRotate)
        {
            flipRotate = true;
            _boxRect.transform.DORotate(new Vector3(0, 0, 3f), 2f).OnComplete(() => IdleWobble());
        }
        else
        {
            flipRotate = false;
            _boxRect.transform.DORotate(new Vector3(0, 0, -3f), 2f).OnComplete(() => IdleWobble());
        }
    }

    public void EnableWinState()
    {
        _isInWinState = true;
    }

    private void SendToMainMenu()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("MainMenu");
    }
}

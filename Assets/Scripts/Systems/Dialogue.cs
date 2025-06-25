using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

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

    private readonly char[] _punctuationArr = new char[] { '.', ',', '!', '?' };

    void Start()
    {
        _dialogueText = GetComponent<DialogueText>();
        _boxRect = GetComponent<RectTransform>();
        _text.maxVisibleCharacters = 0;

        _boxRect.transform.localScale = Vector3.zero;

        _currentText = _dialogueText.GetText(1);
        _text.text = _currentText[0];
        WriteText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void WriteText()
    {
        _boxRect.transform.DOScale(new Vector3(1, 1, 1), 0.25f);
        IdleWobble();
        StartCoroutine(WriteNextCharacter());
    }

    private IEnumerator WriteNextCharacter()
    {
        _text.maxVisibleCharacters++;

        if (_punctuationArr.Contains(_text.text[_text.maxVisibleCharacters - 1]))
        {
            yield return new WaitForSeconds(_typewriterPunctuationLength);
        }
        else
            yield return new WaitForSeconds(_typewriterLength);

        if (_text.maxVisibleCharacters < _text.text.Length)
            StartCoroutine(WriteNextCharacter());
        else
        {
            yield return new WaitForSeconds(_nextLineTimer);

            if (_lineIndex < _currentText.Length - 1)
            {
                _text.maxVisibleCharacters = 0;
                _lineIndex++;
                _text.text = _currentText[_lineIndex];
                StartCoroutine(WriteNextCharacter());
                _boxRect.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => _boxRect.transform.DOScale(Vector3.one, 0.25f));
            }
            else
                _boxRect.transform.DOScale(Vector3.zero, 0.25f);
        }
    }

    private void IdleWobble()
    {
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
}

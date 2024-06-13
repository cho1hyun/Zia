using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    public TMP_Text _text;
    public int _textId;

    void Awake()
    {
        StartCoroutine(GameManagerWait());
    }

    public void TextChange()
    {
        if (_textId != 0)
            _text.text = string.Format(TableManager.Instance.GetLocalizeText(_textId));
    }

    IEnumerator GameManagerWait()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        if (GameManager.Instance.load)
        {
            if (ReferenceEquals(_text, null))
                _text = GetComponent<TMP_Text>();

            TextChange();

            GameManager.Instance.languageChange += TextChange;
        }
    }
}

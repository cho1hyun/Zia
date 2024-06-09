using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    public TMP_Text _text;
    public int _textId;

    private void Awake()
    {
        if (ReferenceEquals(_text, null))
            _text = GetComponent<TMP_Text>();

        if (_textId != 0)
        {
            _text.text = string.Format(TableManager.Instance.GetLocalizeText(_textId));
        }
    }
}
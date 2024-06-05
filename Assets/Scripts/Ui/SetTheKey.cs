using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTheKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onSelect.AddListener(delegate { Select(); });
    }

    public void Select()
    {
        UiManager.Instance.setting.nowKeySet = GetComponent<TMP_InputField>();

        UiManager.Instance.setting.SetKey(GetComponent<TMP_InputField>().text);
    }
}

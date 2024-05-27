using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputThekey : MonoBehaviour
{
    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        if (key == KeyCode.None)
        {
            key = (KeyCode)Enum.Parse(typeof(KeyCode), GetComponent<Image>().sprite.name);
            GetComponent<Button>().onClick.AddListener(Click);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            GetComponent<Button>().onClick.Invoke();
        }
        if (Input.GetKeyUp(key))
        {
            GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    public void Click()
    {
        if (UiManager.instance.setting.nowKeySet != null)
        {
            for (int i = 0; i < UiManager.instance.setting.keyInput.Count; i++)
            {
                if (UiManager.instance.setting.keyInput[i] == UiManager.instance.setting.nowKeySet)
                {
                    if (i == 0)
                    {
                        UiManager.instance.menuKey = key;
                    }

                    if (i >= 1 && i <= 6)
                    {
                        UiManager.instance.keys[i - 1] = key;
                        if (Ingame.Instance != null) Ingame.Instance.SkillGroup.GetChild(i - 1).GetComponent<Skill>().key = key;
                    }

                    if (i >= 7 && i <= 8)
                    {
                        UiManager.instance.keys[i - 1] = key;
                        if (Ingame.Instance != null) Ingame.Instance.Skills.GetChild(i - 7).GetComponent<Skill>().key = key;
                    }
                }
            }

            UiManager.instance.setting.nowKeySet.text = KeyString(key.ToString());
            EventSystem.current.SetSelectedGameObject(gameObject);
            UiManager.instance.setting.Duplication(key.ToString());

            if (UiManager.instance.setting.nowKey != null)
            {
                UiManager.instance.setting.nowKey.text = KeyString(key.ToString());
                UiManager.instance.setting.nowKey = null;
            }
        }
    }

    public string KeyString(string key)
    {
        if (key.Contains("Alpha"))
        {
            return key.Replace("Alpha", string.Empty);
        }

        switch (key)
        {
            case "Escape":
                return key = "Esc";
            default:
                break;
        }

        return key;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class Setting : MonoBehaviour
{
    public GameObject SettingUI;

    public Transform tabButton;
    public Transform tabUI;
    public Transform tabShow;

    public Transform keys;
    public Transform Inputs;

    public List<GameObject> ModePc;
    public List<GameObject> ModeMobile;

    public bool ShowKey;

    public TMP_Text nowKey;
    public TMP_InputField nowKeySet;

    void Start()
    {
        ShowKeyObj(ShowKey);
    }

    public void ShowSettong(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetSetting(bool active)
    {
        SettingUI.SetActive(active);
        ShowSettong(active && transform.parent.GetChild(1).gameObject.activeSelf && !transform.parent.GetChild(2).gameObject.activeSelf);
    }

    public void ShowUI(int active)
    {
        for (int i = 0; i < tabUI.childCount; i++)
        {
            tabButton.GetChild(i).GetComponent<Image>().color = i == active ? Color.white : Color.black;
            tabButton.GetChild(i).GetChild(0).GetComponent<TMP_Text>().color = i == active ? Color.black : Color.white;
            tabUI.GetChild(i).gameObject.SetActive(i == active);
            tabShow.GetChild(i).GetChild(0).gameObject.SetActive(i == active);
        }
    }

    public void Duplication(KeyCode key)
    {
        for (int i = 0; i < Inputs.childCount; i++)
        {
            TMP_InputField InputField = Inputs.GetChild(i).GetChild(1).GetComponent<TMP_InputField>();

            if (InputField.text != string.Empty && InputField.text == KeyString(key.ToString()) && InputField != nowKeySet)
            {
                InputField.text = string.Empty;

                if (i == 0)
                {
                    UiManager.Instance.menuKey = KeyCode.None;
                }
                else
                {
                    UiManager.Instance.keys[i - 1] = KeyCode.None;
                }
            }
            else
            {
                InputField.text = InputField.text;
            }
        }

        nowKeySet = null;
    }

    public void BottonClick(int button)
    {
        Transform parent = EventSystem.current.currentSelectedGameObject.transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetComponent<Image>().color = button == i ? Color.white : Color.black;
            parent.GetChild(i).GetChild(0).GetComponent<Text>().color = button == i ? Color.black : Color.white;
        }
    }

    public void Controll(int mode)
    {
        UiManager.Instance.mode = (Mode)mode;

        for (int i = 0; i < ModePc.Count; i++)
        {
            ModePc[i].SetActive(UiManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < ModeMobile.Count; i++)
        {
            ModeMobile[i].SetActive(UiManager.Instance.mode == Mode.Mobile);
        }

        for (int i = 0; i < Ingame.Instance.ModePc.Count; i++)
        {
            Ingame.Instance.ModePc[i].SetActive(UiManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < Ingame.Instance.ModeMobile.Count; i++)
        {
            Ingame.Instance.ModeMobile[i].SetActive(UiManager.Instance.mode == Mode.Mobile);
        }

        ShowKeyObj(ShowKey);
    }

    public void ShowCooltime(bool Show)
    {
        for (int i = 0; i < Ingame.Instance.SkillGroup.childCount; i++)
        {
            Ingame.Instance.SkillGroup.GetChild(i).GetComponent<Skill>().cooltimeText.gameObject.SetActive(Show);
        }

        for (int i = 0; i < Ingame.Instance.Skills.childCount; i++)
        {
            Ingame.Instance.Skills.GetChild(i).GetComponent<Skill>().cooltimeText.gameObject.SetActive(Show);
        }
    }

    public void ShowKeyObj(bool Show)
    {
        if (Ingame.Instance == null)
            return;

        ShowKey = Show;

        for (int i = 0; i < Ingame.Instance.Key.Count; i++)
        {
            Ingame.Instance.Key[i].SetActive(Show && UiManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < Ingame.Instance.SkillGroup.childCount; i++)
        {
            Ingame.Instance.SkillGroup.GetChild(i).GetComponent<Skill>().keyObj.SetActive(Show && UiManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < Ingame.Instance.Skills.childCount; i++)
        {
            Ingame.Instance.Skills.GetChild(i).GetComponent<Skill>().keyObj.SetActive(Show && UiManager.Instance.mode == Mode.PC);
        }
    }

    public TMP_Text SetKey(string key)
    {
        for (int i = 0; i < Ingame.Instance.Key.Count; i++)
        {
            if (Ingame.Instance.Key[i].GetComponent<TMP_Text>() != null && Ingame.Instance.Key[i].GetComponent<TMP_Text>().text == key)
            {
                return nowKey = Ingame.Instance.Key[i].GetComponent<TMP_Text>();
            }
        }

        for (int i = 0; i < Ingame.Instance.SkillGroup.childCount; i++)
        {
            if (Ingame.Instance.SkillGroup.GetChild(i).GetComponent<Skill>().keyText.text == key)
            {
                return nowKey = Ingame.Instance.SkillGroup.GetChild(i).GetComponent<Skill>().keyText;
            }
        }

        for (int i = 0; i < Ingame.Instance.Skills.childCount; i++)
        {
            if (Ingame.Instance.Skills.GetChild(i).GetComponent<Skill>().keyText.text == key)
            {
                return nowKey = Ingame.Instance.Skills.GetChild(i).GetComponent<Skill>().keyText;
            }
        }

        return null;
    }

    public void SetKeysSprite()
    {
        for (int i = 0; i < keys.childCount; i++)
        {
            for (int j = 0; j < keys.GetChild(i).childCount; j++)
            {
                if (keys.GetChild(i).GetChild(j).GetComponent<InputThekey>() != null)
                {
                    keys.GetChild(i).GetChild(j).GetComponent<InputThekey>().SetKey();
                }
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
                return key;
        }
    }
}

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

    public Transform keyInputs;
    public List<TMP_InputField> keyInput;

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

    public void Duplication(string key)
    {
        for (int i = 0; i < keyInputs.childCount; i++)
        {
            TMP_InputField InputField = keyInputs.GetChild(i).GetChild(1).GetComponent<TMP_InputField>();

            InputField.text = InputField.text == key && InputField != nowKeySet ? string.Empty : InputField.text;
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
        UiManager.instance.mode = (Mode)mode;

        for (int i = 0; i < ModePc.Count; i++)
        {
            ModePc[i].SetActive(UiManager.instance.mode == Mode.PC);
        }

        for (int i = 0; i < ModeMobile.Count; i++)
        {
            ModeMobile[i].SetActive(UiManager.instance.mode == Mode.Mobile);
        }

        for (int i = 0; i < Ingame.Instance.ModePc.Count; i++)
        {
            Ingame.Instance.ModePc[i].SetActive(UiManager.instance.mode == Mode.PC);
        }

        for (int i = 0; i < Ingame.Instance.ModeMobile.Count; i++)
        {
            Ingame.Instance.ModeMobile[i].SetActive(UiManager.instance.mode == Mode.Mobile);
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
        ShowKey = Show;

        for (int i = 0; i < Ingame.Instance.Key.Count; i++)
        {
            Ingame.Instance.Key[i].SetActive(Show && UiManager.instance.mode == Mode.PC);
        }

        for (int i = 0; i < Ingame.Instance.SkillGroup.childCount; i++)
        {
            Ingame.Instance.SkillGroup.GetChild(i).GetComponent<Skill>().keyObj.SetActive(Show && UiManager.instance.mode == Mode.PC);
        }

        for (int i = 0; i < Ingame.Instance.Skills.childCount; i++)
        {
            Ingame.Instance.Skills.GetChild(i).GetComponent<Skill>().keyObj.SetActive(Show && UiManager.instance.mode == Mode.PC);
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
}

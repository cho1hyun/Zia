using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

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
                    GameManager.Instance.menuKey = KeyCode.None;

                else
                    GameManager.Instance.keys[i - 1] = KeyCode.None;
            }

            else
                InputField.text = InputField.text;
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
        GameManager.Instance.mode = (Mode)mode;

        for (int i = 0; i < ModePc.Count; i++)
        {
            ModePc[i].SetActive(GameManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < ModeMobile.Count; i++)
        {
            ModeMobile[i].SetActive(GameManager.Instance.mode == Mode.Mobile);
        }

        for (int i = 0; i < UiManager.Instance.ingame.ModePc.Count; i++)
        {
            UiManager.Instance.ingame.ModePc[i].SetActive(GameManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < UiManager.Instance.ingame.ModeMobile.Count; i++)
        {
            UiManager.Instance.ingame.ModeMobile[i].SetActive(GameManager.Instance.mode == Mode.Mobile);
        }

        ShowKeyObj(ShowKey);
    }

    public void ShowCooltime(bool Show)
    {
        for (int i = 0; i < UiManager.Instance.ingame.SkillGroup.childCount; i++)
        {
            UiManager.Instance.ingame.SkillGroup.GetChild(i).GetComponent<Skill>().cooltimeText.gameObject.SetActive(Show);
        }

        for (int i = 0; i < UiManager.Instance.ingame.Skills.childCount; i++)
        {
            UiManager.Instance.ingame.Skills.GetChild(i).GetComponent<Skill>().cooltimeText.gameObject.SetActive(Show);
        }
    }

    public void ShowKeyObj(bool Show)
    {
        ShowKey = Show;

        for (int i = 0; i < UiManager.Instance.ingame.Key.Count; i++)
        {
            UiManager.Instance.ingame.Key[i].SetActive(Show && GameManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < UiManager.Instance.ingame.SkillGroup.childCount; i++)
        {
            UiManager.Instance.ingame.SkillGroup.GetChild(i).GetComponent<Skill>().keyObj.SetActive(Show && GameManager.Instance.mode == Mode.PC);
        }

        for (int i = 0; i < UiManager.Instance.ingame.Skills.childCount; i++)
        {
            UiManager.Instance.ingame.Skills.GetChild(i).GetComponent<Skill>().keyObj.SetActive(Show && GameManager.Instance.mode == Mode.PC);
        }
    }

    public TMP_Text SetKey(string key)
    {
        for (int i = 0; i < UiManager.Instance.ingame.Key.Count; i++)
        {
            if (UiManager.Instance.ingame.Key[i].GetComponent<TMP_Text>() != null && UiManager.Instance.ingame.Key[i].GetComponent<TMP_Text>().text == key)
                return nowKey = UiManager.Instance.ingame.Key[i].GetComponent<TMP_Text>();
        }

        for (int i = 0; i < UiManager.Instance.ingame.SkillGroup.childCount; i++)
        {
            if (UiManager.Instance.ingame.SkillGroup.GetChild(i).GetComponent<Skill>().keyText.text == key)
                return nowKey = UiManager.Instance.ingame.SkillGroup.GetChild(i).GetComponent<Skill>().keyText;
        }

        for (int i = 0; i < UiManager.Instance.ingame.Skills.childCount; i++)
        {
            if (UiManager.Instance.ingame.Skills.GetChild(i).GetComponent<Skill>().keyText.text == key)
                return nowKey = UiManager.Instance.ingame.Skills.GetChild(i).GetComponent<Skill>().keyText;
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
                    keys.GetChild(i).GetChild(j).GetComponent<InputThekey>().SetKey();
            }
        }
    }
    public string KeyString(string key)
    {
        if (key.Contains("Alpha"))
            return key.Replace("Alpha", string.Empty);

        switch (key)
        {
            case "Escape":
                return "Esc";
            default:
                return key;
        }
    }

    public List<Slider> sliders;
    public List<bool> onNoff;
    public List<bool> onNoff2;
    public List<float> vol;
    public List<Transform> soundbtn;
    public List<TMP_Text> soundText;

    public void SoundBtn(int t)
    {
        onNoff2[t] = true;
        onNoff[t] = !onNoff[t];

        sliders[t].value = onNoff[t] ? vol[t] : 0;

        soundbtn[t].GetChild(3).gameObject.SetActive(!onNoff[t]);

        soundText[t].text = string.Format("{0}%", onNoff[t] ? ((int)(vol[t] * 100)).ToString() : 0);

        soundbtn[t].GetChild(0).gameObject.SetActive(onNoff[t] && sliders[t].value >= 0.3f);
        soundbtn[t].GetChild(1).gameObject.SetActive(onNoff[t] && sliders[t].value >= 0.6f);
        soundbtn[t].GetChild(2).gameObject.SetActive(onNoff[t] && sliders[t].value >= 1);

        SLIDER_Volume(t);

        onNoff2[t] = false;
    }

    public void SLIDER_Volume(int t)
    {
        if (onNoff2[t] == false)
        {
            onNoff[t] = true;

            if (onNoff[t])
                vol[t] = sliders[t].value;

            soundbtn[t].GetChild(0).gameObject.SetActive(onNoff[t] && sliders[t].value >= 0.3f);
            soundbtn[t].GetChild(1).gameObject.SetActive(onNoff[t] && sliders[t].value >= 0.6f);
            soundbtn[t].GetChild(2).gameObject.SetActive(onNoff[t] && sliders[t].value >= 1);
            soundbtn[t].GetChild(3).gameObject.SetActive(!onNoff[t]);

            soundText[t].text = string.Format("{0}%", ((int)(vol[t] * 100)).ToString());

            switch (t)
            {
                case 0:
                    GameManager.Instance.gameObject.GetComponent<SoundManager>().SetMasterVolume(sliders[t].value);
                    GameManager.Instance.gameObject.GetComponent<SoundManager>().SetMasterVolume(sliders[t].value);
                    break;
                case 1:
                    GameManager.Instance.gameObject.GetComponent<SoundManager>().SetBgmVolume(sliders[t].value);
                    break;
                case 2:
                    GameManager.Instance.gameObject.GetComponent<SoundManager>().SetEffectVolume(sliders[t].value);
                    break;
                default:
                    break;
            }
        }
    }

    public void GoLobby()
    {
        UiManager.Instance.Action(1);
        SceneManager.LoadScene(1);
    }

    public void Gotuto()
    {
        UiManager.Instance.pade.tuto = true;
        UiManager.Instance.Story.GetComponent<CanvasGroup>().alpha = 0;
        UiManager.Instance.Action(2);
        SceneManager.LoadScene(2);
    }
}

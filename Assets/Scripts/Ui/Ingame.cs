using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ingame : MonoBehaviour
{
    public Joystick Joystick;
    public Transform SkillGroup;
    public Transform Skills;
    public Tiktok Tiktok;
    public Transform Quest;

    public Boss Boss;
    public CharacterController Character;

    public List<GameObject> ModePc;
    public List<GameObject> ModeMobile;
    public List<GameObject> Key;

    public GameObject characterObj;

    public Transform Over;

    public void setDungeon()
    {
        Character = Instantiate(characterObj).GetComponent<CharacterController>();
        Character.SetCharacter();

        SetViewMode(GameManager.Instance.mode);

        Tiktok.TimeSet();

        SetKey();


        for (int i = 0; i < Quest.childCount; i++)
        {
            QuestTable quest = TableManager.Instance.GetFirstQuest();
            Quest.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text= string.Format(TableManager.Instance.GetLocalizeText(quest.des));
            Quest.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = string.Format("[{0}/{1}]", 0, quest.accomplishValue);
        }
    }

    void SetKey()
    {
        int a = 4;

        for (int i = 0; i < SkillGroup.childCount; i++)
        {
            SkillGroup.GetChild(i).GetComponent<Skill>().key = GameManager.Instance.keys[a];
            a++;
        }

        for (int i = 0; i < Skills.childCount; i++)
        {
            Skills.GetChild(i).GetComponent<Skill>().key = GameManager.Instance.keys[a];
            a++;
        }
    }

    public void WaitBtn()
    {
        UiManager.Instance.setting.ShowSettong(true);
    }

    public void SetViewMode(Mode mode)
    {
        for (int i = 0; i < ModePc.Count; i++)
            ModePc[i].SetActive(mode == Mode.PC);

        for (int i = 0; i < ModeMobile.Count; i++)
            ModeMobile[i].SetActive(mode == Mode.Mobile);

        for (int i = 0; i < Key.Count; i++)
            Key[i].SetActive(mode == Mode.PC && UiManager.Instance.setting.ShowKey);
    }

    public void GameOver(bool win)
    {
        if (Over != null && !Over.gameObject.activeSelf)
        {
            if (win)
                GameManager.Instance.userData.clearStage = GameManager.Instance.userData.lastStage;

            Over.gameObject.SetActive(true);
            Over.GetChild(0).gameObject.SetActive(!win);
            Over.GetChild(1).gameObject.SetActive(win);
        }
    }

    public void Lobby()
    {
        UiManager.Instance.setting.GoLobby();
    }

    public void OffDungoen()
    {
        if (Over != null)
            Over.gameObject.SetActive(false);
    }
}

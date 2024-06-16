using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame : MonoBehaviour
{
    public Joystick Joystick;
    public Transform SkillGroup;
    public Transform Skills;
    public Tiktok Tiktok;

    public Boss Boss;
    public CharacterController Character;

    public List<GameObject> ModePc;
    public List<GameObject> ModeMobile;
    public List<GameObject> Key;

    public GameObject characterObj;

    void Update()
    {
        Time.timeScale = UiManager.Instance.setting.gameObject.activeSelf ? 0 : 1;
    }

    public void setDungeon()
    {
        Character = Instantiate(characterObj).GetComponent<CharacterController>();
        Character.SetCharacter();

        SetViewMode(GameManager.Instance.mode);

        Tiktok.TimeSet();

        SetKey();
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
}

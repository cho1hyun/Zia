using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame : MonoBehaviour
{
    public static Ingame Instance;

    public Joystick Joystick;
    public Transform SkillGroup;
    public Transform Skills;
    public Tiktok Tiktok;

    public Boss Boss;
    public Character Character;

    public List<GameObject> ModePc;
    public List<GameObject> ModeMobile;
    public List<GameObject> Key;

    void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }

    void Start()
    {
        SetViewMode(UiManager.instance.mode);

        Tiktok.TimeSet();

        SetKey();
    }

    void Update()
    {
        Time.timeScale = UiManager.instance.setting.gameObject.activeSelf ? 0 : 1;
    }

    void SetKey()
    {
        int a = 4;

        for (int i = 0; i < SkillGroup.childCount; i++)
        {
            SkillGroup.GetChild(i).GetComponent<Skill>().key = UiManager.instance.keys[a];
            a++;
        }

        for (int i = 0; i < Skills.childCount; i++)
        {
            Skills.GetChild(i).GetComponent<Skill>().key = UiManager.instance.keys[a];
            a++;
        }
    }

    public void WaitBtn()
    {
        UiManager.instance.setting.ShowSettong(true);
    }

    public void SetViewMode(Mode mode)
    {
        for (int i = 0; i < ModePc.Count; i++)
            ModePc[i].SetActive(mode == Mode.PC);

        for (int i = 0; i < ModeMobile.Count; i++)
            ModeMobile[i].SetActive(mode == Mode.Mobile);

        for (int i = 0; i < Key.Count; i++)
            Key[i].SetActive(mode == Mode.PC && UiManager.instance.setting.ShowKey);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum window
{
    None,
    Setting,
    Notice,
    Shop,
    Character,
    Dungeon,
    exit,
}

public class Lobby : MonoBehaviour
{
    public Transform UserTexts;

    public Notice Notice;

    public CharacterInfo CharacterInfo;

    public DungeonInfo DungeonInfo;

    public GameObject Shop;

    public GameObject exit;

    int openCount;

    void OnEnable()
    {
        if (openCount == 0)
        {
            UserTexts.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
            UserTexts.GetChild(1).GetComponent<TMP_Text>().text = GameManager.Instance.userData.userId;

            Notice.gameObject.SetActive(true);
            openCount++;
        }
    }

    public void CloseWindow(window open = window.None)
    {
        if (UiManager.Instance.setting != null && UiManager.Instance.setting.gameObject.activeSelf && open != window.Setting)
            UiManager.Instance.setting.SetSetting(false);

        if (Notice != null && Notice.gameObject.activeSelf && open != window.Notice)
            Notice.Open(false);

        if (DungeonInfo != null && DungeonInfo.gameObject.activeSelf && open != window.Dungeon)
            DungeonInfo.gameObject.SetActive(false);

        if (CharacterInfo != null && CharacterInfo.gameObject.activeSelf && open != window.Character) 
            CharacterInfo.OpenCharacter(false);

        if (Shop != null && Shop.gameObject.activeSelf && open != window.Shop)
            Shop.SetActive(false);

        if (exit != null && exit.activeSelf && open != window.exit)
            exit.SetActive(false);
    }

    public void OpenShop()
    {
        CloseWindow(window.Shop);

        Shop.SetActive(true);
    }
}

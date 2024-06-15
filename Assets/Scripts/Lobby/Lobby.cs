using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        StartCoroutine(GameManagerWait());
    }

    public void OpenShop()
    {
        if (gameObject.activeSelf)
        {
            if (UiManager.Instance.setting.gameObject.activeSelf)
                UiManager.Instance.setting.SetSetting(false);

            if (Notice.gameObject.activeSelf)
                Notice.Open(false);

            if (DungeonInfo.gameObject.activeSelf)
                DungeonInfo.gameObject.SetActive(false);

            if (CharacterInfo.gameObject.activeSelf)
                CharacterInfo.OpenCharacter(false);

            if (exit.activeSelf)
                exit.SetActive(false);

            Shop.SetActive(true);
        }
    }

    IEnumerator GameManagerWait()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        if (openCount == 0 && GameManager.Instance.load)
        {
            UserTexts.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
            UserTexts.GetChild(1).GetComponent<TMP_Text>().text = GameManager.Instance.userData.userId;

            Notice.gameObject.SetActive(true);
            openCount++;
        }
        else
        {
            while (UiManager.Instance == null)
            {
                yield return null;
            }
        }
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public Pade pade;

    public Logo logo;

    public Lobby lobby;

    public Ingame ingame;

    public Story Story;

    public Setting setting;

    public GameObject toast;
    public TMP_Text toastMessage;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (InPutTrue())
        {
            if (Story.gameObject.activeSelf)
            {
                Story.SkipObj.SetActive(!Story.gameObject.activeSelf);
            }
            else if (lobby.gameObject.activeSelf && !ingame.gameObject.activeSelf)
            {
                if (setting.gameObject.activeSelf)
                {
                    setting.SetSetting(false);
                }
                else if (lobby.Notice.gameObject.activeSelf)
                {
                    lobby.Notice.Open(false);
                }
                else if (lobby.DungeonInfo.gameObject.activeSelf)
                {
                    lobby.DungeonInfo.gameObject.SetActive(false);
                }
                else if (lobby.CharacterInfo.gameObject.activeSelf)
                {
                    lobby.CharacterInfo.OpenCharacter(false);
                }
                else if (lobby.Shop.activeSelf)
                {
                    lobby.Shop.SetActive(false);
                }
                else
                {
                    lobby.exit.SetActive(!lobby.exit.activeSelf);
                }
            }
            else if (!lobby.gameObject.activeSelf && ingame.gameObject.activeSelf)
            {
                if (setting.SettingUI.activeSelf)
                {
                    setting.SettingUI.SetActive(false);
                }
                else
                {
                    setting.ShowSettong(!setting.gameObject.activeSelf);
                }
            }
        }

        Time.timeScale = setting.gameObject.activeSelf && !setting.SettingUI.activeSelf && !pade.gameObject.activeSelf ? 0 : 1;
    }

    bool InPutTrue()
    {
        if (GameManager.Instance.mode != Mode.PC)
            return false;

        if (setting.nowKeySet != null)
            return false;

        if (transform.GetChild(0).gameObject.activeSelf)
            return false;

        if (transform.GetChild(5).gameObject.activeSelf)
            return false;

        if (transform.GetChild(6).gameObject.activeSelf)
            return false;

        if (Input.GetKeyDown(GameManager.Instance.menuKey))
            return true;

        return false;
    }

    public void Action(int count)
    {
        pade.gameObject.SetActive(true);
        pade.count = count;
    }

    public void ShowToast(int message = 0)
    {
        toast.SetActive(true);
        toastMessage.text = string.Format(TableManager.Instance.GetLocalizeText(message));
    }
}

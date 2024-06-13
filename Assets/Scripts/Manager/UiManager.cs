using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public Pade pade;

    public Logo logo;

    public Setting setting;

    public GameObject toast;
    public TMP_Text toastMessage;

    public bool load;

    public Mode mode; 
    public KeyCode menuKey;

    public List<KeyCode> keys;

    void Awake()
    {
        Instance = this;

        if (load)
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(i == 0);
    }

    void Update()
    {
        if (InPutTrue())
        {
            setting.SettingUI.SetActive(!setting.gameObject.activeSelf && transform.GetChild(1).gameObject.activeSelf && !transform.GetChild(2).gameObject.activeSelf);
            setting.ShowSettong(!setting.gameObject.activeSelf);

        }
    }

    bool InPutTrue()
    {
        if (mode != Mode.PC)
            return false;

        if (setting.nowKeySet != null)
            return false;

        if (transform.GetChild(0).gameObject.activeSelf)
            return false;

        if (transform.GetChild(4).gameObject.activeSelf)
            return false;

        if (transform.GetChild(5).gameObject.activeSelf)
            return false;

        if (transform.GetChild(6).gameObject.activeSelf)
            return false;

        if (transform.GetChild(7).gameObject.activeSelf)
            return false;

        if (Input.GetKeyDown(menuKey))
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

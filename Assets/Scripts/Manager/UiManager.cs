using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public Pade pade;

    public Logo logo;

    public Setting setting;

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
            if (setting.SettingUI.activeSelf)
            {
                setting.SettingUI.SetActive(false);
            }
            else
            {
                setting.gameObject.SetActive(setting.gameObject.activeSelf ? false : true);
            }
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
}

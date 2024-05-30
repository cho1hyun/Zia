using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class InputThekey : MonoBehaviour
{
    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        SetKey();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) || Input.GetKey(key))
        {
            GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    public void SetKey()
    {
        if (key == KeyCode.None)
        {
            key = (KeyCode)Enum.Parse(typeof(KeyCode), GetComponent<Image>().sprite.name);
            GetComponent<Button>().onClick.AddListener(Click);
        }

        GetComponent<Image>().sprite = SetKeySprite();
    }

    Sprite SetKeySprite()
    {
        if (key == UiManager.instance.menuKey)
        {
            return Resources.Load<SpriteAtlas>("Atlas/Black").GetSprite(key.ToString());
        }

        for (int i = 0; i < UiManager.instance.keys.Count; i++)
        {
            if (key == UiManager.instance.keys[i])
            {
                return Resources.Load<SpriteAtlas>("Atlas/Black").GetSprite(key.ToString());
            }
        }

        return Resources.Load<SpriteAtlas>("Atlas/White").GetSprite(key.ToString());
    }

    public void Click()
    {
        if (UiManager.instance.setting.nowKeySet != null)
        {
            for (int i = 0; i < UiManager.instance.setting.Inputs.childCount; i++)
            {
                if (UiManager.instance.setting.Inputs.GetChild(i).GetChild(1).gameObject == UiManager.instance.setting.nowKeySet.gameObject)
                {
                    if (i == 0)
                    {
                        UiManager.instance.menuKey = key;
                    }

                    if (i >= 1 && i <= UiManager.instance.keys.Count)
                    {
                        UiManager.instance.keys[i - 1] = key;

                        if (i >= 5 && i <= 10 && Ingame.Instance != null) Ingame.Instance.SkillGroup.GetChild(i - 5).GetComponent<Skill>().key = key;

                        if (i >= 11 && i <= 12 && Ingame.Instance != null) Ingame.Instance.Skills.GetChild(i - 11).GetComponent<Skill>().key = key;
                    }
                }
            }

            UiManager.instance.setting.nowKeySet.text = UiManager.instance.setting.KeyString(key.ToString());
            EventSystem.current.SetSelectedGameObject(gameObject);
            UiManager.instance.setting.Duplication(key);

            if (UiManager.instance.setting.nowKey != null)
            {
                UiManager.instance.setting.nowKey.text = UiManager.instance.setting.KeyString(key.ToString());
                UiManager.instance.setting.nowKey = null;
            }

            UiManager.instance.setting.SetKeysSprite();
        }
    }
}

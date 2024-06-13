using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public Image cooltimeImg;
    public TMP_Text cooltimeText;
    public GameObject keyObj;
    public TMP_Text keyText;

    public float coolltime;
    public int damage;
    public int hitCount;

    public KeyCode key;

    float delay;

    public void Update()
    {
        if (GameManager.Instance.mode == Mode.PC && !UiManager.Instance.setting.gameObject.activeSelf && Input.GetKey(key) && delay <= 0f)
        {
            UseSkill();

            delay = 0.5f;
        }

        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        }
    }

    public void SetKey(KeyCode code)
    {
        key = code;
    }

    public void ShowKey(bool active)
    {
        keyObj.SetActive(active);
        keyText.text = key.ToString();
    }

    public void UseSkill()
    {
        if (!cooltimeImg.gameObject.activeSelf)
        {
            StartCoroutine(CoolTimeRun());

            UiManager.Instance.ingame.Boss.GetDamage(damage, hitCount);


            for (int i = 0; i < GameManager.Instance.keys.Count; i++)
            {
                if (key == GameManager.Instance.keys[i])
                {
                    switch (i)
                    {
                        case 4:
                            Character.Instance.Main.SetTrigger("Skill0");
                            break;
                        case 5:
                            Character.Instance.Main.SetTrigger("Evasion");
                            break;
                        case 6:
                            Character.Instance.SwitchCharacter();
                            break;
                        case 7:
                            Character.Instance.Main.SetTrigger("Skill1");
                            break;
                        case 8:
                            Character.Instance.Main.SetTrigger("Skill2");
                            break;
                        case 9:
                            Character.Instance.Main.SetTrigger("Skill3");
                            break;
                        case 10:
                            Character.Instance.Main.SetTrigger("Ult");
                            break;
                        case 11:
                            break;
                        case 12:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    IEnumerator CoolTimeRun()
    {
        cooltimeImg.gameObject.SetActive(true);

        float time = 0.0f;

        while (time < coolltime)
        {
            cooltimeImg.fillAmount = 1f - time / coolltime;

            cooltimeText.text = (coolltime - time).ToString("F1");

            time += Time.deltaTime;

            yield return null;
        }

        cooltimeImg.gameObject.SetActive(false);
    }
}

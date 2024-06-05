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
        if (UiManager.Instance.mode == Mode.PC && !UiManager.Instance.setting.gameObject.activeSelf && Input.GetKey(key) && delay <= 0f)
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

            Ingame.Instance.Boss.GetDamage(damage, hitCount);
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

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

    int atkCount;

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
        Animator animator = UiManager.Instance.ingame.Character.Main;
        if (!cooltimeImg.gameObject.activeSelf && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run")))
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
                            animator.SetTrigger("Skill0");

                            if (animator.GetInteger("Type") == 90000)
                            {
                                animator.SetInteger("AttackCount", atkCount);
                                atkCount = atkCount >= 4 ? 0 : atkCount + 1;
                            }

                            break;
                        case 5:
                            animator.SetTrigger("Evasion");
                            break;
                        case 6:
                            UiManager.Instance.ingame.Character.SwitchCharacter();
                            break;
                        case 7:
                            animator.SetTrigger("Skill1");
                            break;
                        case 8:
                            animator.SetTrigger("Skill2");
                            break;
                        case 9:
                            animator.SetTrigger("Skill3");
                            break;
                        case 10:
                            animator.SetTrigger("Ult");
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

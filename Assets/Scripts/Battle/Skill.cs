using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public Image cooltimeImg;
    public Image cooltimeImg2;
    public Image SkillImg;
    public TMP_Text cooltimeText;
    public GameObject keyObj;
    public TMP_Text keyText;

    public float coolltime;
    public int damage;
    public int hitCount;

    public KeyCode key;

    float delay;

    int atkCount;

    Coroutine coolRun;

    public void Update()
    {
        if (isPlay())
        {
            UseSkill();

            delay = 0.5f;
        }

        if (delay > 0f)
        {
            delay -= Time.deltaTime;
        }
    }

    bool isPlay()
    {
        if (GameManager.Instance.mode != Mode.PC)
            return false;

        if (UiManager.Instance.ingame.Character == null)
            return false;

        if (UiManager.Instance.setting.gameObject.activeSelf)
            return false;

        if (UiManager.Instance.ingame.Over.gameObject.activeSelf)
            return false;

        if (delay > 0f)
            return false;

        if (Input.GetKey(key))
            return true;

        return false;
    }

    public void ChangeCharacter()
    {
        if (coolRun != null)
            StopCoroutine(coolRun);
        cooltimeImg.gameObject.SetActive(false);
    }

    public void SetSkill(SkillSet set)
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Skill");

        SkillImg.sprite = spriteAtlas.GetSprite(set.name.ToString()); ;
        coolltime = set.cool;
    }
    public void SetSkillUlt(int id)
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Icon");

        SkillImg.sprite= spriteAtlas.GetSprite(id.ToString());
        coolltime = TableManager.Instance.GetCharacterSkill(TableManager.Instance.GetCharacter(id).skillset, SkillType.Ult).cool;

        Color color = TableManager.Instance.GetAttributeColor(TableManager.Instance.GetCharacter(id).attribute);
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = color;
        transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().color = color;
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
            if (coolltime > 0)
                coolRun = StartCoroutine(CoolTimeRun());

            if (UiManager.Instance.ingame.Boss.GetComponent<CanvasGroup>().alpha != 1)
                UiManager.Instance.ingame.Boss.GetDamage(0, hitCount);
            else
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
        float time = 0.0f;

        cooltimeImg.gameObject.SetActive(true);

        if (cooltimeImg2 != null)
        {
            cooltimeImg2.gameObject.SetActive(true);

            cooltimeImg.fillAmount = 1f;
            cooltimeImg2.fillAmount = 1f;

            float _coolTime = coolltime / 15 * 7;
            while (time < _coolTime)
            {
                cooltimeImg2.fillAmount = 1f - time / _coolTime;

                cooltimeText.text = (coolltime - time).ToString("F1");

                time += Time.deltaTime;

                yield return null;
            }

            float _time = 0.0f;
            _coolTime = coolltime / 15 * 8;
            while (time < _coolTime)
            {
                cooltimeImg.fillAmount = 1f - _time / _coolTime;

                cooltimeText.text = (coolltime - time).ToString("F1");

                time += Time.deltaTime;
                _time += Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            while (time < coolltime)
            {
                cooltimeImg.fillAmount = 1f - time / coolltime;

                cooltimeText.text = (coolltime - time).ToString("F1");

                time += Time.deltaTime;

                yield return null;
            }
        }

        cooltimeImg.gameObject.SetActive(false);
    }
}

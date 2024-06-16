using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    public Transform HpGroup;
    public TMP_Text HpPer;
    public TMP_Text Hpcount;
    public Image Gage;

    public CanvasGroup Combo;
    public TMP_Text ComboText;

    public float Hp;
    public int Hps;
    float nowHp;

    int damageSum;

    int gage;
    int comboCount;
    int nowComboCount;
    int gageComboCount;

    float gageWidth;
    float fillWidth;

    Coroutine damages;
    Coroutine combos;
    Coroutine gages;

    bool weakness;

    private void Awake()
    {
        nowHp = Hp;
    }

    public void GetDamage(int Damage, int hitCount)
    {
        if (hitCount == 0)
            return;

        if (damages != null) StopCoroutine(damages);
        damages = StartCoroutine(SetDamage(Damage));

        if (combos != null) StopCoroutine(combos);
        combos = StartCoroutine(ComboUp(hitCount));
    }

    IEnumerator SetDamage(int Damage)
    {
        if (weakness) Damage *= 2;

        damageSum += Damage;
        float lineHp = Hp / HpGroup.childCount;

        int dam = (int)(damageSum / lineHp);
        float ame = damageSum % lineHp;

        float time = 0f;
        if (dam >= 1)
        {
            for (int i = 0; i < dam; i++)
            {
                Image fill = HpGroup.GetChild(HpGroup.childCount - 1 - i).GetChild(0).GetComponent<Image>();
                if (fill.fillAmount > 0)
                {
                    float fill_0 = fill.fillAmount;
                    while (time < 0.1f)
                    {
                        fill.fillAmount = fill_0 - time * 10f * fill_0;

                        yield return null;
                    }
                    fill.fillAmount = 0f;
                }
                HpGroup.GetChild(HpGroup.childCount - 1 - i).GetComponent<Image>().fillAmount = 0f;
            }

            time = 0f;
            while (time < 0.5f)
            {
                time += Time.deltaTime;
            }
        }
        else
        {
            while (time < 1f)
            {
                time += Time.deltaTime;
            }
        }

        nowHp -= damageSum;


        SetHp();
    }

    void SetHp()
    {
        nowHp = Mathf.Clamp(nowHp, 0, Hp);

        string hpPer = nowHp == Hp ? "100" : (nowHp * 100 / Hp).ToString("F2");
        HpPer.text = hpPer + "%";

        Hpcount.transform.parent.gameObject.SetActive(Hps != 0);
        Hpcount.text = Hps.ToString();
    }

    IEnumerator ComboUp(int hitCount)
    {
        int c = nowComboCount == 0 ? 1 : nowComboCount;
        ComboText.text = c + " Hit";
        Combo.alpha = 1;

        comboCount += hitCount;

        while (nowComboCount < comboCount)
        {
            nowComboCount++;
            ComboText.text = nowComboCount + " Hit";

            yield return new WaitForSeconds(0.05f);

            if (gageComboCount < nowComboCount / 100)
            {
                gageComboCount = gageComboCount <= 10 ? nowComboCount / 100 : 10;

                if (gages != null) StopCoroutine(gages);
                gages = StartCoroutine(GageUp());
            }
        }

        yield return new WaitForSeconds(0.5f);
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;

            Combo.alpha = 1 - time * 2f;

            yield return null;
        }
        Combo.alpha = 0f;
        nowComboCount = 0;
        comboCount = 0;
        gageComboCount = 0;

        combos = null;
    }

    IEnumerator GageUp()
    {
        if (gage >= 10)
            yield break;

        float time = 0;
        gageWidth = gageWidth > 32.0f ? gageWidth : 32.0f > 32.0f + 56.0f * gage ? gageWidth : 32.0f + 56.0f * gage;
        float nextWidth = 88.0f + 56.0f * gage;
        while (time < 0.5f)
        {
            time += Time.deltaTime;

            Gage.rectTransform.sizeDelta = new Vector2(gageWidth + (nextWidth - gageWidth) * time * 2f, Gage.rectTransform.sizeDelta.y);
            gageWidth = Gage.rectTransform.sizeDelta.x;
            yield return null;
        }
        gage++;
        Gage.rectTransform.sizeDelta = new Vector2(32.0f + 56f * gage, Gage.rectTransform.sizeDelta.y);

        if (gage >= 10)
            StartCoroutine(GageMax());
    }

    IEnumerator GageMax()
    {
        //

        float time = 0;
        weakness = true;

        while (time < 0.2f)
        {
            time += Time.deltaTime;

            Gage.color = new Color32(100, (byte)(200 - 100 * time * 5), (byte)(200 - 100 * time * 5), 255);
            yield return null;
        }
        Gage.color = new Color32(100, 100, 100, 255);

        time = 0;
        while (time < 4.3f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        time = 0;
        float nowWidth = 32.0f + 56f * gage;
        while (time < 0.5f)
        {
            time += Time.deltaTime;

            Gage.rectTransform.sizeDelta = new Vector2(nowWidth + -560f * time * 2f, Gage.rectTransform.sizeDelta.y);
            yield return null;
        }
        gage = 0;
        weakness = false;
        gageWidth = 32.0f;
        Gage.color = new Color32(100, 200, 200, 255);
        Gage.rectTransform.sizeDelta = new Vector2(32.0f, Gage.rectTransform.sizeDelta.y);
    }
}

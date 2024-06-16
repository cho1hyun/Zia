using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tiktok : MonoBehaviour
{
    public Image timeImage;
    public TMP_Text timeText;

    public float timeLimit;

    public Coroutine time;


    void OnDisable()
    {
        if (time != null)
            StopCoroutine(time);
    }

    public void TimeSet()
    {
        time = StartCoroutine(TimeRun());
    }

    IEnumerator TimeRun()
    {
        float time = 0.0f;

        while (UiManager.Instance.ingame.Over != null && time < timeLimit && !UiManager.Instance.ingame.Over.gameObject.activeSelf) 
        {
            timeImage.fillAmount = 1f - time / timeLimit;

            int m = (int)(timeLimit - time) / 60;
            float s = (timeLimit - time) % 60;

            string mStr = m > 0 ? m.ToString("00") + ":" : string.Empty;

            timeText.text = mStr+ s.ToString("00.00");

            time += Time.deltaTime;

            yield return null;
        }

        UiManager.Instance.ingame.GameOver(false);

    }
}

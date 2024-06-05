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

    public void TimeSet()
    {
        StartCoroutine(TimeRun());
    }

    IEnumerator TimeRun()
    {
        float time = 0.0f;

        while (time < timeLimit)
        {
            timeImage.fillAmount = 1f - time / timeLimit;

            int m = (int)(timeLimit - time) / 60;
            float s = (timeLimit - time) % 60;

            string mStr = m > 0 ? m.ToString("00") + ":" : string.Empty;

            timeText.text = mStr+ s.ToString("00.00");

            time += Time.deltaTime;

            yield return null;
        }
    }
}

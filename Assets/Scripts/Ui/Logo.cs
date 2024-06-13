using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public TMP_Text text;
    public List<GameObject> obj;
    public GameObject LoginButton;

    public GameObject LoginUI;
    public GameObject GoogleLoginUI;

    void OnEnable()
    {
        StartCoroutine(TextAction());
    }

    IEnumerator TextAction()
    {
        string txt = string.Empty;
        int time = 0;

        while (time <= 15)
        {
            txt = string.Empty;
            for (int i = 0; i < (time % 3) + 1; i++)
            {
                txt += ".";
            }

            text.text = string.Format(TableManager.Instance.GetLocalizeText(16), txt);

            time += 1;
            yield return new WaitForSeconds(0.1f);
        }

        txt = "{0}/{1}";
        text.text = string.Format(TableManager.Instance.GetLocalizeText(17), string.Format(txt, 0, TableManager.Instance.getMaxData()));
        yield return null;

        for (int i = 1; i <= TableManager.Instance.getMaxData(); i++)
        {
            yield return StartCoroutine(TableReader.LoadData(GameManager.Instance.Address(i), GameManager.Instance.Sheet(i), TableManager.Instance.GetData));

            Task<Exception> task = GameManager.Instance.TableDataLoad(i);

            yield return new WaitUntil(() => task.IsCompleted);

            if (i < TableManager.Instance.getMaxData())
            {
                text.text = string.Format(TableManager.Instance.GetLocalizeText(17), string.Format(txt, i, TableManager.Instance.getMaxData()));
            }
            else
            {
                text.text = string.Format(TableManager.Instance.GetLocalizeText(17), string.Format(txt, TableManager.Instance.getMaxData(), TableManager.Instance.getMaxData()));
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        text.gameObject.SetActive(false);
        LoginButton.SetActive(true);
    }

    public void Login()
    {
        LoginUI.SetActive(true);
    }

    public void GoogleLogin()
    {
        LoginButton.SetActive(false);
        GoogleLoginUI.SetActive(true);
    }

    public void LoginAction()
    {
        LoginButton.SetActive(false);

        text.gameObject.SetActive(true);
        text.text = string.Format(TableManager.Instance.GetLocalizeText(26));

        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
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
        string download = "리소스를 받아 오는 중";

        int time = 0;

        while (time <= 15) {

            text.text = download;
            for (int i = 0; i < (time % 3) + 1; i++)
            {
                text.text += ".";
            }

            time += 1;
            yield return new WaitForSeconds(0.1f);
        }


        string tableLoad = "테이블을 읽어 들이는중 (";
        time = 0;

        while (time <= 50)
        {

            text.text = tableLoad;
            text.text += time + "/50)";
            time += 1;
            yield return new WaitForSeconds(0.05f);
        }

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
        text.fontSize = 100;
        text.text = "게임 시작";

        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);
        }
    }
}

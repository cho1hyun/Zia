using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    public Image noticeImg;
    public TMP_Text noticeStr;

    public int nowNotice;

    public GameObject NoticeBtn;
    public Transform NoticeBtnPar;

    void OnEnable()
    {
        CreateNotice();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Set(int n)
    {
        nowNotice = n;

        //noticeImg.sprite = noticeImgs[nowNotice];
        //noticeStr.text = noticeStrs[nowNotice];

        for (int i = 0; i < NoticeBtnPar.childCount; i++)
        {
            NoticeBtnPar.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = i == n ? Color.white : Color.black;
            NoticeBtnPar.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().color = i == n ? Color.white : Color.black;
        }
    }

    void CreateNotice()
    {
        if (NoticeBtnPar.childCount <= 0)
        {
            for (int i = 0; i < 0/**/; i++)
            {
                Button button = Instantiate(NoticeBtn, NoticeBtnPar).GetComponent<Button>();

                int index = i;
                button.onClick.AddListener(delegate { Set(index); });
            }

            Set(0);
        }
    }
}

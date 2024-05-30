using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    public Image noticeImg;
    public TMP_Text noticeStr;

    public GameObject nextBtn;
    public GameObject prevBtn;

    public List<Sprite> noticeImgs;
    public List<string> noticeStrs;

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

    public void Next(int next = 1)
    {
        nowNotice += next;

        prevBtn.SetActive(nowNotice != 0);
        nextBtn.SetActive(nowNotice != noticeImgs.Count - 1);

        noticeImg.sprite = noticeImgs[nowNotice];
        noticeStr.text = noticeStrs[nowNotice];

        LayoutRebuilder.ForceRebuildLayoutImmediate(noticeStr.rectTransform);

        for (int i = 0; i < NoticeBtnPar.childCount; i++)
        {
            NoticeBtnPar.GetChild(i).transform.GetChild(0).gameObject.SetActive(i == nowNotice);
        }
    }

    public void Set(int n)
    {
        nowNotice = n;

        prevBtn.SetActive(nowNotice != 0);
        nextBtn.SetActive(nowNotice != noticeImgs.Count - 1);

        noticeImg.sprite = noticeImgs[nowNotice];
        noticeStr.text = noticeStrs[nowNotice];

        for (int i = 0; i < NoticeBtnPar.childCount; i++)
        {
            NoticeBtnPar.GetChild(i).transform.GetChild(0).gameObject.SetActive(i == nowNotice);
        }
    }

    void CreateNotice()
    {
        if (NoticeBtnPar.childCount <= 0)
        {
            for (int i = 0; i < noticeImgs.Count; i++)
            {
                Button button = Instantiate(NoticeBtn, NoticeBtnPar).GetComponent<Button>();

                int index = i;
                button.onClick.AddListener(delegate { Set(index); });
            }

            Next(0);
        }
    }
}

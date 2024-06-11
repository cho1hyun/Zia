using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    List<NoticeTable> noticeList;

    public Image noticeImg;
    public TMP_Text noticeStr;

    public GameObject NoticeBtn;
    public Transform NoticeBtnPar;

    public Transform ScrollViewGroup;

    public int nowNotice;

    void OnEnable()
    {
        CreateNotice();
    }

    public void Open(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Link()
    {
        switch (noticeList[nowNotice].type)
        {
            case NoticeType.None:
                break;
            case NoticeType.Link:
                Application.OpenURL(noticeList[nowNotice].link);
                break;
            case NoticeType.Shop:
                break;
            default:
                break;
        }
    }

    void Set(int n)
    {
        nowNotice = n;

        noticeImg.transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        noticeStr.text = TableManager.Instance.GetLocalizeText(noticeList[n].desc);

        StartCoroutine(SpriteLoad());

        for (int i = 0; i < NoticeBtnPar.childCount; i++)
        {
            NoticeBtnPar.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = i == n ? Color.white : Color.black;
            NoticeBtnPar.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().color = i == n ? Color.white : Color.black;
        }
    }

    void CreateNotice()
    {
        noticeList = TableManager.Instance.GetNoticeAll();

        if (NoticeBtnPar.childCount <= 0)
        {
            for (int i = 0; i < noticeList.Count; i++)
            {
                Button button = Instantiate(NoticeBtn, NoticeBtnPar).GetComponent<Button>();

                button.transform.GetChild(1).GetComponent<TMP_Text>().text = TableManager.Instance.GetLocalizeText(noticeList[i].name);

                int index = i;
                button.onClick.AddListener(delegate { Set(index); });
            }

            Set(0);
        }

        ScrollViewGroup.GetChild(1).GetChild(0).GetComponent<RectTransform>().offsetMax = new Vector2(3 * (ScrollViewGroup.GetChild(1).GetComponent<RectTransform>().rect.width + ScrollViewGroup.GetComponent<HorizontalLayoutGroup>().spacing), 0);
    }

    IEnumerator SpriteLoad()
    {
        if (!noticeList[nowNotice].img.Contains("."))
        {
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Notice");

            noticeImg.sprite = spriteAtlas.GetSprite(noticeList[nowNotice].img);
        }
        else
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(noticeList[nowNotice].img);

            yield return www.SendWebRequest();

            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            noticeImg.sprite = sprite;
        }
        noticeImg.GetComponent<RectTransform>().sizeDelta = new Vector2(noticeImg.GetComponent<RectTransform>().sizeDelta.x, noticeImg.GetComponent<RectTransform>().sizeDelta.x / noticeImg.sprite.bounds.size.x * noticeImg.sprite.bounds.size.y);
    }
}

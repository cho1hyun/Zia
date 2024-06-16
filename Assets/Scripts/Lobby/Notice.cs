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

    List<Sprite> sprites;

    void OnEnable()
    {
        CreateNotice();
    }

    public void Open(bool active)
    {
        gameObject.SetActive(active);

        if (active)
            Set(0);
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
                UiManager.Instance.lobby.OpenShop();
                break;
            default:
                break;
        }
    }

    void Set(int n)
    {
        nowNotice = n;

        ScrollViewGroup.GetChild(1).GetChild(0).GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);

        noticeStr.text = TableManager.Instance.GetLocalizeText(noticeList[n].desc);

        noticeImg.sprite = sprites[n];

        if (noticeImg.sprite != null)
        {
            noticeImg.GetComponent<RectTransform>().sizeDelta = new Vector2(noticeImg.GetComponent<RectTransform>().sizeDelta.x, noticeImg.GetComponent<RectTransform>().sizeDelta.x / noticeImg.sprite.bounds.size.x * noticeImg.sprite.bounds.size.y);
        }
        else
        {
            noticeImg.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }


        for (int i = 0; i < NoticeBtnPar.childCount; i++)
        {
            NoticeBtnPar.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = i == n ? Color.white : Color.black;
            NoticeBtnPar.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().color = i == n ? Color.black : Color.white;
        }
    }

    void CreateNotice()
    {
        noticeList = TableManager.Instance.GetNoticeAll();

        if (NoticeBtnPar.childCount <= 0 && TableManager.Instance.GetNoticeAll() != null)
        {
            for (int i = 0; i < noticeList.Count; i++)
            {
                Button button = Instantiate(NoticeBtn, NoticeBtnPar).GetComponent<Button>();

                button.transform.GetChild(1).GetComponent<TMP_Text>().text = TableManager.Instance.GetLocalizeText(noticeList[i].name);

                int index = i;
                button.onClick.AddListener(delegate { Set(index); });
            }

            StartCoroutine(SpriteLoad());
        }

        ScrollViewGroup.GetChild(1).GetChild(0).GetComponent<RectTransform>().offsetMax = new Vector2(3 * (ScrollViewGroup.GetChild(1).GetComponent<RectTransform>().rect.width + ScrollViewGroup.GetComponent<HorizontalLayoutGroup>().spacing), 0);
    }

    IEnumerator SpriteLoad()
    {
        sprites = new List<Sprite>();

        for (int i = 0; i < noticeList.Count; i++)
        {
            if (!noticeList[i].img.Contains("."))
            {
                SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Notice");

                sprites.Add(spriteAtlas.GetSprite(noticeList[i].img));
            }
            else
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(noticeList[i].img);

                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    sprites.Add(sprite);
                }
                else
                {
                    sprites.Add(null);
                }
            }
        }
        Set(0);

        UiManager.Instance.pade.PadeOff();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GoodsSlot : MonoBehaviour
{
    public int goodsId;

    public Image goodsIcon;
    public TMP_Text goodsCount;

    void OnEnable()
    {
        AmountChange();

        GameManager.Instance.amountChange += AmountChange;
    }

    public void AmountChange()
    {
        if (goodsId != 0)
        {
            GoodsTable goods = TableManager.Instance.GetGoods(goodsId);
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Icon");

            goodsIcon.sprite = spriteAtlas.GetSprite(goods.id.ToString());
            goodsCount.text = string.Format("{0:n0}", GameManager.Instance.userData.gooods[goodsId]);
        }
    }

    public void OpenShop()
    {
        UiManager.Instance.lobby.OpenShop();
    }
}

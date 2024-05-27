using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform outline;
    public RectTransform handle;

    Vector2 size;

    public void OnBeginDrag(PointerEventData eventData)
    {
        size = new Vector2(Screen.width, Screen.height) + GetComponent<RectTransform>().offsetMax + GetComponent<RectTransform>().offsetMin;

        Vector2 pos = eventData.position - size / 2;

        outline.gameObject.SetActive(true);

        outline.anchoredPosition = new Vector2(Mathf.Clamp(pos.x, (outline.sizeDelta.x - size.x) / 2.25f, (size.x - outline.sizeDelta.x) / 2.25f), Mathf.Clamp(pos.y, (outline.sizeDelta.y - size.y) / 2.25f, (size.y - outline.sizeDelta.y) / 2.25f));
        handle.anchoredPosition = Vector3.zero;

        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position - size / 2 - outline.anchoredPosition;

        handle.anchoredPosition = new Vector2(Mathf.Clamp(pos.x, outline.sizeDelta.x / -2, outline.sizeDelta.x / 2), Mathf.Clamp(pos.y, outline.sizeDelta.y / -2, outline.sizeDelta.y / 2));

        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        outline.gameObject.SetActive(false);

        //throw new System.NotImplementedException();
    }
}

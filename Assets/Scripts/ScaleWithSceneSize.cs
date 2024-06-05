using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithSceneSize : MonoBehaviour
{
    void Update()
    {
        SetSize();
    }

    void SetSize()
    {
        float width = Screen.width / 16;
        float height = Screen.height / 9;

        if (width == height)
        {
            GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
        else if (height > width)
        {
            float w = (GetComponent<RectTransform>().rect.height / 9 * 16 - GetComponent<RectTransform>().rect.height / Screen.height * Screen.width) / 2;
            GetComponent<RectTransform>().offsetMin = new Vector2(-w, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(w, 0);
        }
        else
        {
            GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
    }
}

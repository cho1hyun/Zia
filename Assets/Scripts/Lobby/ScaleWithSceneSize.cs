using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ScaleWithSceneSize : MonoBehaviour
{
    public RectTransform Top;
    public RectTransform Middle;
    public RectTransform Bottom;

    float up;
    float down;
    float left;
    float right;

    void Start()
    {
        up = Top.rect.height;
        down = Bottom.rect.height;
        left = Middle.offsetMin.x ;
        right = -Middle.offsetMax.x;
    }

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

            Top.sizeDelta = new Vector2(0, up);
            Middle.offsetMin = new Vector2(left, -up);
            Middle.offsetMax = new Vector2(-right, down);
            Bottom.sizeDelta = new Vector2(0, down);
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

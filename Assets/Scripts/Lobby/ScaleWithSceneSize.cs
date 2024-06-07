using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ScaleWithSceneSize : MonoBehaviour
{
    public RectTransform Top;
    public RectTransform Middle;
    public RectTransform Bottom;
    public List<RectTransform> Doors;

    float up;
    float down;
    float left;
    float right;

    Vector2 max;
    Vector2 min;

    void Start()
    {
        up = Top.rect.height;
        down = Bottom.rect.height;
        left = Middle.offsetMin.x ;
        right = -Middle.offsetMax.x;

        max = Doors[0].offsetMax;
        min = Doors[0].offsetMin;

    }

    void Update()
    {
        SetSize();
    }

    void SetSize()
    {
        float width = Screen.width / 16;
        float height = Screen.height / 9;

        float y = 16.0f / 9.0f * Screen.height / Screen.width;

        float x = (GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.height * 1080f - GetComponent<RectTransform>().rect.width);

        if (width == height)
        {
            GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            Top.sizeDelta = new Vector2(0, up);
            Middle.offsetMin = new Vector2(left, down);
            Middle.offsetMax = new Vector2(-right, -up);
            Bottom.sizeDelta = new Vector2(0, down);

            Doors[0].offsetMax = max;
            Doors[0].offsetMin = min;

            Doors[1].offsetMax = max;
            Doors[1].offsetMin = min;
        }
        else if (height > width)
        {
            float w = (GetComponent<RectTransform>().rect.height / 9 * 16 - GetComponent<RectTransform>().rect.height / Screen.height * Screen.width) / 2;
            GetComponent<RectTransform>().offsetMin = new Vector2(-w, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(w, 0);

            Top.sizeDelta = new Vector2(0, up * y);
            Middle.offsetMin = new Vector2(left * y, down * y);
            Middle.offsetMax = new Vector2(-right * y, -up * y);
            Bottom.sizeDelta = new Vector2(0, down * y);

            Doors[0].offsetMax = new Vector2(max.x * y, max.y * y);
            Doors[0].offsetMin = new Vector2(min.x * y, min.y * y);

            Doors[1].offsetMax = new Vector2(max.x * y, max.y * y);
            Doors[1].offsetMin = new Vector2(min.x * y, min.y * y);
        }
        else
        {
            GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            Top.sizeDelta = new Vector2(0, up * y);
            Middle.offsetMin = new Vector2((left + x / 2) / 1080f * GetComponent<RectTransform>().rect.height, down * y);
            Middle.offsetMax = new Vector2((-right - x / 2) / 1080f * GetComponent<RectTransform>().rect.height, -up * y);
            Bottom.sizeDelta = new Vector2(0, down * y);

            Doors[0].offsetMax = new Vector2(max.x * y, max.y * y);
            Doors[0].offsetMin = new Vector2(min.x * y, min.y * y);

            Doors[1].offsetMax = new Vector2(max.x * y, max.y * y);
            Doors[1].offsetMin = new Vector2(min.x * y, min.y * y);
        }
    }
}

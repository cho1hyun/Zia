using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pade : MonoBehaviour
{
    public int count;

    public void PadeOnAction()
    {
        for (int i = 0; i < UiManager.Instance.transform.childCount; i++)
        {
            UiManager.Instance.transform.GetChild(i).gameObject.SetActive(i == count);
        }
    }

    public void PadeOffAction()
    {
        gameObject.SetActive(false);
    }
}

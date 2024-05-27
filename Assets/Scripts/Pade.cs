using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pade : MonoBehaviour
{
    public int count;

    public void PadeOnAction()
    {
        switch (count)
        {
            case 0:
                UiManager.instance.transform.GetChild(0).gameObject.SetActive(false);
                UiManager.instance.transform.GetChild(2).gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void PadeOffAction()
    {
        gameObject.SetActive(false);
    }
}

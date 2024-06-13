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
            if (UiManager.Instance.transform.GetChild(i) != transform)
            {
                UiManager.Instance.transform.GetChild(i).gameObject.SetActive(i == count);
            }
        }
        if (count != 1)
        {
            PadeOff();
        }
    }

    public void PadeOffAction()
    {
        gameObject.SetActive(false);
    }

    public void PadeOff()
    {
        GetComponent<Animator>().SetTrigger("Pade");
    }
}

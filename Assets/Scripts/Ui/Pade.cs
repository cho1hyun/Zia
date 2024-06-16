using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pade : MonoBehaviour
{
    public int count;
    int first = 0;

    public void PadeOnAction()
    {
        switch (count)
        {
            case 0:
                return;
            case 1:
                UiManager.Instance.lobby.CloseWindow();
                break;
            case 4:
                PadeOff();
                return;
            default:
                break;
        }

        for (int i = 0; i < UiManager.Instance.transform.childCount; i++)
        {
            if (UiManager.Instance.transform.GetChild(i) != transform)
                UiManager.Instance.transform.GetChild(i).gameObject.SetActive(i == count);
        }

        switch (count)
        {
            case 2:
                UiManager.Instance.ingame.setDungeon();
                break;
            default:
                break;
        }

        if (first == 0 && count != 1)
        {
            PadeOff();
            first++;
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

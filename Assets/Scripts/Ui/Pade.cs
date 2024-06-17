using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pade : MonoBehaviour
{
    public int count;
    int first = 0;
    public bool tuto;

    public void PadeOnAction()
    {
        switch (count)
        {
            case 0:
                return;
            case 1:
                GameManager.Instance.gameObject.GetComponent<SoundManager>().PlayBgm("LobbyBGM001");
                UiManager.Instance.ingame.OffDungoen();
                UiManager.Instance.lobby.CloseWindow();
                break;
            case 2:
                UiManager.Instance.ingame.OffDungoen();
                UiManager.Instance.lobby.CloseWindow();
                break;
            case 4:
                GameManager.Instance.gameObject.GetComponent<SoundManager>().StopBgm();
                UiManager.Instance.Story.gameObject.SetActive(false);
                PadeOff();
                return;
            default:
                break;
        }

        for (int i = 0; i < UiManager.Instance.transform.childCount; i++)
        {
            if (UiManager.Instance.transform.GetChild(i) != transform && UiManager.Instance.transform.GetChild(i).gameObject != gameObject)
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

        if (count != 1 || first != 0)
        {
            PadeOff();
        }

        if (tuto)
        {
            tuto = false;
            UiManager.Instance.Story.SetScenario(0);
        }
    }

    public void PadeOffAction()
    {
        gameObject.SetActive(false);
    }

    public void PadeOff()
    {
        GetComponent<Animator>().SetTrigger("Pade");

        if (first == 0)
        {
            first++;
        }
    }
}

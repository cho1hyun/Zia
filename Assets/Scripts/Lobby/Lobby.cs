using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public Notice Notice;

    public DungeonInfo DungeonInfo;

    public GameObject exit;

    int openCount;

    void OnEnable()
    {
        if (openCount == 0 && GameManager.Instance.load)
        {
            Notice.gameObject.SetActive(true);
            openCount++;
        }
        else
        {
            StartCoroutine(GameManagerWait());
        }
    }

    IEnumerator GameManagerWait()
    {
        while (UiManager.Instance == null)
        {
            yield return null;
        }

        UiManager.Instance.pade.PadeOff();
    }
}

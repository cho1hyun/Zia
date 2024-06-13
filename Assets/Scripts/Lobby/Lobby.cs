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
        if (openCount == 0)
        {
            Notice.gameObject.SetActive(true);
            openCount++;
        }
        else
        {
            UiManager.Instance.pade.PadeOff();
        }
    }
}

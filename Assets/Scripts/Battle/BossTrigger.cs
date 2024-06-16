using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            UiManager.Instance.ingame.Boss.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}

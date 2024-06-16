using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool isBoss;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && !other.isTrigger)
        {
            if (!isBoss)
                Destroy(gameObject);

            else
            {
                UiManager.Instance.ingame.Boss.GetDamage(500, 1);
            }
        }
    }
}

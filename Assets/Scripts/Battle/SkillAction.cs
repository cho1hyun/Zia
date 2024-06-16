using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    public List<GameObject> Weapon;

    IEnumerator startMove(float dis)
    {
        Animator animator = UiManager.Instance.ingame.Character.Main;
        int a = 1;
        float b = 0;
        switch (animator.GetInteger("Type"))
        {
            case 90000:
                a = 1;
                break;
            case 90001:
                a = -1;
                break;
            default:
                break;
        }
        while (b < 1.0f)
        {
            transform.parent.parent.parent.Translate(Vector3.forward * a * dis * Time.deltaTime);
            b += Time.deltaTime;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    public List<GameObject> Weapon;

    IEnumerator startMove(float dis)
    {
        float pos = dis > 0 ? 1 : -1;
        dis = dis > 0 ? dis : -dis;
        float time = 0;

        while (time < 1.0f)
        {
            transform.parent.parent.parent.Translate(Vector3.forward * pos * dis * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
}

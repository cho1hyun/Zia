using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillAction : MonoBehaviour
{

    public void DaggerSkill2(float dis)
    {
        float t = 0;
        while (t < 1.0f)
        {
            transform.parent.parent.parent.Translate(Vector3.forward * dis * 0.1f);
            t += 0.1f;
        }
    }
}

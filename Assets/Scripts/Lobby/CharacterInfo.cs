using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public GameObject CharacterStage;
    public List<CanvasGroup> CanvasGroups;

    public void OpenCharacter(bool active)
    {
        CharacterStage.SetActive(active);
        CanvasGroups[0].alpha = active ? 0 : 1;
        CanvasGroups[1].alpha = active ? 0 : 1;
        gameObject.SetActive(active);
    }
}

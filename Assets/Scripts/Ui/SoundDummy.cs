using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDummy : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<AudioSource>().clip != null)
        {
            GetComponent<AudioSource>().Play();

            if (GetComponent<AudioSource>().clip != null && !GetComponent<AudioSource>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}

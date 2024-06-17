using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public List<AudioClip> Bgms;
    public List<AudioClip> Effects;
    public List<AudioClip> Scenario;

    public AudioSource bgmSource;
    public AudioSource EffectsSource;

    public GameObject prefab;
    public Transform dummy;

    public float masterVolume;
    public float bgmVolume;
    public float effectVolume;

    public AudioClip GetBgmClip(string clipName)
    {
        for (int i = 0; i < Bgms.Count; i++)
        {
            if (Bgms[i].name == clipName)
            {
                return Bgms[i];
            }
        }
        return null;
    }

    public AudioClip GetEffectClip(string clipName)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            if (Effects[i].name == clipName)
            {
                return Effects[i];
            }
        }
        return null;
    }

    public AudioClip GetScenarioClip(string clipName)
    {
        for (int i = 0; i < Scenario.Count; i++)
        {
            if (Scenario[i].name == clipName)
            {
                return Scenario[i];
            }
        }
        return null;
    }

    public void PlayBgm(string clipName)
    {
        bgmSource.clip = GetBgmClip(clipName);
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void PlayEffect(string clipName)
    {
        EffectsSource.clip = GetEffectClip(clipName);
        EffectsSource.Play();
    }

    public void PlayScenarioBgm(string clipName)
    {
        bgmSource.clip = GetScenarioClip(clipName);
        bgmSource.Play();
    }

    public void PlayScenarioEffect(string clipName)
    {
        EffectsSource.clip = GetScenarioClip(clipName);
        EffectsSource.Play();
    }

    public void SetMasterVolume(float vol)
    {
        mixer.SetFloat("Master", vol);

        masterVolume = vol;
        bgmSource.volume = masterVolume * bgmVolume;
        EffectsSource.volume = masterVolume * bgmVolume;
    }

    public void SetBgmVolume(float vol)
    {
        mixer.SetFloat("Bgm", vol);
        bgmVolume = masterVolume * bgmVolume;
        bgmSource.volume = vol;
    }

    public void SetEffectVolume(float vol)
    {
        mixer.SetFloat("Effect", vol);
        effectVolume = masterVolume * bgmVolume;
        EffectsSource.volume = vol;
    }
}

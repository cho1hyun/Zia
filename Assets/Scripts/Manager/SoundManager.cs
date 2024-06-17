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

    Coroutine bgmcoru;

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

    public AudioClip GetBgmClip(int clipName)
    {
        for (int i = 0; i < Bgms.Count; i++)
        {
            if (i == clipName)
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

    public AudioClip GetEffectClip(int clipName)
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            if (i == clipName)
            {
                return Bgms[i];
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

    public AudioClip GetScenarioClip(int clipName)
    {
        for (int i = 0; i < Scenario.Count; i++)
        {
            if (i == clipName)
            {
                return Bgms[i];
            }
        }
        return null;
    }

    public void PlayBgm(string clipName)
    {
        if (bgmcoru != null)
            bgmcoru = null;

        bgmcoru = StartCoroutine(BgmCo(clipName));
    }

    public void PlayBgm(int clipName)
    {
        if (bgmcoru != null)
            bgmcoru = null;

        bgmcoru = StartCoroutine(BgmCo(clipName));
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

        bgmSource.volume = bgmVolume * vol;
        EffectsSource.volume = effectVolume * vol;
    }

    public void SetBgmVolume(float vol)
    {
        mixer.SetFloat("Bgm", vol);
        bgmVolume = vol;

        bgmSource.volume = masterVolume * vol;
    }

    public void SetEffectVolume(float vol)
    {
        mixer.SetFloat("Effect", vol);
        effectVolume = vol;

        EffectsSource.volume = masterVolume * vol;
    }

    IEnumerator BgmCo(string clip)
    {
        bgmSource.Stop();
        bgmSource.clip = GetBgmClip(clip);
        while (!bgmSource.isPlaying)
        {
            bgmSource.Play();
            yield return null;
        }
    }
    IEnumerator BgmCo(int clip)
    {
        bgmSource.Stop();
        bgmSource.clip = GetBgmClip(clip);
        while (!bgmSource.isPlaying)
        {
            bgmSource.Play();
            yield return null;
        }
    }
}

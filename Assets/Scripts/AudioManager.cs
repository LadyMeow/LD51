using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static float Volume;

    public AudioSource MusicGame;
    public AudioSource MusicIntro;
    public AudioSource CollectSound;
    public AudioSource NewHighscoreSound;

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        Volume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        MusicGame.volume = Volume;
        CollectSound.volume = Volume;

        DontDestroyOnLoad(((this.gameObject)));
    }

    public void SetMasterVolume(float volume)
    {
        Volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        MusicGame.volume = Volume;
        CollectSound.volume = Volume;
    }

    public void SwitchToIntroMode()
    {
        StartCoroutine(FadeOut(MusicGame, 0.7f));

        StartCoroutine(FadeIn(MusicIntro, 5f));
    }

    public void SwitchToGameMode()
    {
        StartCoroutine(FadeOut(MusicIntro, 0.7f));

        StartCoroutine(FadeIn(MusicGame, 5f));
    }

    public static IEnumerator FadeOut(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }


    public static IEnumerator FadeIn(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;

        source.volume = 0;
        source.Play();

        while (source.volume < 1.0f)
        {
            source.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        source.volume = 1f;
    }

    public void playCollect()
    {
        CollectSound.Play();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip startMenuMusic;
    [SerializeField] AudioClip mainTrackMusic;
    [SerializeField] AudioClip gameOverMusic;

    AudioSource audioSource;
    AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayStartMenuMusic()
    {
        PlayMusic(startMenuMusic);
        audioSource.volume = 0.3f;
    }

    public void PlayMainTrackMusic()
    {
        PlayMusic(mainTrackMusic);
        AudioSource.volume = 1f;
    }

    public void PlayGameOverMusic()
    {        
        PlayMusic(gameOverMusic);
        AudioSource.loop = false;
    }

    void PlayMusic(AudioClip clip)
    {
        AudioUtility.PlayMusic(AudioSource, clip);
    }
    public void PauseMusic()
    {
        AudioSource.Pause();
    }
    public void UnpauseMusic()
    {
        AudioSource.UnPause();
    }

    public void StopMusic()
    {
        AudioSource.Stop();
    }

}

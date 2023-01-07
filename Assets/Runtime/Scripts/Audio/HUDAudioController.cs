using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HUDAudioController : MonoBehaviour
{
    AudioSource audioSource;
    AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    [SerializeField] AudioClip buttonAudio;
    [SerializeField] AudioClip countdownAudio;
    [SerializeField] AudioClip countdownEndAudio;

    void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }

    public void PlayButtonAudio()
    {
        Play(buttonAudio);
    }
    public void PlayCountdownAudio()
    {
        Play(countdownAudio);
    }
    public void PlayCountdownEndAudio()
    {
        Play(countdownEndAudio);
    }
}

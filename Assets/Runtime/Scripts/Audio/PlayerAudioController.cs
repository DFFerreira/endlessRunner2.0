using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] AudioClip rollSound;
    [SerializeField] AudioClip jumpSound;

    AudioSource audioSource;

    AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }

    public void PlayRollSound()
    {
        Play(rollSound);
    }

    public void PlayJumpSound()
    {
        Play(jumpSound);
    }

}

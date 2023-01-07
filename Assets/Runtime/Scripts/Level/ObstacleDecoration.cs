using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] AudioClip collAudio;
    [SerializeField] Animation animation;

    AudioSource audioSource;
    AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    void Awake()
    {
        animation.playAutomatically = false;
    }

    public void PlayCollisionFeedback()
    {
        AudioUtility.PlayAudioCue(AudioSource, collAudio);
        if(animation!=null)
        {
            animation.Play();
        }
    }
}

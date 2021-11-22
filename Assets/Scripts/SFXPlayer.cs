using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources = null;

    [SerializeField]
    private float volume = 0.0f;

    private int audioSourceIndex;

    private static SFXPlayer instance;

    public static SFXPlayer Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(AudioClip audioClip, float pitch)
    {
        AudioSource audioSource = audioSources[audioSourceIndex];
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip, volume);

        audioSourceIndex = (audioSourceIndex + 1) % audioSources.Length;
    }
}

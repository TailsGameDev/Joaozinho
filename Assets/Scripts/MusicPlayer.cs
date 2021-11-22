using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip[] musics = null;

    private int musicIndex;

    private void Awake()
    {
        musicIndex = Random.Range(0, musics.Length);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(musics[musicIndex]);
            musicIndex = (musicIndex + 1) % musics.Length;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip[] musics = null;

    private bool initialized;

    private int musicIndex;

    public bool Initialized { set => initialized = value; }

    private void Update()
    {
        if (initialized && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(musics[musicIndex]);
            musicIndex = (musicIndex + 1) % musics.Length;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private enum State
    {
        PAUSED,
        PLAYING,
        WAITING_TO_PAUSE,
    }

    [SerializeField] private AudioClip introAudio = null;
    [SerializeField] private GameObject videoNode = null;
    [SerializeField] private Image introVideoImage = null;
    [SerializeField] private Sprite[] introVideoSprites = null;
    [SerializeField] private float timeIntervalToChangeSprite = 0.0f;
    private float timeToChangeSprite;
    private bool isVideoPaused;
    private AudioSource audioSource;

    [SerializeField] private GameObject pauseMenuNode = null;
    [SerializeField] private MusicPlayer musicPlayer = null;
    [SerializeField] private SpawnManager spawnManager = null;
    [SerializeField] private float waitTimeFromDeathToPause = 0.0f;
    private State currentState;
    private float timeToPause;

    private IEnumerator Start()
    {
        this.audioSource = SFXPlayer.Instance.PlaySFXExclusiveAudioSource(introAudio);

        // Pause The Game
        currentState = State.PAUSED;
        spawnManager.SetEnable(false);
        Player.Instance.SetTheActive(false);
        pauseMenuNode.SetActive(true);

        while (!audioSource.isPlaying)
        {
            yield return null;
        }

        float timeToEndVideo = Time.time + introAudio.length;
        while (Time.time < timeToEndVideo && !Input.GetKeyDown(KeyCode.Tab))
        {
            yield return null;
        }

        // Play The Game
        Destroy(audioSource);
        videoNode.SetActive(false);
        musicPlayer.Initialized = true;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.PLAYING:
                spawnManager.SetEnable(true);
                Player.Instance.SetTheActive(true);

                if (Player.IsDead)
                {
                    timeToPause = Time.time + waitTimeFromDeathToPause;
                    currentState = State.WAITING_TO_PAUSE;
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    currentState = State.PAUSED;
                    pauseMenuNode.SetActive(true);
                }
                break;
            case State.WAITING_TO_PAUSE:
                if (Time.time > timeToPause)
                {
                    pauseMenuNode.SetActive(true);
                    currentState = State.PAUSED;
                }
                break;
            case State.PAUSED:
                if (Time.time > timeToChangeSprite)
                {
                    timeToChangeSprite = Time.time + timeIntervalToChangeSprite;
                    introVideoImage.sprite = introVideoSprites[Random.Range(0, introVideoSprites.Length)];
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    pauseMenuNode.SetActive(false);
                    currentState = State.PLAYING;
                }
                break;
        }
    }

    public void OnPlayPauseVideoButtonClicked()
    {
        isVideoPaused = !isVideoPaused;
        if (isVideoPaused)
        {
            this.audioSource.Pause();
        }
        else
        {
            this.audioSource.Play();
        }
    }
}
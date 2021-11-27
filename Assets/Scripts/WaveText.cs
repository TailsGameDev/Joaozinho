using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
{
    [SerializeField] private Text waveText = null;

    [SerializeField] private Text bigWaveText = null;

    [SerializeField] private float animationTimeIntervalForBigWaveText = 0.0f;

    private float timeToHideBigWaveText;

    private void Start()
    {
        SpawnManager.Instance.RegisterOnWaveChangedAction(UpdateWave);
    }
    private void UpdateWave()
    {
        waveText.text = "Onda " + SpawnManager.Instance.CurrentWaveIndex;
        bigWaveText.text = "Onda " + SpawnManager.Instance.CurrentWaveIndex;
        timeToHideBigWaveText = Time.time + animationTimeIntervalForBigWaveText;
        bigWaveText.enabled = true;
    }

    private void Update()
    {
        if (Time.time > timeToHideBigWaveText)
        {
            bigWaveText.enabled = false;
        }
    }
}

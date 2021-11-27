using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
{
    [SerializeField] private Text waveText = null;

    private void Start()
    {
        SpawnManager.Instance.RegisterOnWaveChangedAction(UpdateWave);
    }
    private void UpdateWave()
    {
        waveText.text = "Onda " + SpawnManager.Instance.CurrentWaveIndex;
    }
}

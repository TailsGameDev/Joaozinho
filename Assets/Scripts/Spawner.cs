using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint = null;

    private TransformWrapper spawnPointWrapper;

    public TransformWrapper SpawnPoint { get => spawnPointWrapper; }

    private void Start()
    {
        spawnPointWrapper = new TransformWrapper(spawnPoint);

        SpawnManager.Instance.AddSpawner(this);
    }
}

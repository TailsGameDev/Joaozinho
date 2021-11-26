using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint = null;

    private Enemy enemy;

    private TransformWrapper spawnPointWrapper;

    public TransformWrapper SpawnPoint { get => spawnPointWrapper; }
    public Enemy Enemy { get => enemy; set => enemy = value; }

    private void Start()
    {
        spawnPointWrapper = new TransformWrapper(spawnPoint);

        SpawnManager.Instance.AddSpawner(this);
    }
}

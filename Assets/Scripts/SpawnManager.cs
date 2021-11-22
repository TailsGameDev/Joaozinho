using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<Spawner> spawners;

    private static SpawnManager instance;

    [SerializeField]
    private float minTimeBetweenSpawns = 0.0f;
    [SerializeField]
    private float maxTimeBetweenSpawns = 0.0f;

    private float timeToSpawn = 0.0f;

    [SerializeField]
    private Damageable prototypeToSpawn = null;

    private List<Damageable> spawnedDamageables = null;


    public static SpawnManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;

        spawners = new List<Spawner>();
        spawnedDamageables = new List<Damageable>();
    }

    public void AddSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
        spawnedDamageables.Add(null);
    }

    private void Update()
    {
        if (Time.time > timeToSpawn)
        {
            int randomIndex = Random.Range(0, spawners.Count);
            spawnedDamageables[randomIndex] = Instantiate(prototypeToSpawn, 
                spawners[randomIndex].SpawnPoint.Position, Quaternion.identity);

            timeToSpawn = Time.time + Random.Range
                (minTimeBetweenSpawns, maxTimeBetweenSpawns);
        }
    }
}

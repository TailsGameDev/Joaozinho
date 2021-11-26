using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    private struct WaveData
    {
        [SerializeField] private List<Enemy> enemiesToClone;
        [SerializeField] private float minTimeBetweenSpawns;
        [SerializeField] private float maxTimeBetweenSpawns;

        public List<Enemy> EnemiesToSpawn { get => enemiesToClone; }
        public float MinTimeBetweenSpawns { get => minTimeBetweenSpawns; }
        public float MaxTimeBetweenSpawns { get => maxTimeBetweenSpawns; }
    }
    [SerializeField] private WaveData[] waves = null;
    private int currentWaveIndex;

    [SerializeField] private Text waveText = null;

    private List<Spawner> spawners = new List<Spawner>();

    private static SpawnManager instance;

    private float timeToSpawn = 0.0f;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    private static System.Action onGameWin;

    public static SpawnManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    public void AddSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
    }

    public static void RegisterOnGameWinAction(System.Action action)
    {
        onGameWin += action;
    }

    private void Update()
    {
        if (currentWaveIndex < waves.Length)
        {
            WaveData currentWaveData = waves[currentWaveIndex];

            if (Time.time > timeToSpawn)
            {
                // Instantiate prototype and remove it from list if needed
                if (currentWaveData.EnemiesToSpawn.Count > 0)
                {
                    Enemy enemyToClone = currentWaveData.EnemiesToSpawn[0];
                    currentWaveData.EnemiesToSpawn.RemoveAt(0);

                    int index = GetRandomFreeIndex(max: spawners.Count);
                    Vector3 spawnPosition =
                        spawners[index].SpawnPoint.Position;

                    Enemy enemy =
                        Instantiate(enemyToClone, spawnPosition, Quaternion.identity);
                    aliveEnemies.Add(enemy);
                    spawners[index].Enemy = enemy;
                }

                // Set new timeToSpawn based on cooldown from currentWaveData
                float cooldownToSpawn = Random.Range
                    (currentWaveData.MinTimeBetweenSpawns, currentWaveData.MaxTimeBetweenSpawns);
                timeToSpawn = Time.time + cooldownToSpawn;
            }

            bool allEnemiesOfThisWaveGotKilled =
                aliveEnemies.Count == 0 && currentWaveData.EnemiesToSpawn.Count == 0;
            if (allEnemiesOfThisWaveGotKilled)
            {
                // Start next wave
                currentWaveIndex++;

                waveText.text = currentWaveIndex.ToString();
            }
            else
            {
                for (int e = aliveEnemies.Count - 1; e >= 0; e--)
                {
                    Enemy enemy = aliveEnemies[e];
                    bool enemyIsDead = enemy == null;
                    if (enemyIsDead)
                    {
                        aliveEnemies.RemoveAt(e);
                    }
                }
            }
        }
        else
        {
            onGameWin?.Invoke();
            this.enabled = false;
        }
    }

    private int GetRandomFreeIndex(int max)
    {
        int randomAttempt = Random.Range(minInclusive: 0, maxExclusive: max);

        bool isOccupied = spawners[randomAttempt].Enemy != null;
        if (isOccupied)
        {
            randomAttempt = GetRandomFreeIndex(max);
        }

        return randomAttempt;
    }
}

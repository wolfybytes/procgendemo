using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public delegate void OnStateChange();
    public OnStateChange onWaveEnd;
    public OnStateChange onLevelEnd;

    private List<GameObject> spawnedEnemies;
    private int numEnemiesRemaining;

    public int seed;
    public Vector2 spawnBorders = new Vector2(-11, 11);
    public int numWaves = 4;
    public int currentWave = 0;
    public List<GameObject> enemyTypes;
    public List<Wave> waves;

    public void GenerateFromSeed(int seed)
    {
        this.seed = seed;
        GenerateWaves();
        SpawnWave();
    }

    private void Start()
    {
        spawnedEnemies = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            SpawnWave();
        if (Input.GetKeyDown(KeyCode.K))
            GenerateWaves();
    }

    private void GenerateWaves()
    {
        Random.seed = seed;

        currentWave = 0;
        waves = new List<Wave>();

        for (int i = 0; i < numWaves; i++)
        {
            waves.Add(new Wave(GenerateSpawns()));
        }    
    }

    private List<EnemySpawn> GenerateSpawns()
    {
        List<EnemySpawn> newSpawns = new List<EnemySpawn>();
        int numEnemies = Random.Range(3, 6);
        for (int i = 0; i < numEnemies; i++)
        {
            newSpawns.Add(new EnemySpawn(GetRandomEnemy(), GetRandomSpawnLocation()));
        }
        return newSpawns;
    }

    private GameObject GetRandomEnemy()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Count)];
    }

    private Vector3 GetRandomSpawnLocation()
    {
        Vector3 spawnPosition = new Vector3();
        int side = Random.Range(0, 3);
        if (side == 0)
            spawnPosition = new Vector3(spawnBorders.x, 0.5f, Random.Range(spawnBorders.x, spawnBorders.y));
        else if (side == 1)
            spawnPosition = new Vector3(spawnBorders.y, 0.5f, Random.Range(spawnBorders.x, spawnBorders.y));
        else if (side == 2)
            spawnPosition = new Vector3(Random.Range(spawnBorders.x, spawnBorders.y), 0.5f, spawnBorders.x);
        else
            spawnPosition = new Vector3(Random.Range(spawnBorders.x, spawnBorders.y), 0.5f, spawnBorders.y);
        return spawnPosition;
    }

    public void SpawnWave()
    {
        if (spawnedEnemies == null)
            spawnedEnemies = new List<GameObject>();
        foreach (GameObject o in spawnedEnemies)
            Destroy(o);
        spawnedEnemies = new List<GameObject>();

        foreach(EnemySpawn spawn in waves[currentWave].spawns)
        {
            GameObject newEnemy = Instantiate(spawn.enemy, spawn.spawnLocation, new Quaternion(0f, 0f, 0f, 0f), transform);
            newEnemy.GetComponent<Enemy>().onDeath += CheckIfWaveEnded;
            spawnedEnemies.Add(newEnemy);
        }
        numEnemiesRemaining = spawnedEnemies.Count;

        currentWave += 1;
    }

    public void CheckIfWaveEnded()
    {
        numEnemiesRemaining -= 1;
        if (numEnemiesRemaining <= 0 && currentWave + 1 > numWaves)
        {
            currentWave = 0;
            onLevelEnd?.Invoke();
        }
        else if (numEnemiesRemaining <= 0)
        {
            onWaveEnd?.Invoke();
        }
    }
}

[System.Serializable]
public class Wave
{
    public List<EnemySpawn> spawns;

    public Wave(List<EnemySpawn> spawns)
    {
        this.spawns = spawns;
    }
}

[System.Serializable]
public class EnemySpawn
{
    public GameObject enemy;
    public Vector3 spawnLocation;

    public EnemySpawn(GameObject enemy, Vector3 spawnLocation)
    {
        this.enemy = enemy;
        this.spawnLocation = spawnLocation;
    }
}

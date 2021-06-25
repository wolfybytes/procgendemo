using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GenerateEnvironment environment;
    private WaveSpawner spawner;

    public int mainSeed;

    private void Start()
    {
        Init();

        StartNewGame();
    }

    private void Init()
    {
        environment = GameObject.FindObjectOfType<GenerateEnvironment>();
        spawner = GameObject.FindObjectOfType<WaveSpawner>();

        spawner.onWaveEnd += spawner.SpawnWave;
        spawner.onLevelEnd += StartNewGame;
    }

    private void StartNewGame()
    {
        //mainSeed = Random.Range(0, 99999);

        environment.GenerateFromSeed(mainSeed);
        spawner.GenerateFromSeed(mainSeed);
    }
}

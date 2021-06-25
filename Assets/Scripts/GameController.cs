using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GenerateEnvironment environment;
    private WaveSpawner spawner;

    public GameObject endScreen;
    public int mainSeed;

    private void Start()
    {
        Init();

        StartNewGame();
    }

    private void Init()
    {
        endScreen.SetActive(false);
        environment = GameObject.FindObjectOfType<GenerateEnvironment>();
        spawner = GameObject.FindObjectOfType<WaveSpawner>();

        spawner.onWaveEnd += spawner.SpawnWave;
        spawner.onLevelEnd += EnableEndScreen;//StartNewGame;
    }

    private void StartNewGame()
    {
        mainSeed = GlobalGameData.instance.GetGlobalSeed(); //Random.Range(0, 99999);

        environment.GenerateFromSeed(mainSeed);
        spawner.GenerateFromSeed(mainSeed);
    }

    private void EnableEndScreen()
    {
        endScreen.SetActive(true);
    }
}

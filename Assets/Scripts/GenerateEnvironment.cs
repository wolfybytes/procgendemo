using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    private int numObjects;
    private int numDetails;

    public int seed;
    public Vector2 spawnRange = new Vector2(-10f, 10f);
    public List<Biome> biomes;
    public List<GameObject> environment;
    public Material onLevelMat;
    public Material offLevelMat;

    public void GenerateFromSeed(int seed)
    {
        this.seed = seed;

        GenerateLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            GenerateLevel();
        if (Input.GetKeyDown(KeyCode.H))
            GenerateSeed();
    }

    public void GenerateLevel()
    {
        Random.seed = seed;
        foreach (GameObject o in environment)
            Destroy(o);

        environment = new List<GameObject>();

        numObjects = Random.Range(3, 6);
        numDetails = Random.Range(6, 10);
        Biome currentBiome = biomes[Random.Range(0, biomes.Count)];
        for (int i = 0; i < numObjects; i++)
        {
            GameObject newObj = Instantiate(currentBiome.GetRandomObject(), new Vector3(Random.Range(spawnRange.x, spawnRange.y), 0f, Random.Range(spawnRange.x, spawnRange.y)), new Quaternion(0f, Random.Range(0f, 1f), 0f, 0f), gameObject.transform);
            newObj.transform.localScale *= Random.Range(1f, 2f);
            environment.Add(newObj);
        }
        for (int i = 0; i < numDetails; i++)
        {
            GameObject newObj = Instantiate(currentBiome.GetRandomDetail(), new Vector3(Random.Range(spawnRange.x, spawnRange.y), 0f, Random.Range(spawnRange.x, spawnRange.y)), new Quaternion(0f, Random.Range(0f, 1f), 0f, 0f), gameObject.transform);
            newObj.transform.localScale *= Random.Range(1f, 1.5f);
            environment.Add(newObj);
        }
        onLevelMat.color = currentBiome.onLevelColor;
        onLevelMat.SetColor("_EmissionColor", currentBiome.onLevelColor);
        offLevelMat.color = currentBiome.offLevelColor;
        offLevelMat.SetColor("_EmissionColor", currentBiome.offLevelColor);
    }

    public void GenerateSeed()
    {
        seed = Random.Range(0, 99999);
    }
}

[System.Serializable]
public class Biome
{
    public string name;
    public List<GameObject> objs;
    public List<GameObject> details;
    public Color onLevelColor;
    public Color offLevelColor;

    public GameObject GetRandomObject()
    {
        return objs[Random.Range(0, objs.Count)];
    }

    public GameObject GetRandomDetail()
    {
        return details[Random.Range(0, details.Count)];
    }
}

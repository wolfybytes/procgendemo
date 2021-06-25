using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameData : MonoBehaviour
{
    public static GlobalGameData instance;

    public int globalSeed;

    private void Start()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
        DontDestroyOnLoad(this);
    }

    public int GetGlobalSeed()
    {
        return globalSeed;
    }

    public void SetGlobalSeed(int seed)
    {
        globalSeed = seed;
    }
}

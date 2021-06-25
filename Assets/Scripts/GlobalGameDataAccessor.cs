using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameDataAccessor : MonoBehaviour
{
    public void SetGlobalSeed(int seed)
    {
        GlobalGameData.instance.SetGlobalSeed(seed);
    }
}

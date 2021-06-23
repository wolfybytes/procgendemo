using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStringSeed : MonoBehaviour
{
    private int num = 6;

    public int seed;
    public int[] values;

    void Start()
    {
        Random.seed = seed;
        values = new int[num];

        for(int i = 0; i < num; i++)
        {
            values[i] = Random.Range(0, 9);
        }
    }
}

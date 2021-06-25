using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerUtil : MonoBehaviour
{
    public string gameLevel;

    public void LoadGameLevel()
    {
        SceneManager.LoadScene(gameLevel);
    }
}

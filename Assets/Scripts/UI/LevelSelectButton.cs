using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    // The name of the scene to load.
    public string levelToLoad;

    // Load the level.
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This is the level selection scene.
    public string levelSelectionScene;

    // Start a new game by directing them to their level selection scene.
    public void NewGame()
    {
        SceneManager.LoadScene(levelSelectionScene);
    }

    // Exit the game.
    public void QuitGame()
    {
        Application.Quit();
    }
}

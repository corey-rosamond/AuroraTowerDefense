using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public int initialAmountOfGold = 200;

    public List<Castle> castles = new List<Castle>();

    private bool gameInProgress = true;

    private void Awake()
    {
        instance = this;
        GoldManager.Give(initialAmountOfGold);
    }

    public void Update()
    {

    }

    public static void RemoveCastle(Castle castleToRemove)
    {
        instance.castles.Remove(castleToRemove);
        if (instance.castles.Count == 0)
        {
            Debug.Log("Game Over");
        }
    }

    public static bool IsGameInProgress()
    {
        return instance.gameInProgress;
    }
}

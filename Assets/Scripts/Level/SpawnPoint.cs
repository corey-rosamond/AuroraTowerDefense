using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // The position where we will spawn the enemy.
    public GameObject point;

    // The castle enemies spawned at this point should target.
    public Castle targetCastle;
}

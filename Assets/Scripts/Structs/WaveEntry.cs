using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveEntry
{
    // The enemy prefab to spawn
    public GameObject enemyPrefab;
    // This is the spawn point where we will spawn the enemies.
    public SpawnPoint spawnPoint;
    // This is the initial castle you want the enemies to move towards.
    public Castle castle;
    // This is the amount of enemies to spawn in total.
    public int amountToSpawn;
    // This is the rate at which to spawn the enemies
    public float spawnRate;
    // This is the amount of enemies that have been spawned.
    public int amountSpawned = 0;
    // this is the amount of time until the next spawn.
    public float? timeUntilNextSpawn = null;
    // The time to wait until starting to spawn.
    public float delayUntilStart = 0;
    // The time left until we start spawning
    public float? timeUntilStartSpawning = null;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The possible states this object can exist in.
    public enum State
    {
        countdown,
        spawning,
        complete
    }
    // The singleton instance of this object.
    public static SpawnManager instance;
    // The current state of the spawn manager.
    public State state = State.countdown;
    // List of all enemies active on the map currently.
    public List<EnemyController> activeEnemies = new List<EnemyController>();
    // The index of the wave we are currently working on.
    public int currentWaveIndex = 0;
    // This is a collection of all the waves that will be spawned in this level.
    public List<Wave> waves = new List<Wave>();

    // This is called just before start and will initialize our instance variable.
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Only do something in the update if the game is in progress.
        if(LevelManager.IsGameInProgress() && instance.state != State.complete)
        {
            // Pass the update call to the correct update method based on the object state.
            switch (instance.state)
            {
                // Do the countdown update.
                case State.countdown:
                    instance.UpdateCountdown(); break;
                // Do the spawning update
                case State.spawning:
                    instance.UpdateSpawning(); break;
            }
        }
    }

    // The update method that is used when this object is in the countdown state.
    private void UpdateCountdown()
    {
        // Get a reference to the current wave we are working with.
        Wave currentWave = waves[currentWaveIndex];
        // Check if our waves timeUntilStart is null
        if (currentWave.timeUntilWaveStarts == null)
        {
            // We have not initialized it before so initialize it to the timeBeforeWave value.
            currentWave.timeUntilWaveStarts = currentWave.timeBeforeWave;
        }
        else
        {
            // Subtract delta time from the timeUntilWaveStarts
            currentWave.timeUntilWaveStarts -= Time.deltaTime;
            // Check if it is time to start spawning.
            if(currentWave.timeUntilWaveStarts <= 0)
            {
                // Set our state to spawning.
                state = State.spawning;
            }
        }
    }

    // The update method used when this object is in the spawning state.
    private void UpdateSpawning()
    {
        // Assume we are done spawning this wave unless told otherwise.
        bool doneSpawning = true;
        // A reference to the wave we are currently working on.
        Wave currentWave = waves[currentWaveIndex];
        // Iterate through the wave entries
        foreach (WaveEntry currentWaveEntry in currentWave.entries)
        {
            // Check if this has been initialized
            if (currentWaveEntry.timeUntilNextSpawn == null)
            {
                // Was never initialized so set it to the spawnRate.
                currentWaveEntry.timeUntilNextSpawn = currentWaveEntry.spawnRate;
                // Set done spawning to false as we have not even started.
                doneSpawning = false;
            }
            else
            {
                // Check if there are still enemies left to be spawned in this entry.
                if (currentWaveEntry.amountSpawned < currentWaveEntry.amountToSpawn)
                {
                    // Set done spawning as there are still enemies left to spawn.
                    doneSpawning = false;
                    // Subtract delta time from the timeUntilNextSpawn.
                    currentWaveEntry.timeUntilNextSpawn -= Time.deltaTime;
                    // Check if it is time to spawn an enemy.
                    if (currentWaveEntry.timeUntilNextSpawn <= 0)
                    {
                        // Instantiate the new enemy and save it to a game object
                        GameObject enemy = Instantiate(
                            currentWaveEntry.enemyPrefab,
                            currentWaveEntry.spawnPoint.point.transform.position,
                            currentWaveEntry.spawnPoint.point.transform.rotation
                        );
                        // Get the enemy controller
                        EnemyController enemyController = enemy.GetComponent<EnemyController>();
                        // Run the enemies setup method
                        enemyController.Setup(currentWaveEntry.castle);
                        // Add the enemy to the active enemies collection
                        activeEnemies.Add(enemyController);
                        // Increment the amountSpawned
                        currentWaveEntry.amountSpawned++;
                        // Reset the timeUntilNextSpawn back to the spawnRate
                        currentWaveEntry.timeUntilNextSpawn = currentWaveEntry.spawnRate;
                    }
                }
            }
            // Check if we are done spawning this wave.
            if(doneSpawning)
            {
                // Increment the currentWaveIndex
                currentWaveIndex++;
                // Check if there is a next wave.
                if(waves.Count <= currentWaveIndex)
                {
                    // There is not another wave to spawn so move the object to the complete state.
                    state = State.complete;
                }
                else
                {
                    // There is another wave to do so start on its countdown.
                    state = State.countdown;
                }
            }
        }
    }
}

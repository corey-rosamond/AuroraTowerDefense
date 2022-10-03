using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave
{
    // The amount of time to wait before starting to spawn the wave.
    public float timeBeforeWave = 5;
    // Time before the wave starts spawning.
    public float? timeUntilWaveStarts = null;
    // List of wave entries associated with this wave.
    public List<WaveEntry> entries;
}

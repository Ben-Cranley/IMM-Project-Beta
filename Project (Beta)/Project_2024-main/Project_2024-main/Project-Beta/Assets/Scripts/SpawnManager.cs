using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The 3 car prefabs to choose from
    public GameObject[] carPrefabs;

    // Positions along the X-axis where cars can spawn
    private float[] spawnPositionsX = new float[] { -7.25f, -5.0f, -3.0f, 2.5f, 6.5f, 7.5f, 15.5f, 15.0f, 17.0f, 22.5f, 25.5f, 27.0f };

    // Constant Z-axis position where the cars will spawn
    private float spawnPosZ = 100.0f;

    // Interval between each spawn (in seconds)
    public float spawnInterval;

    // Start spawning cars when the game starts
    public void StartSpawning()
    {
        Debug.Log("Started spawning cars");
        // Start calling SpawnRandomCar repeatedly at the defined spawnInterval
        InvokeRepeating(nameof(SpawnRandomCar), 0f, spawnInterval);
    }

    // Stop spawning cars when the game ends
    public void StopSpawning()
    {
        Debug.Log("Stopped spawning cars");
        // Stop the car spawning
        CancelInvoke(nameof(SpawnRandomCar));
    }

    // Spawns a random car at a random position
    private void SpawnRandomCar()
    {
        // Randomly select an X position from the spawn positions
        int randomIndexX = Random.Range(0, spawnPositionsX.Length);
        float spawnPosX = spawnPositionsX[randomIndexX];

        // Create a spawn position using the random X position and constant Z position
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        // Randomly choose a car prefab from the array of car prefabs
        int carIndex = Random.Range(0, carPrefabs.Length);
        GameObject spawnedCar = Instantiate(carPrefabs[carIndex], spawnPos, carPrefabs[carIndex].transform.rotation);

        // Tag the spawned car for collision detection (optional but good practice)
        spawnedCar.tag = "Car";
    }
}

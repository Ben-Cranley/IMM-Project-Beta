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
    private GamesManager gamesManager; // Reference to GamesManager
    private DifficultyButton difficultyButton; // Reference to GamesManager
    private int difficulty;

    // Start spawning cars when the game starts
public void StartSpawning()
{
    gamesManager = FindObjectOfType<GamesManager>();

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
    public void SpawnRandomCar()
    {
        // Randomly select an X position from the spawn positions
        int randomIndexX = Random.Range(0, spawnPositionsX.Length);
        float spawnPosX = spawnPositionsX[randomIndexX];

        // Create a spawn position using the random X position and constant Z position
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        // Randomly choose a car prefab from the array of car prefabs
        int carIndex = Random.Range(0, carPrefabs.Length);
        GameObject spawnedCar = Instantiate(carPrefabs[carIndex], spawnPos, carPrefabs[carIndex].transform.rotation);

        // Tag the spawned car for collision detection
        spawnedCar.tag = "Car";
    }

    // Update method for despawning cars
void Update()
{
    // Find all cars in the scene
    GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

    // Loop through each car using a for loop
    for (int i = 0; i < cars.Length; i++)
    {
        // Check if the car's Z position is less than or equal to 0
        if (cars[i].transform.position.z <= 0)
        {
            // Destroy the car
            Destroy(cars[i]);
            gamesManager.UpdateScore(1);
        }
    }
}

    // allows gamesmanager to pass the difficulty value
public void SetDifficulty(int newDifficulty)
{
    // Adjust spawn interval based on difficulty
    if (newDifficulty == 1)
    {
        spawnInterval = 1.5f; // Easier difficulty
    }
    else if (newDifficulty == 2)
    {
        spawnInterval = 1f; // Medium difficulty
    }
    else if (newDifficulty == 3)
    {
        spawnInterval = 0.5f; // Harder difficulty
    }
    else
    {
        spawnInterval = 1.5f; // Default to easy difficulty
    }

    // If spawning is already active, update spawn interval in real-time
    if (IsInvoking(nameof(SpawnRandomCar)))
    {
        CancelInvoke(nameof(SpawnRandomCar));
        InvokeRepeating(nameof(SpawnRandomCar), 0f, spawnInterval);
    }
}
}

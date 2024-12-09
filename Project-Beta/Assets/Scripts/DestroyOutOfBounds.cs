using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Define the boundary limit
    [SerializeField] private float lowerBound = 0; // The Z position below which the object will be destroyed   

    private GamesManager gamesManager; // Reference to GamesManager
    private AudioSource crashSound;
    public AudioClip carCrash; // You can assign a car crash sound in the Inspector

    void Start()
    {
        // Find the GamesManager in the scene
        gamesManager = FindObjectOfType<GamesManager>();

        // Get the crash sound audio source if it's part of the car prefab
        crashSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the object has passed the boundary and is tagged as "Car"
        if (transform.position.z < lowerBound && gameObject.CompareTag("Car") && gamesManager.isGameActive)
        {
            Debug.Log("Destroying car at position: " + transform.position);  // Log car destruction

            // Play crash sound if available
            if (crashSound != null && carCrash != null)
            {
                crashSound.PlayOneShot(carCrash, 1.0f);
            }

            Destroy(gameObject);  // Destroy the car object
            gamesManager.UpdateScore(5);  // Update the score
        }
    }
}

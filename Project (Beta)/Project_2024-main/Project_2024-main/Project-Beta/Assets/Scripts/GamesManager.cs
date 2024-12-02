using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamesManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;

    public GameObject titleScreen;

    public TextMeshProUGUI scoreText;
    private int score;

    public SpawnManager spawnManager;

    public bool isGameActive;

    private AudioSource crashSound;
    public AudioClip playerCrash;


    // Start is called before the first frame update
    void Start()
    {

    }

public void StartGame()
{
    // Set game to active and hide the title screen
    isGameActive = true;
    titleScreen.SetActive(false);

    // Reset score and other UI elements
    score = 0;
    crashSound = GetComponent<AudioSource>();
    scoreText.gameObject.SetActive(true);

    // Start car spawning after the delay
    spawnManager.StartSpawning();

}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

public void GameOver()
{
    Debug.Log("Game Over!");
    crashSound.PlayOneShot(playerCrash, 1.0f);
    Time.timeScale = 0;  
    gameOverText.gameObject.SetActive(true); 

}
}

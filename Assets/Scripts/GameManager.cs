using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs; //Can also use a list instead, eg. list<GameObject> enemyPrefabs; But lists might negatively affect performance more than arrays
    [SerializeField] GameObject powerupPrefab;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] GameObject titleScreen;
    AudioSource music;
    public bool isGameActive;
    int score;

    float spawnRangeXLeft = -7.5f;
    float spawnRangeXRight = 19.5f;
    float spawnPosY = 0.5f;
    float spawnRangeZ = 19.5f;
    float startDelay = 3.0f;

    void Start()
    {
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void SpawnRandomEnemy()
    //This function is an example of recursion (a function that calls itself)
    {
        if (isGameActive == true)
        {
            Vector3 spawnPos = new Vector3(Random.Range(spawnRangeXLeft, spawnRangeXRight), spawnPosY, spawnRangeZ);
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);

            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);

            float spawnInterval = Random.Range(1, 3);

            Invoke("SpawnRandomEnemy", spawnInterval);
        }
    }

    void SpawnPowerup()
    //TODO Learn how to use timers to remove the spanwed powerup after X seconds
    //Using a coroutine seemed like it would be possible, but I don't know how to remove only one instance of the powerup, rather than the entire powerup prefab
    {
        if (isGameActive == true)
        {
            Vector3 spawnPos = new Vector3(Random.Range(spawnRangeXLeft, spawnRangeXRight), spawnPosY, Random.Range(-spawnRangeZ, spawnRangeZ));

            Instantiate(powerupPrefab, spawnPos, powerupPrefab.transform.rotation);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void gameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        music.Play();
        titleScreen.gameObject.SetActive(false);
        startDelay /= difficulty; //TODO Difficult setting only affects the startDelay. It doesn't affect frequency of spawning, or enemy speed, etc
        score = 0;
        UpdateScore(0);
        Invoke("SpawnRandomEnemy", startDelay); //If I use InvokeRepeating for enemy instead, I won't be able to use Random.Range for the spawnInterval
        InvokeRepeating("SpawnPowerup", startDelay, 10);
    }
}
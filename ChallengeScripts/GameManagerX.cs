﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public List<GameObject> targetPrefabs;
    private float spawnRate = 1.5f;
    private int score;
    private float timer;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public bool isGameActive;

    public GameObject titleScreen;
    public Button restartButton;
    

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square
    
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked



    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        timer = 60;
        UpdateScore(0);
       }

    void Update() {
        if (isGameActive) {
            timer -= Time.deltaTime;
            timerText.SetText("Time: " + Mathf.Round(timer));
            if(timer < 0) {
                GameOver();
            }
        }
      }



// While game is active spawn a random target
IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
            
        }
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    
    
    

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
       // timerText.text = "0";
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

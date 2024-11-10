using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        int gameSessionCount = FindObjectsOfType<gameSession>().Length;
        if (gameSessionCount>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        FindObjectOfType<SceneSaves>().ResetSceneSaves();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }
    
    public int GetLives()
    {
        return playerLives;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetGameSessionData(int levelNumber, int livesCount, int newScore)
    {
        playerLives = livesCount;
        score = newScore;

        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();

        SceneManager.LoadScene(levelNumber);
    }

}

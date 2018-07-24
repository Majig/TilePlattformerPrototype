using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {
    // Config Fields
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    /// <summary>
    /// implementation of singleton pattern
    /// check if another object of GameSession exists
    /// if so: destroy; else: DontDestroyOnLoad 
    /// </summary>
    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
	}

    public void AddToScore (int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Behaviour for when player dies
    /// </summary>
    public void ProcessPlayerDeath ()
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

    /// <summary>
    /// Resets the session by loading Main Menu Level
    /// </summary>
    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    /// <summary>
    /// Reduces player lives and prints current count to console
    /// Reloads current scene
    /// </summary>
    private void TakeLife ()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

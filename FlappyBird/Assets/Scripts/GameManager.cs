using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;

    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;

    public Player player;

    private void Awake()
    {
        Application.targetFrameRate = 60; // lock fps

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for(int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

    }

    public void Pause()
    {
        Time.timeScale = 0f; // ea way to pause this game
        player.enabled = false;
    }


    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
        Debug.Log("Game Over");
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString(); // changing score by text component
        Debug.Log(score);
    }


}

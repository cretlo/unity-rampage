using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager gameManager;
  public TextMeshProUGUI timeText;
  public TextMeshProUGUI scoreText;
  private float seconds;
  private int totalScore;
  private bool endRun;
  void Awake()
  {
    seconds = 0;
    totalScore = 0;
    endRun = false;
    // Creates a single instance of the gameManager when the GameManager
    // obj is loaded
    gameManager = this;
  }

  // Update is called once per frame
  void Update()
  {
    if (!endRun)
    {
      seconds = Time.timeSinceLevelLoad;
      TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

      timeText.text = timeSpan.ToString(@"hh\:mm\:ss");
      scoreText.text = "Score: " + totalScore.ToString();

    }


  }

  public bool IsRunEnded()
  {
    return this.endRun;
  }

  public void EndRun()
  {
    endRun = true;
  }

  public void Restart()
  {
    if (IsRunEnded())
    {
      SceneManager.LoadScene("Main");

    }
  }
  public void AddPoints(int points)
  {
    totalScore += points;
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public static GameManager gameManager;
  public TextMeshProUGUI timeText;
  public TextMeshProUGUI scoreText;
  private float seconds;
  private int totalScore;
  void Awake()
  {
    seconds = 0;
    totalScore = 0;
    // Creates a single instance of the gameManager when the GameManager
    // obj is loaded
    gameManager = this;
  }

  // Update is called once per frame
  void Update()
  {
    seconds = Time.timeSinceLevelLoad;
    TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

    timeText.text = timeSpan.ToString(@"hh\:mm\:ss");
    scoreText.text = "Score: " + totalScore.ToString();


  }

  public void AddPoints(int points)
  {
    totalScore += points;
  }
}

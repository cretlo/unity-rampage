using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager gameManager;
  // public TextMeshProUGUI timeText;
  // public TextMeshProUGUI scoreText;
  private List<string> _leaderBoard;
  private string _time;
  private float _seconds;
  private int _totalScore;
  private bool _endRun;
  void Awake()
  {
    _seconds = 0;
    _totalScore = 0;
    _endRun = false;
    // Creates a single instance of the gameManager when the GameManager
    // obj is loaded
    gameManager = this;
  }

  // Update is called once per frame
  void Update()
  {
    if (!_endRun)
    {
      _seconds = Time.timeSinceLevelLoad;
      TimeSpan timeSpan = TimeSpan.FromSeconds(_seconds);
      _time = timeSpan.ToString(@"hh\:mm\:ss");

      // timeText.text = timeSpan.ToString(@"hh\:mm\:ss");
      // scoreText.text = "Score: " + totalScore.ToString();

    }


  }
  public int GetScore()
  {
    return this._totalScore;
  }

  public string GetTime()
  {
    return this._time;
  }

  public bool IsRunEnded()
  {
    return this._endRun;
  }

  public void EndRun()
  {
    _endRun = true;
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
    _totalScore += points;
  }

  public void SetLeaderboard(List<string> leaderboard)
  {
    this._leaderBoard = leaderboard;
  }

  public List<string> GetLeaderboard()
  {
    return this._leaderBoard;
  }
}

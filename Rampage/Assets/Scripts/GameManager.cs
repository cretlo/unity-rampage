using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // Event
  public delegate void GameOver(string username, string score, string time);
  public static event GameOver RunEnded;
  public static GameManager gameManager;
  private List<string> _leaderBoard;
  // TODO: Need to bring the username over from main menu
  private string _username;
  private string _time;
  private float _seconds;
  private int _totalScore;
  private bool _endRun;


  void Awake()
  {
    // TODO: Need to bring the username over from main menu
    _username = "Cretlo";
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
    }


  }
  public int GetScore()
  {
    return this._totalScore;
  }

  public string GetUsername()
  {
    return this._username;
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
    // Restart if the run is ended and return
    if (_endRun)
    {
      return;
    }

    _endRun = true;
    RunEnded(this._username, this._totalScore.ToString(), this._time);
  }

  public void Restart()
  {
    if (_endRun)
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

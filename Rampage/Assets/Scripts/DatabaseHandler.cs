using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;

public class DatabaseHandler : MonoBehaviour
{
  private DatabaseReference _dbReference;
  private string _userID;
  private Dictionary<string, Dictionary<string, string>> playerList { get; set; }
  private List<string> leaderboard;
  private GameManager _gameManager;
  private bool isPlayerSaved;
  // Start is called before the first frame update
  void Awake()
  {
    isPlayerSaved = false;
    _userID = System.Guid.NewGuid().ToString();
    _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    leaderboard = new List<string>();

  }

  void Start()
  {
    _gameManager = GameManager.gameManager;
    // SavePlayerStats();
  }

  // Update is called once per frame
  void Update()
  {

    // Save the players stats when the run is ended
    if (_gameManager.IsRunEnded() && !isPlayerSaved)
    {
      isPlayerSaved = true;
      SavePlayerStats();
    }

  }

  void GetPlayerStats()
  {
    FirebaseDatabase.DefaultInstance
            .GetReference("players")
            .ValueChanged += HandleValueChanged;

  }

  public void SavePlayerStats()
  {
    var player = new Dictionary<string, Dictionary<string, string>>();
    var playerStats = new Dictionary<string, string>() {
        {"name", "tim"},
        {"time", "00:00:00"}
    };
    player.Add(
        System.Guid.NewGuid().ToString(),
        playerStats
    );
    // Player player = new Player("Dale", "00:01:35", "250");
    string playerJson = JsonConvert.SerializeObject(player);
    _dbReference.Child("players").SetRawJsonValueAsync(playerJson);

    // Retreive the updated leaderboard
    GetPlayerStats();
  }

  void HandleValueChanged(object sender, ValueChangedEventArgs args)
  {
    if (args.DatabaseError != null)
    {
      Debug.LogError(args.DatabaseError.Message);
      return;
    }
    // Do something with the data in args.Snapshot
    string leaderboardJson = args.Snapshot.GetRawJsonValue();
    playerList = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(leaderboardJson);
    foreach (var player in playerList)
    {
      foreach (var playerInfo in player.Value)
      {
        leaderboard.Add(playerInfo.Value);
      }
    }
    _gameManager.SetLeaderboard(leaderboard);
  }
}


[System.Serializable]
class Player
{
  public Dictionary<string, Dictionary<string, string>> player;

  public Player(string username, string time, string score)
  {
    this.player = new Dictionary<string, Dictionary<string, string>>();
    var playerInfo = new Dictionary<string, string>() {
        {"username", username},
        {"time", time},
        {"score", score}
    };
    player.Add(System.Guid.NewGuid().ToString(), playerInfo);

  }

}

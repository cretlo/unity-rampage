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
  private GameManager _gameManager;
  private bool isPlayerSaved;

  // Event listeners
  public delegate void DatabaseAction(List<string> players);
  public static event DatabaseAction RetreivedData;
  void Awake()
  {
    isPlayerSaved = false;
    _userID = System.Guid.NewGuid().ToString();
    _dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    // Subscribe to the game manager for when events happen
    GameManager.RunEnded += SavePlayerStats;

  }

  void OnDisable()
  {
    GameManager.RunEnded -= SavePlayerStats;
  }

  // Start is called before the first frame update
  void Start()
  {
    // Needs to be called on the first frame so the static
    // variable can be initialized
    _gameManager = GameManager.gameManager;

    // Passes the HandleValueChanged function to the listener when something happens
    // on firebase's side
    FirebaseDatabase.DefaultInstance
            .GetReference("players").OrderByValue()
            .ValueChanged += HandleValueChanged;
  }

  // Update is called once per frame
  void Update()
  {

    // Save the players stats when the run is ended
    // if (_gameManager.IsRunEnded() && !isPlayerSaved)
    // {
    //   SavePlayerStats();
    //   isPlayerSaved = true;
    // }

  }

  void GetPlayerStats()
  {
    // FirebaseDatabase.DefaultInstance
    //         .GetReference("players")
    //         .ValueChanged += HandleValueChanged;

  }

  public async void SavePlayerStats(string username, string score, string time)
  {
    var player = new Dictionary<string, Dictionary<string, string>>();
    var playerStats = new Dictionary<string, string>() {
        {"name", username},
        {"score", score},
        {"time", time},
    };
    player.Add(
        System.Guid.NewGuid().ToString(),
        playerStats
    );

    string playerJson = JsonConvert.SerializeObject(playerStats);
    string guid = System.Guid.NewGuid().ToString();
    await _dbReference.Child("players").Child(guid).SetRawJsonValueAsync(playerJson);
  }

  void HandleValueChanged(object sender, ValueChangedEventArgs args)
  {
    List<string> leaderboard = new List<string>();

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
    // _gameManager.SetLeaderboard(leaderboard);

    // Event listener
    // Calls the menu's leaderboard handling function
    RetreivedData(leaderboard);
  }
}

// "player" end up becoming the root of the json conversion
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

using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;

public class DatabaseHandler : MonoBehaviour
{
  private DatabaseReference _dbReference;
  private string _userID;
  private Dictionary<string, Dictionary<string, string>> playerList { get; set; }
  private GameManager _gameManager;
  private bool isPlayerSaved;
  private List<Player> _players = new List<Player>();

  // Event 
  public delegate void DatabaseAction(List<Player> players);
  public static event DatabaseAction RetreivedData;
  void Awake()
  {
    isPlayerSaved = false;
    _userID = System.Guid.NewGuid().ToString();
    _dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    // Subscribe to the game manager for when events happen
    // GameManager.RunEnded += SavePlayerStats;

  }
  void OnEnable()
  {
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

  }

  public async void SavePlayerStats(string username, string score, string time)
  {
    var player = new Dictionary<string, Dictionary<string, string>>();
    var playerStats = new Dictionary<string, string>() {
        {"username", username},
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
    // Clear the list
    _players = new List<Player>();


    if (args.DatabaseError != null)
    {
      Debug.LogError(args.DatabaseError.Message);
      return;
    }

    if (!args.Snapshot.Exists)
    {
      Debug.LogError("Firebase snapshot doesn't exist");
      return;
    }
    // Do something with the data in args.Snapshot
    string leaderboardJson = args.Snapshot.GetRawJsonValue();
    playerList = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(leaderboardJson);

    // Go through each guid/player, get info for player, and add to list
    foreach (var guid in playerList)
    {

      Player player = new Player();


      foreach (var playerInfo in guid.Value)
      {
        switch (playerInfo.Key)
        {
          case "username":
            player.username = playerInfo.Value;
            break;
          case "score":
            player.score = playerInfo.Value;
            break;
          case "time":
            player.time = playerInfo.Value;
            break;
          default:
            Debug.LogError("Switch case didn't recognice a key");
            break;
        }
      }
      _players.Add(player);
    }

    _players.Sort(new PlayerComparer());
    _players.Reverse();

    RetreivedData(_players);
  }
}

// Used for sorting the players
class PlayerComparer : IComparer<Player>
{
  public int Compare(Player left, Player right)
  {
    return int.Parse(left.score) - int.Parse(right.score);
  }
}

public class Player
{
  public string username;
  public string score;
  public string time;

  public Player()
  {

  }

  public Player(string username, string score, string time)
  {
    this.username = username;
    this.score = score;
    this.time = time;
  }
}

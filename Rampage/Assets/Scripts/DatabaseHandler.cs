using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;
using ProfanityFilter;
using System.Threading.Tasks;
public class DatabaseHandler : MonoBehaviour
{
  private DatabaseReference _dbReference;
  private string _userID;
  private Dictionary<string, Dictionary<string, string>> playerList { get; set; }
  private GameManager _gameManager;
  private bool isPlayerSaved;
  private List<Player> _players = new List<Player>();
  ProfanityFilter.ProfanityFilter filter;

  // Event 
  public delegate void DatabaseAction(List<Player> players);
  public static event DatabaseAction RetreivedData;
  void Awake()
  {
    filter = new ProfanityFilter.ProfanityFilter();
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

  public async void SavePlayerStats(string username, string score, string time, string seconds)
  {
    bool isBadWord = await Task.Run(() => filter.IsProfanity(username));
    // Don't allow a username with profanity to get posted to the leaderboard
    if (isBadWord)
    {
      return;
    }


    // var player = new Dictionary<string, Dictionary<string, string>>();
    var playerStats = new Dictionary<string, string>() {
        {"username", username},
        {"score", score},
        {"seconds", seconds},
        {"time", time},
    };
    // player.Add(
    //     System.Guid.NewGuid().ToString(),
    //     playerStats
    // );

    string playerJson = JsonConvert.SerializeObject(playerStats);
    string sysid = SystemInfo.deviceUniqueIdentifier;

    var snapshot = await _dbReference.Child("players").Child(sysid).GetValueAsync();

    if (snapshot.Exists)
    {
      string fetchedScore = snapshot.Child("score").Value.ToString();
      string fetchedSeconds = snapshot.Child("seconds").Value.ToString();
      int dbScore = int.Parse(fetchedScore);
      float dbSeconds = float.Parse(fetchedSeconds);
      int intScore = int.Parse(score);
      float floatSeconds = float.Parse(seconds);

      if (dbScore > intScore)
      {
        return;
      }

      if (dbScore == intScore)
      {
        if (dbSeconds < floatSeconds)
        {
          return;
        }
      }
    }


    // string guid = System.Guid.NewGuid().ToString();
    await _dbReference.Child("players").Child(sysid).SetRawJsonValueAsync(playerJson);
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
    foreach (var sysid in playerList)
    {

      Player player = new Player();


      foreach (var playerInfo in sysid.Value)
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
          case "seconds":
            player.seconds = playerInfo.Value;
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
    if (_players.Count > 100)
    {
      _players = _players.GetRange(0, 100);
    }

    RetreivedData(_players);
  }
}

// Used for sorting the players
class PlayerComparer : IComparer<Player>
{
  public int Compare(Player left, Player right)
  {
    int leftScore = int.Parse(left.score);
    int rightScore = int.Parse(right.score);
    float leftSeconds = float.Parse(left.seconds);
    float rightSeconds = float.Parse(right.seconds);

    if (leftScore == rightScore)
    {
      if (leftSeconds == rightSeconds) { return 0; }

      if (leftSeconds > rightSeconds)
      {
        return -1;
      }
      else
      {
        return 1;
      }
    }
    return leftScore - rightScore;
  }
}

public class Player
{
  public string username;
  public string score;
  public string seconds;
  public string time;

  public Player()
  {

  }

  public Player(string username, string score, string time, string seconds)
  {
    this.username = username;
    this.score = score;
    this.time = time;
    this.seconds = seconds;
  }
}

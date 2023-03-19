using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MenuHandler : MonoBehaviour
{
  public InputActionProperty joystickButton;
  public TextMeshProUGUI handMenuNameTextMesh;
  public TextMeshProUGUI handMenuTimeTextMesh;
  public TextMeshProUGUI handMenuScoreTextMesh;
  public TextMeshProUGUI endButtonText;
  public TextMeshProUGUI prefabPlayerInfoText;
  public GameObject playerInfoContainer;
  private RectTransform _canvas;
  private float _canvasWidth;
  private float _canvasHeight;
  private bool _joystickClickActive;
  private float _time;
  private float lerpedScale;
  private float lerpedShrinkingScale;
  private GameManager _gameManager;
  private bool _isLeaderboardHandled;
  private bool _isMenuOpen;
  public delegate void MenuEvent();
  public static event MenuEvent MenuOpen;
  public static event MenuEvent MenuClosed;
  void Awake()
  {
    _canvas = GetComponent<RectTransform>();
    _canvasWidth = _canvas.localScale.x;
    _canvasHeight = _canvas.localScale.y;
    _joystickClickActive = false;
    _time = 0;
    _isLeaderboardHandled = false;
    _isMenuOpen = false;

    DatabaseHandler.RetreivedData += DisplayPlayers;

  }

  void OnDisable()
  {
    DatabaseHandler.RetreivedData -= DisplayPlayers;
  }

  // Start is called before the first frame update
  void Start()
  {
    _gameManager = GameManager.gameManager;

    // Set the username on the hand menu
    handMenuNameTextMesh.text = _gameManager.GetUsername();

    // Set initial size
    _canvas.localScale = Vector3.zero;


  }

  public void DisplayPlayers(List<Player> players)
  {
    int listedPlayersCount = playerInfoContainer.transform.childCount;
    List<TextMeshProUGUI> textMeshes = new List<TextMeshProUGUI>();

    // If players already listed, remove them to update the leaderboard
    if (listedPlayersCount > 0)
    {
      for (int i = 0; i < listedPlayersCount; i++)
      {
        Transform listedPlayer = playerInfoContainer.transform.GetChild(i);
        if (listedPlayer == null) { continue; }
        Destroy(listedPlayer.GetComponent<TextMeshProUGUI>());
        Destroy(listedPlayer.gameObject);
      }
    }

    for (int i = 0; i < players.Count; i++)
    {
      string playerUsername = players[i].username;
      string playerScore = players[i].score;
      string playerTime = players[i].time;
      string text = $"{playerUsername}/{playerScore}/{playerTime}";
      var textMesh = Instantiate(prefabPlayerInfoText);
      textMesh.SetText(text);
      textMesh.transform.SetParent(playerInfoContainer.transform);
      textMesh.transform.localRotation = Quaternion.Euler(Vector3.zero);
      textMesh.transform.localScale = Vector3.one;
      textMesh.rectTransform.localRotation = prefabPlayerInfoText.rectTransform.localRotation;
      textMesh.rectTransform.localPosition = prefabPlayerInfoText.rectTransform.localPosition;
      textMesh.alignment = TextAlignmentOptions.Center;
      textMesh.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 16);
      // textMesh.autoSizeTextContainer = false;
      textMesh.enableAutoSizing = false;
      textMesh.fontSize = 16;
      textMesh.enabled = true;

      textMeshes.Add(textMesh);
    }

  }

  // Update is called once per frame
  void Update()
  {
    ShowMenu();
    // Change button text if the run is ended
    if (_gameManager.IsRunEnded())
    {
      endButtonText.text = "Restart";

    }
    else
    {

      handMenuTimeTextMesh.text = _gameManager.GetTime();
      handMenuScoreTextMesh.text = _gameManager.GetScore().ToString();

    }
  }

  public void EndGameButtonClicked()
  {
    print("Exit the game");
  }

  void ShowMenu()
  {

    _joystickClickActive = joystickButton.action.ReadValue<float>() == 1;

    if (_joystickClickActive)
    {
      MenuOpen();
    }
    else
    {
      MenuClosed();
    }

    if (_joystickClickActive)
    {
      if (_time < 0.2f)
      {
        _time += Time.deltaTime;
      }
      else
      {
        _time = 0.2f;
      }
      lerpedScale = Mathf.Lerp(0, 0.01f, _time / 0.2f);
      _canvas.localScale = new Vector3(lerpedScale, lerpedScale, 0.01f);
      // _time += Time.deltaTime;
    }
    else
    {
      if (_time > 0)
      {
        _time -= Time.deltaTime;
      }
      else
      {
        _time = 0;
      }

      lerpedShrinkingScale = Mathf.Lerp(0, _canvas.localScale.x, _time * 2f);
      _canvas.localScale = new Vector3(lerpedShrinkingScale, lerpedShrinkingScale, 0.01f);

    }

  }
}

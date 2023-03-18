using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MenuHandler : MonoBehaviour
{
  public InputActionProperty joystickButton;
  public TextMeshProUGUI buttonText;
  private RectTransform _canvas;
  private float _canvasWidth;
  private float _canvasHeight;
  private bool _joystickClickActive;
  private float _time;
  private float maxScale = 0.1f;
  private Collider test;

  private float lerpedScale;
  private float lerpedShrinkingScale;
  void Awake()
  {
    _canvas = GetComponent<RectTransform>();
    _canvasWidth = _canvas.localScale.x;
    _canvasHeight = _canvas.localScale.y;
    _joystickClickActive = false;
    _time = 0;

  }
  // Start is called before the first frame update
  void Start()
  {


    // Set initial size
    _canvas.localScale = Vector3.zero;


  }

  // Update is called once per frame
  void Update()
  {
    ShowMenu();

    // Change button text if the run is ended
    if (GameManager.gameManager.IsRunEnded())
    {
      buttonText.text = "Restart";

    }

  }

  public void EndGameButtonClicked()
  {
    print("IT IS CLICKED");
  }

  void ShowMenu()
  {
    _joystickClickActive = joystickButton.action.ReadValue<float>() == 1;

    if (_joystickClickActive)
    {
      if (_time >= 0.2)
      {
        return;
      }
      lerpedScale = Mathf.Lerp(0, 0.01f, _time / 0.2f);
      _canvas.localScale = new Vector3(lerpedScale, lerpedScale, 0.01f);
      _time += Time.deltaTime;

    }
    else
    {
      if (_time <= 0)
      {
        return;
      }

      lerpedShrinkingScale = Mathf.Lerp(0, _canvas.localScale.x, _time * 2f);
      _canvas.localScale = new Vector3(lerpedShrinkingScale, lerpedShrinkingScale, 0.01f);
      _time -= Time.deltaTime;

    }

  }
}

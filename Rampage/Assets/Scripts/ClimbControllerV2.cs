using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;

public class ClimbControllerV2 : MonoBehaviour
{
  public InputActionProperty controllerInput;
  public static ClimbControllerV2 currController;
  private float _grabPressed;
  private bool _isGrabbing;
  private bool _printed;

  // Start is called before the first frame update
  void Awake()
  {
    currController = null;
    _printed = false;
    _isGrabbing = false;

  }

  // Update is called once per frame
  void Update()
  {
    // Get the grip input from controller
    _grabPressed = controllerInput.action.ReadValue<float>();



    // Player let go
    if (_grabPressed == 0)
    {
      if (currController == this)
      {
        currController = null;
        _isGrabbing = false;
      }
    }
  }

  void OnTriggerExit(Collider col)
  {
    if (col.tag == "WallChunk")
    {
      if (currController == this)
      {
        currController = null;
        // isGrabbing = false;

      }
    }
  }

  void OnTriggerStay(Collider col)
  {
    if (col.tag == "WallChunk" && _grabPressed == 1 && currController != this)
    {
      currController = this;
      // if (!isGrabbing)
      // {
      // isGrabbing = true;

      // }
    }

  }
}

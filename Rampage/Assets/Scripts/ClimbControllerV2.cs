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
  float grabPressed;
  bool isGrabbing;
  bool printed;

  // Start is called before the first frame update
  void Start()
  {
    currController = null;
    printed = false;
    isGrabbing = false;

  }

  // Update is called once per frame
  void Update()
  {
    // Get the grip input from controller
    grabPressed = controllerInput.action.ReadValue<float>();


    // Controller let go
    if (grabPressed == 0)
    {
      isGrabbing = false;
    }

    // Release if not grabbing
    if (!isGrabbing && currController == this)
    {
      currController = null;
    }


  }

  void OnTriggerExit(Collider col)
  {
    if (col.tag == "WallChunk")
    {
      isGrabbing = false;
    }
  }

  void OnTriggerStay(Collider col)
  {
    if (col.tag == "WallChunk" && grabPressed > 0 && currController != this)
    {
      if (!isGrabbing)
      {
        currController = this;
        isGrabbing = true;

      }
    }

  }
}

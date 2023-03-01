using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingManagerV2 : MonoBehaviour
{
  public ConfigurableJoint jointHandle;
  private Rigidbody xrRb;
  private bool isJointAttached;
  private CharacterStateManager characterStateManager;
  private ClimbControllerV2 activeController;
  // Start is called before the first frame update
  void Start()
  {
    xrRb = GetComponent<Rigidbody>();
    characterStateManager = GetComponent<CharacterStateManager>();
    isJointAttached = false;
    activeController = null;


  }

  // Update is called once per frame
  void Update()
  {

    // If ready to climb
    if (ClimbControllerV2.currController)
    {
      // Check if already climbing with the active controller
      if (activeController == ClimbControllerV2.currController)
      {
        // Just move the player
        jointHandle.targetPosition = -ClimbControllerV2.currController.transform.localPosition;
        return;
      }
      else
      {
        // Set the new active controller and start climbing with it
        activeController = ClimbControllerV2.currController;
      }
      startClimbing();
    }
    else
    {
      notClimbing();

    }


  }
  void startClimbing()
  {
    characterStateManager.deactivateOpenXRComponents();


    jointHandle.connectedBody = xrRb;
    jointHandle.transform.position = ClimbControllerV2.currController.transform.position;
    jointHandle.transform.rotation = xrRb.rotation;
    jointHandle.targetPosition = -ClimbControllerV2.currController.transform.localPosition;
    xrRb.useGravity = false;
    xrRb.isKinematic = false;
    isJointAttached = true;

  }

  void notClimbing()
  {
    activeController = null;
    isJointAttached = false;
    jointHandle.connectedBody = null;
    xrRb.isKinematic = true;
    characterStateManager.activateOpenXRComponents();

  }
}

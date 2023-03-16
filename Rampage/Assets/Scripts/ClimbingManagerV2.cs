using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingManagerV2 : MonoBehaviour
{
  public ConfigurableJoint jointHandle;
  private Rigidbody _xrRb;
  private bool _isJointAttached;
  private CharacterStateManager _characterStateManager;
  private ClimbControllerV2 _activeController;
  // Start is called before the first frame update
  void Awake()
  {
    _xrRb = GetComponent<Rigidbody>();
    _characterStateManager = GetComponent<CharacterStateManager>();
    _isJointAttached = false;
    _activeController = null;
  }

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // if (ClimbControllerV2.currController != null)
    // {
    //   print("Printing");

    // }


    // There is a controller gripping and in a wall
    if (ClimbControllerV2.currController != null)
    {

      CharacterStateManager.isLeaping = false;

      // Check if already climbing with the active controller
      if (_activeController != null && _activeController == ClimbControllerV2.currController)
      {
        // Just move the player
        jointHandle.targetPosition = -ClimbControllerV2.currController.transform.localPosition;
        return;
      }
      else
      {
        // Set the new active controller and start climbing with it
        _activeController = ClimbControllerV2.currController;
        StartClimbing();

      }



    }
    else
    {
      if (CharacterStateManager.isClimbing)
      {
        StopClimbing();
        //print("Deactivated Climbing");
      }
    }


  }
  void StartClimbing()
  {
    //print("STARTED CLIMBING");
    // _characterStateManager.DeactivateOpenXRComponents();
    // _characterStateManager.ActivateClimbingPhysics();
    _characterStateManager.ClimbingState();

    jointHandle.connectedBody = _xrRb;
    // jointHandle.transform.position = ClimbControllerV2.currController.transform.position;
    jointHandle.transform.position = _activeController.transform.position;
    jointHandle.transform.rotation = _xrRb.rotation;
    // jointHandle.targetPosition = -ClimbControllerV2.currController.transform.localPosition;
    jointHandle.targetPosition = -_activeController.transform.localPosition;

    // _xrRb.useGravity = false;
    // _xrRb.isKinematic = false;
    _isJointAttached = true;

    // CharacterStateManager.isClimbing = true;
    return;
  }

  void StopClimbing()
  {
    _activeController = null;
    _isJointAttached = false;
    jointHandle.connectedBody = null;
    // _xrRb.isKinematic = true;
    // _characterStateManager.DeactivatePhysics();
    // _characterStateManager.ActivateOpenXRComponents();
    _characterStateManager.NormalState();

    // CharacterStateManager.isClimbing = false;
    return;
  }
}

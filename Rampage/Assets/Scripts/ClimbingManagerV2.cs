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

    // If ready to climb
    if (ClimbControllerV2.currController)
    {
      // Check if already climbing with the active controller
      if (_activeController == ClimbControllerV2.currController)
      {
        // Just move the player
        jointHandle.targetPosition = -ClimbControllerV2.currController.transform.localPosition;
        return;
      }
      else
      {
        // Set the new active controller and start climbing with it
        _activeController = ClimbControllerV2.currController;
      }

      startClimbing();
      CharacterStateManager.isClimbing = true;
      CharacterStateManager.isLeaping = false;
    }
    else
    {
      if (CharacterStateManager.isClimbing)
      {
        notClimbing();
        CharacterStateManager.isClimbing = false;
      }

    }


  }
  void startClimbing()
  {
    _characterStateManager.DeactivateOpenXRComponents();

    jointHandle.connectedBody = _xrRb;
    jointHandle.transform.position = ClimbControllerV2.currController.transform.position;
    jointHandle.transform.rotation = _xrRb.rotation;
    jointHandle.targetPosition = -ClimbControllerV2.currController.transform.localPosition;

    _xrRb.useGravity = false;
    _xrRb.isKinematic = false;
    _isJointAttached = true;
  }

  void notClimbing()
  {
    _activeController = null;
    _isJointAttached = false;
    jointHandle.connectedBody = null;
    _xrRb.isKinematic = true;
    _characterStateManager.ActivateOpenXRComponents();
  }
}

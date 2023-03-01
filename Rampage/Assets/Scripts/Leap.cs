using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Leap : MonoBehaviour
{
  public InputActionProperty rightTrigger;
  public GameObject xrOrigin;
  private Component inputActionManager;
  private Component locomotionSystem;
  private Component continuousMoveProvider;
  private Component characterController;
  private Component continuousTurnProvider;
  private Rigidbody rb;
  private CapsuleCollider capsuleCollider;
  bool isLeapActive = false;


  public static Vector3 rightControllerVector;
  public static Vector3 leftControllerVector;
  // Start is called before the first frame update
  void Start()
  {
    rightControllerVector = Vector3.zero;
    leftControllerVector = Vector3.zero;

  }

  // Update is called once per frame
  void Update()
  {
    float rightTrigerActivated = rightTrigger.action.ReadValue<float>();

    if (rightControllerVector != Vector3.zero && leftControllerVector != Vector3.zero && rightTrigerActivated == 1 && !isLeapActive)
    {
      deactivateDefaultState();
      activateLeapState();
      isLeapActive = true;
    }
  }

  void FixedUpdate()
  {
    if (isLeapActive)
    {
      rb.AddForce(Vector3.up, ForceMode.Impulse);
    }
  }


  void activateDefaultState()
  {

    xrOrigin.GetComponent<LocomotionSystem>().enabled = true;
    xrOrigin.GetComponent<ContinuousMoveProviderBase>().enabled = true;
    xrOrigin.GetComponent<ContinuousTurnProviderBase>().enabled = true;
    xrOrigin.GetComponent<CharacterController>().enabled = true;
  }

  void deactivateDefaultState()
  {
    xrOrigin.GetComponent<LocomotionSystem>().enabled = false;
    xrOrigin.GetComponent<ContinuousMoveProviderBase>().enabled = false;
    xrOrigin.GetComponent<ContinuousTurnProviderBase>().enabled = false;
    xrOrigin.GetComponent<CharacterController>().enabled = false;
  }

  void activateLeapState()
  {
    rb = xrOrigin.GetComponent<Rigidbody>();
    rb.isKinematic = false;

  }
  void deactivateLeapState()
  {
    rb = xrOrigin.GetComponent<Rigidbody>();
    rb.isKinematic = true;

  }


}

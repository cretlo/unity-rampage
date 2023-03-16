using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction;

public class CharacterStateManager : MonoBehaviour
{
  private LocomotionSystem locomotionSystem;
  private ContinuousMoveProviderBase continuousMoveProvider;
  private ContinuousTurnProviderBase continuousTurnProvider;
  private CharacterController characterController;
  private InputActionManager inputActionManager;
  private Rigidbody _xrRb;


  public static bool isClimbing = false;
  public static bool isLeaping = false;
  void Awake()
  {
    // isClimbing = false;
    // isLeaping = false;
    _xrRb = GetComponent<Rigidbody>();

    locomotionSystem = GetComponent<LocomotionSystem>();
    continuousMoveProvider = GetComponent<ContinuousMoveProviderBase>();
    continuousTurnProvider = GetComponent<ContinuousTurnProviderBase>();
    characterController = GetComponent<CharacterController>();
    inputActionManager = GetComponent<InputActionManager>();

  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ClimbingState()
  {
    _xrRb.isKinematic = false;
    _xrRb.useGravity = false;

    //inputActionManager.enabled = false;
    //locomotionSystem.enabled = false;
    continuousMoveProvider.enabled = false;
    continuousTurnProvider.enabled = false;
    characterController.enabled = false;

    CharacterStateManager.isClimbing = true;
  }

  public void NormalState()
  {
    _xrRb.isKinematic = true;
    _xrRb.useGravity = false;

    //inputActionManager.enabled = true;
    //locomotionSystem.enabled = true;
    characterController.enabled = true;
    continuousMoveProvider.enabled = true;
    continuousTurnProvider.enabled = true;

    CharacterStateManager.isClimbing = false;
    CharacterStateManager.isLeaping = false;

  }

  public void LeapingState()
  {
    //inputActionManager.enabled = false;
    // locomotionSystem.enabled = false;
    continuousMoveProvider.enabled = false;
    continuousTurnProvider.enabled = false;
    characterController.enabled = false;

    _xrRb.isKinematic = false;
    _xrRb.useGravity = true;

    CharacterStateManager.isLeaping = true;
  }
}

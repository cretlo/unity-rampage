using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class CharacterStateManager : MonoBehaviour
{
  private LocomotionSystem locomotionSystem;
  private ContinuousMoveProviderBase continuousMoveProvider;
  private ContinuousTurnProviderBase continuousTurnProvider;
  private CharacterController characterController;
  private InputActionManager inputActionManager;
  private Rigidbody _xrRb;
  public InputActionAsset inputAsset;


  public static bool isClimbing;
  public static bool isLeaping;
  void Awake()
  {
    isClimbing = false;
    isLeaping = false;
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
    //    inputActionManager.DisableInput();
    continuousMoveProvider.enabled = false;
    continuousTurnProvider.enabled = false;
    //locomotionSystem.enabled = false;
    characterController.enabled = false;

    CharacterStateManager.isClimbing = true;
  }

  public void NormalState()
  {
    _xrRb.isKinematic = true;
    _xrRb.useGravity = false;

    //inputActionManager.EnableInput();
    //inputActionManager.enabled = true;
    characterController.enabled = true;
    continuousMoveProvider.enabled = true;
    continuousTurnProvider.enabled = true;
    SetReferences();
    //locomotionSystem.enabled = true;

    CharacterStateManager.isClimbing = false;
    CharacterStateManager.isLeaping = false;

  }

  public void LeapingState()
  {
    //inputActionManager.DisableInput();
    //inputActionManager.enabled = false;
    continuousMoveProvider.enabled = false;
    continuousTurnProvider.enabled = false;
    characterController.enabled = false;
    //locomotionSystem.enabled = false;

    _xrRb.isKinematic = false;
    _xrRb.useGravity = true;

    CharacterStateManager.isLeaping = true;
  }

  private void SetReferences()
  {
    if (inputActionManager.actionAssets[0] == null)
    {
      print("Action Asset was null");
      inputActionManager.actionAssets[0] = inputAsset;
    }
    locomotionSystem.xrOrigin = GetComponent<XROrigin>();
    continuousMoveProvider.system = locomotionSystem;
    continuousTurnProvider.system = locomotionSystem;

  }
}

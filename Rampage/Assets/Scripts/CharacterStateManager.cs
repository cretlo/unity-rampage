using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class CharacterStateManager : MonoBehaviour
{
  public InputActionAsset inputAsset;
  private LocomotionSystem _locomotionSystem;
  private ActionBasedContinuousMoveProvider _continuousMoveProvider;
  private ActionBasedContinuousTurnProvider _continuousTurnProvider;
  private CharacterController _characterController;
  private InputActionManager _inputActionManager;
  private Rigidbody _xrRb;

  private InputActionProperty _leftInputAction;
  private InputActionProperty _rightInputAction;


  public static bool isClimbing;
  public static bool isLeaping;
  void Awake()
  {
    isClimbing = false;
    isLeaping = false;
    _xrRb = GetComponent<Rigidbody>();

    _locomotionSystem = GetComponent<LocomotionSystem>();
    _continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
    _continuousTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
    _characterController = GetComponent<CharacterController>();
    _inputActionManager = GetComponent<InputActionManager>();

    _leftInputAction = _continuousMoveProvider.leftHandMoveAction;
    _rightInputAction = _continuousTurnProvider.rightHandTurnAction;
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
    _continuousMoveProvider.enabled = false;
    _continuousTurnProvider.enabled = false;
    _characterController.enabled = false;
    _locomotionSystem.enabled = false;

    CharacterStateManager.isClimbing = true;
  }

  public void NormalState()
  {
    _xrRb.isKinematic = true;
    _xrRb.useGravity = false;

    //inputActionManager.EnableInput();
    _inputActionManager.enabled = true;
    _characterController.enabled = true;
    _continuousMoveProvider.enabled = true;
    _continuousTurnProvider.enabled = true;
    _locomotionSystem.enabled = true;
    SetReferences();

    CharacterStateManager.isClimbing = false;
    CharacterStateManager.isLeaping = false;

  }

  public void LeapingState()
  {
    //inputActionManager.DisableInput();
    //inputActionManager.enabled = false;
    _continuousMoveProvider.enabled = false;
    _continuousTurnProvider.enabled = false;
    _characterController.enabled = false;
    _locomotionSystem.enabled = false;

    _xrRb.isKinematic = false;
    _xrRb.useGravity = true;

    CharacterStateManager.isLeaping = true;
  }

  private void SetReferences()
  {
    if (_inputActionManager.actionAssets[0] == null)
    {
      print("Action Asset was null");
      _inputActionManager.actionAssets[0] = inputAsset;
    }
    _locomotionSystem.xrOrigin = GetComponent<XROrigin>();
    _continuousMoveProvider.leftHandMoveAction = _leftInputAction;
    _continuousMoveProvider.rightHandMoveAction = new InputActionProperty();
    _continuousMoveProvider.system = _locomotionSystem;
    _continuousTurnProvider.rightHandTurnAction = _rightInputAction;
    _continuousTurnProvider.leftHandTurnAction = new InputActionProperty();
    _continuousTurnProvider.system = _locomotionSystem;

  }
}

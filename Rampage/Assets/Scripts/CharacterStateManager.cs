using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction;

public class CharacterStateManager : MonoBehaviour
{
  private Component inputActionManager;
  private Component locomotionSystem;
  private Component continuousMoveProvider;
  private Component characterController;
  private Component continuousTurnProvider;
  private Rigidbody xrRb;
  // Start is called before the first frame update
  void Start()
  {
    xrRb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void deactivateOpenXRComponents()
  {
    GetComponent<LocomotionSystem>().enabled = false;
    GetComponent<ContinuousMoveProviderBase>().enabled = false;
    // GetComponent<ContinuousTurnProviderBase>().enabled = false;
    GetComponent<CharacterController>().enabled = false;

  }

  public void activateOpenXRComponents()
  {
    GetComponent<LocomotionSystem>().enabled = true;
    GetComponent<ContinuousMoveProviderBase>().enabled = true;
    // GetComponent<ContinuousTurnProviderBase>().enabled = true;
    GetComponent<CharacterController>().enabled = true;
  }

}
